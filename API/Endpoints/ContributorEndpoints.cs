// A.1 [Contributor] Contributor self-service "doors" (profile + payments).
// What:
// - GET /api/contributor/me: returns the logged-in contributor's profile fields + roles.
// - GET /api/contributor/payments: returns monthly payment history rows (ContributorPayments).
// Why: The staff portal frontend needs a safe, authenticated way to show "My account" information.
// Where: Consumed by the React staff portal in frontend/src/App.jsx and frontend/src/auth/AuthContext.jsx.
using API.Models;
using API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Endpoints;

public static class ContributorEndpoints
{
    public static void MapContributorEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/contributor")
            .RequireAuthorization();

        // B.1 [Contributor.Security] Enforce the forced password-change rule.
        // Why: Admin-created accounts use a temporary password; we block most actions until the user sets a personal password.
        // Where:
        // - This filter blocks protected contributor endpoints when MustChangePassword == true.
        // - The frontend detects the flag via GET /api/contributor/me and redirects to #/portal/change-password.
        group.AddEndpointFilter<MustChangePasswordEnforcementFilter>();

        // B.2 [Contributor.Me] "Profile card" endpoint.
        // What: Returns identity profile fields (email/phone/address/bio/photo) + role list.
        // Why: The portal needs one source-of-truth response for the account screen.
        // Where: Called by AuthContext.refreshMe() (frontend/src/auth/AuthContext.jsx).
        group.MapGet("/me", async (
            HttpContext httpContext,
            UserManager<ApplicationUser> userManager) =>
        {
            var user = await userManager.GetUserAsync(httpContext.User);
            if (user is null)
            {
                return Results.Unauthorized();
            }

            var roles = await userManager.GetRolesAsync(user);

            return Results.Ok(new ContributorMeResponse(
                Email: user.Email ?? user.UserName ?? string.Empty,
                Phone: user.PhoneNumber,
                Address: user.Address,
                Bio: user.Bio,
                PhotoUrl: user.PhotoUrl,
                MustChangePassword: user.MustChangePassword,
                Roles: roles.ToArray()));
        });

        // B.3 [Contributor.Payments] Read payment history for the logged-in contributor.
        // What: Returns persisted monthly summary rows (ContributorPaymentEntity).
        // Why: Payment history is calculated by an admin batch job and should be stable (auditable).
        // Where:
        // - Created/updated by POST /api/admin/payments/calculate-previous-month.
        // - Displayed in the staff portal at #/portal/me (frontend/src/App.jsx).
        group.MapGet("/payments", async (
            HttpContext httpContext,
            UserManager<ApplicationUser> userManager,
            SchedulerContext db) =>
        {
            var user = await userManager.GetUserAsync(httpContext.User);
            if (user is null)
            {
                return Results.Unauthorized();
            }

            var payments = await db.ContributorPayments
                .Where(p => p.UserId == user.Id)
                .OrderByDescending(p => p.PeriodYear)
                .ThenByDescending(p => p.PeriodMonth)
                .ToListAsync();

            return Results.Ok(payments);
        });
    }

    private sealed class MustChangePasswordEnforcementFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            HttpContext httpContext = context.HttpContext;

            if (HttpMethods.IsOptions(httpContext.Request.Method))
            {
                return await next(context);
            }

            string? path = httpContext.Request.Path.Value;
            if (string.Equals(path, "/api/contributor/me", StringComparison.OrdinalIgnoreCase))
            {
                // Why: /me is the one endpoint the frontend needs to call to *discover* that MustChangePassword is true.
                return await next(context);
            }

            var userManager = httpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.GetUserAsync(httpContext.User);
            if (user is null)
            {
                return Results.Unauthorized();
            }

            if (user.MustChangePassword)
            {
                // Why: We deliberately return 403 (not 401) so the client can distinguish "logged in but blocked".
                return Results.StatusCode(StatusCodes.Status403Forbidden);
            }

            return await next(context);
        }
    }
}

public sealed record ContributorMeResponse(
    string Email,
    string? Phone,
    string? Address,
    string? Bio,
    string? PhotoUrl,
    bool MustChangePassword,
    IReadOnlyList<string> Roles);
