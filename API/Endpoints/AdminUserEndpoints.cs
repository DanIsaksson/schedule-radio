// A.1 [Admin.Users] Admin-only account provisioning "doors".
// What:
// - POST /api/admin/users: create a user with a temporary password (MustChangePassword=true).
// - GET /api/admin/users: list users (optionally filtered by role).
// - DELETE /api/admin/users/{userId}: delete a user (with self-delete protection).
// Why: This project blocks public self-registration; admins are responsible for creating staff accounts.
// Where: Mapped in Program.cs via app.MapAdminUserEndpoints(); used from Swagger or a future admin UI.
using System.Security.Cryptography;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints;

// [Endpoint.Admin.Users.1] Admin user management endpoints
// These "doors" are used by an authenticated Admin to provision accounts.
public static class AdminUserEndpoints
{
    public static void MapAdminUserEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/admin")
            .RequireAuthorization("AdminOnly");

        // B.1 [Admin.Users.Create] Create a new Admin/Contributor account.
        // What: Writes an ApplicationUser row (AspNetUsers) + assigns a role, then returns the temp password.
        // Why: New users start with MustChangePassword=true so they must set a personal password on first login.
        // Where: The forced password-change rule is enforced by contributor endpoints (ContributorEndpoints.MustChangePasswordEnforcementFilter).
        group.MapPost("/users", async (
            AdminCreateUserRequest request,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager) =>
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return Results.BadRequest("Email is required.");
            }

            string email = request.Email.Trim();

            if (string.IsNullOrWhiteSpace(request.Role))
            {
                return Results.BadRequest("Role is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Address))
            {
                return Results.BadRequest("Address is required.");
            }

            string address = request.Address.Trim();

            if (string.IsNullOrWhiteSpace(request.Phone))
            {
                return Results.BadRequest("Phone is required.");
            }

            string phone = request.Phone.Trim();

            string role = NormalizeRole(request.Role);
            if (role.Length == 0)
            {
                return Results.BadRequest("Role must be 'Admin' or 'Contributor'.");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                return Results.BadRequest($"Role '{role}' does not exist.");
            }

            string tempPassword = GenerateTemporaryPassword();

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,

                Address = address,
                PhoneNumber = phone,
                Bio = string.IsNullOrWhiteSpace(request.Bio) ? null : request.Bio.Trim(),
                PhotoUrl = string.IsNullOrWhiteSpace(request.PhotoUrl) ? null : request.PhotoUrl.Trim(),

                MustChangePassword = true,
            };

            var createResult = await userManager.CreateAsync(user, tempPassword);
            if (!createResult.Succeeded)
            {
                var errors = createResult.Errors.Select(e => e.Description);
                return Results.BadRequest(new { Errors = errors });
            }

            var addToRoleResult = await userManager.AddToRoleAsync(user, role);
            if (!addToRoleResult.Succeeded)
            {
                await userManager.DeleteAsync(user);
                var errors = addToRoleResult.Errors.Select(e => e.Description);
                return Results.BadRequest(new { Errors = errors });
            }

            return Results.Ok(new AdminCreateUserResponse(
                UserId: user.Id,
                Email: user.Email ?? user.UserName ?? email,
                Role: role,
                TemporaryPassword: tempPassword));
        });

        // B.2 [Admin.Users.List] List users (optionally filtered by role).
        // Why: Admins need visibility into accounts + the MustChangePassword flag for support.
        group.MapGet("/users", async (
            string? role,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager) =>
        {
            string? normalizedRole = null;
            if (!string.IsNullOrWhiteSpace(role))
            {
                normalizedRole = NormalizeRole(role);
                if (normalizedRole.Length == 0)
                {
                    return Results.BadRequest("Role filter must be 'Admin' or 'Contributor'.");
                }

                if (!await roleManager.RoleExistsAsync(normalizedRole))
                {
                    return Results.BadRequest($"Role '{normalizedRole}' does not exist.");
                }
            }

            IList<ApplicationUser> users = normalizedRole is null
                ? await userManager.Users.OrderBy(u => u.Email).ToListAsync()
                : await userManager.GetUsersInRoleAsync(normalizedRole);

            var results = new List<AdminUserListItemResponse>(capacity: users.Count);
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);

                results.Add(new AdminUserListItemResponse(
                    UserId: user.Id,
                    Email: user.Email ?? user.UserName ?? string.Empty,
                    Phone: user.PhoneNumber,
                    Address: user.Address,
                    Bio: user.Bio,
                    PhotoUrl: user.PhotoUrl,
                    MustChangePassword: user.MustChangePassword,
                    Roles: roles.ToArray()));
            }

            return Results.Ok(results);
        });

        // B.3 [Admin.Users.Delete] Delete an account.
        // Why: We block self-delete to prevent an admin from locking themselves out of the system.
        group.MapDelete("/users/{userId}", async (
            string userId,
            HttpContext httpContext,
            UserManager<ApplicationUser> userManager) =>
        {
            string? currentUserId = userManager.GetUserId(httpContext.User);
            if (string.Equals(currentUserId, userId, StringComparison.Ordinal))
            {
                return Results.BadRequest("You cannot delete your own account.");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Results.NotFound();
            }

            var deleteResult = await userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                var errors = deleteResult.Errors.Select(e => e.Description);
                return Results.BadRequest(new { Errors = errors });
            }

            return Results.NoContent();
        });
    }

    private static string NormalizeRole(string role)
    {
        // C.1 [Admin.Users] Role normalization.
        // Why: Keeps inputs flexible (case-insensitive) while ensuring we only accept known roles.
        if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            return "Admin";
        }

        if (string.Equals(role, "Contributor", StringComparison.OrdinalIgnoreCase))
        {
            return "Contributor";
        }

        return string.Empty;
    }

    private static string GenerateTemporaryPassword(int length = 12)
    {
        // C.2 [Admin.Users] Temporary password generator.
        // Why: Ensures a dev-friendly password that still satisfies the Identity password rules configured in Program.cs.
        // Constraint: This is not meant as a production-grade password policy; it's a teaching-friendly helper.
        if (length < 8)
        {
            length = 8;
        }

        const string letters = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string allowed = letters + digits;

        var chars = new char[length];

        // Ensure we meet our Identity password rules:
        // - at least one lowercase
        // - at least one digit
        chars[0] = letters[RandomNumberGenerator.GetInt32(letters.Length)];
        chars[1] = digits[RandomNumberGenerator.GetInt32(digits.Length)];

        for (int i = 2; i < chars.Length; i++)
        {
            chars[i] = allowed[RandomNumberGenerator.GetInt32(allowed.Length)];
        }

        // Shuffle to avoid predictable positions for the guaranteed characters.
        for (int i = chars.Length - 1; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(i + 1);
            (chars[i], chars[j]) = (chars[j], chars[i]);
        }

        return new string(chars);
    }
}

public sealed record AdminCreateUserRequest(
    string? Email,
    string? Role,
    string? Address,
    string? Phone,
    string? Bio,
    string? PhotoUrl);

public sealed record AdminCreateUserResponse(
    string UserId,
    string Email,
    string Role,
    string TemporaryPassword);

public sealed record AdminUserListItemResponse(
    string UserId,
    string Email,
    string? Phone,
    string? Address,
    string? Bio,
    string? PhotoUrl,
    bool MustChangePassword,
    IReadOnlyList<string> Roles);
