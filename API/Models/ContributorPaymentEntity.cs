// A.1 [Db.Schema.Payments] Persisted monthly payment history row.
// What: One row per (UserId, PeriodYear, PeriodMonth) storing the calculated totals for that month.
// Why: Contributors should see a stable "ledger" (audit-friendly) instead of recalculating payouts on every request.
// Where:
// - Written by POST /api/admin/payments/calculate-previous-month (API/Endpoints/AdminPaymentsEndpoints.cs).
// - Read by GET /api/contributor/payments (API/Endpoints/ContributorEndpoints.cs).
// - Rendered in the staff portal UI at #/portal/me (frontend/src/App.jsx).
namespace API.Models;

public class ContributorPaymentEntity
{
    public int Id { get; set; }

    // B.1 The Identity user this payment row belongs to (AspNetUsers.Id).
    public string UserId { get; set; } = string.Empty;

    // B.2 The month this row summarizes (year + month is the "period key").
    // Invariant: SchedulerContext enforces a unique index on (PeriodYear, PeriodMonth, UserId).
    public int PeriodYear { get; set; }

    public int PeriodMonth { get; set; }

    // B.3 Calculated work volume.
    public int TotalMinutes { get; set; }

    // B.4 Calculated money totals (units: SEK).
    public decimal HourlyRate { get; set; }

    public decimal BaseAmount { get; set; }

    public int EventCount { get; set; }

    public decimal EventBonusAmount { get; set; }

    public decimal Subtotal { get; set; }

    public decimal VatRate { get; set; }

    public decimal VatAmount { get; set; }

    public decimal TotalIncludingVat { get; set; }

    public DateTime CalculatedAt { get; set; }
}
