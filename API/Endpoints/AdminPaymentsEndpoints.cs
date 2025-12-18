// A.1 [Payments.Admin] Admin-only payment calculation "door".
// What: Creates/updates monthly summary rows in the ContributorPayments table.
// Why: Contributors should see a stable, auditable monthly payment history (instead of recalculating on every page load).
// Where:
// - Triggered by POST /api/admin/payments/calculate-previous-month (Swagger/Admin tooling).
// - Read by contributors via GET /api/contributor/payments (see ContributorEndpoints.cs) and shown in #/portal/me (frontend/src/App.jsx).
using API.Data;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints;

public static class AdminPaymentsEndpoints
{
    public static void MapAdminPaymentsEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/admin")
            .RequireAuthorization("AdminOnly");

        // B.1 [Payments.Admin] "Calculate previous month" job.
        // What: Computes the period based on DateTime.Today.AddMonths(-1).
        // Why: Mimics a monthly payroll batch job.
        // Constraint: This endpoint does NOT accept a custom year/month; for backfilling older months you must adjust the system date or add a new endpoint.
        group.MapPost("/payments/calculate-previous-month", async (
            UserManager<ApplicationUser> userManager,
            SchedulerContext db,
            PaymentCalculator calculator) =>
        {
            DateTime today = DateTime.Today;
            DateTime previousMonth = today.AddMonths(-1);

            int periodYear = previousMonth.Year;
            int periodMonth = previousMonth.Month;

            DateTime periodStart = new DateTime(periodYear, periodMonth, 1);
            DateTime periodEnd = periodStart.AddMonths(1);

            IList<ApplicationUser> contributors = await userManager.GetUsersInRoleAsync("Contributor");

            int created = 0;
            int updated = 0;

            DateTime calculatedAt = DateTime.UtcNow;

            foreach (var contributor in contributors)
            {
                string userId = contributor.Id;

                var contributorEvents = await db.Events
                    .Where(e => e.ResponsibleUserId == userId && e.Date >= periodStart && e.Date < periodEnd)
                    .ToListAsync();

                var calc = calculator.CalculateForUserAndPeriod(userId, periodYear, periodMonth, contributorEvents);

                var existing = await db.ContributorPayments.SingleOrDefaultAsync(p =>
                    p.UserId == userId
                    && p.PeriodYear == periodYear
                    && p.PeriodMonth == periodMonth);

                if (existing is null)
                {
                    db.ContributorPayments.Add(new ContributorPaymentEntity
                    {
                        UserId = userId,
                        PeriodYear = periodYear,
                        PeriodMonth = periodMonth,
                        TotalMinutes = calc.TotalMinutes,
                        HourlyRate = calc.HourlyRate,
                        BaseAmount = calc.BaseAmount,
                        EventCount = calc.EventCount,
                        EventBonusAmount = calc.EventBonusAmount,
                        Subtotal = calc.Subtotal,
                        VatRate = calc.VatRate,
                        VatAmount = calc.VatAmount,
                        TotalIncludingVat = calc.TotalIncludingVat,
                        CalculatedAt = calculatedAt,
                    });

                    created++;
                    continue;
                }

                existing.TotalMinutes = calc.TotalMinutes;
                existing.HourlyRate = calc.HourlyRate;
                existing.BaseAmount = calc.BaseAmount;
                existing.EventCount = calc.EventCount;
                existing.EventBonusAmount = calc.EventBonusAmount;
                existing.Subtotal = calc.Subtotal;
                existing.VatRate = calc.VatRate;
                existing.VatAmount = calc.VatAmount;
                existing.TotalIncludingVat = calc.TotalIncludingVat;
                existing.CalculatedAt = calculatedAt;

                updated++;
            }

            await db.SaveChangesAsync();

            return Results.Ok(new
            {
                PeriodYear = periodYear,
                PeriodMonth = periodMonth,
                ContributorsProcessed = contributors.Count,
                Created = created,
                Updated = updated,
            });
        });
    }
}
