// A.1 [Payments] PaymentCalculator (business rules for contributor compensation).
// What: Turns a contributor's scheduled events for a month into payment totals (minutes, event bonus, VAT, grand total).
// Why: Keeps payment rules in one place so endpoints/UI don't duplicate money math.
// Where:
// - Called by POST /api/admin/payments/calculate-previous-month (API/Endpoints/AdminPaymentsEndpoints.cs).
// - Results are persisted in the ContributorPayments table and shown to contributors in #/portal/me (frontend/src/App.jsx).
using API.Models;

namespace API.Services;

public sealed class PaymentCalculator
{
    // B.1 [Payments] Default rates for this assignment (units: SEK + fraction).
    // Why: Centralizing the "magic numbers" here makes it obvious what to change if the business rules evolve.
    public const decimal DefaultHourlyRate = 750m;

    public const decimal DefaultEventBonus = 300m;

    public const decimal DefaultVatRate = 0.25m;

    // B.2 CalculateForUserAndPeriod: compute one monthly payment summary for one contributor.
    // Intent: Produce the same numbers that end up in ContributorPaymentEntity (TotalMinutes, BaseAmount, EventBonusAmount, VatAmount, TotalIncludingVat).
    // Inputs/outputs:
    // - `events` should already be filtered to the requested month by the caller (AdminPaymentsEndpoints does this).
    // - Minutes are calculated from Date+Hour+StartMinute/EndMinute; amounts are in SEK.
    // Notes:
    // - "EventCount" is a count of merged logical events (back-to-back intervals with identical details are treated as one).
    // - Domain note: `HasGuest` currently only affects merge boundaries; future rules could add guest travel costs.
    public PaymentCalculationResult CalculateForUserAndPeriod(
        string userId,
        int periodYear,
        int periodMonth,
        IReadOnlyList<EventEntity> events)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("UserId is required.", nameof(userId));
        }

        if (periodMonth < 1 || periodMonth > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(periodMonth), "PeriodMonth must be 1..12.");
        }

        var intervals = events
            .Where(e => string.Equals(e.ResponsibleUserId, userId, StringComparison.Ordinal))
            .Select(EventInterval.FromEvent)
            .OrderBy(i => i.Start)
            .ToList();

        int mergedEventCount = 0;
        int totalMinutes = 0;

        EventInterval? current = null;
        foreach (var next in intervals)
        {
            if (current is null)
            {
                current = next;
                mergedEventCount = 1;
                continue;
            }

            if (CanMerge(current, next))
            {
                current = current.Merge(next);
                continue;
            }

            totalMinutes += current.DurationMinutes;
            mergedEventCount++;
            current = next;
        }

        if (current is not null)
        {
            totalMinutes += current.DurationMinutes;
        }

        decimal hours = totalMinutes / 60m;
        decimal hourlyRate = DefaultHourlyRate;
        decimal baseAmount = hourlyRate * hours;

        decimal eventBonusAmount = DefaultEventBonus * mergedEventCount;
        decimal subtotal = baseAmount + eventBonusAmount;

        decimal vatRate = DefaultVatRate;
        decimal vatAmount = subtotal * vatRate;
        decimal totalIncludingVat = subtotal + vatAmount;

        return new PaymentCalculationResult(
            TotalMinutes: totalMinutes,
            HourlyRate: hourlyRate,
            BaseAmount: baseAmount,
            EventCount: mergedEventCount,
            EventBonusAmount: eventBonusAmount,
            Subtotal: subtotal,
            VatRate: vatRate,
            VatAmount: vatAmount,
            TotalIncludingVat: totalIncludingVat);
    }

    // B.3 [Payments] Merge rule for "logical events".
    // Why: Bonus per event should reflect a continuous show segment, not individual DB rows.
    // Invariant: Only merge when intervals touch exactly AND all show metadata matches.
    private static bool CanMerge(EventInterval current, EventInterval next)
    {
        if (current.End != next.Start)
        {
            return false;
        }

        return string.Equals(current.Title, next.Title, StringComparison.Ordinal)
               && string.Equals(current.EventType, next.EventType, StringComparison.Ordinal)
               && current.HostCount == next.HostCount
               && current.HasGuest == next.HasGuest;
    }

    // C.1 [Payments] EventInterval is a small "value object" for time math.
    // What: Converts EventEntity (date/hour/minutes) into real DateTime Start/End so we can merge + measure durations.
    // Where: Created by FromEvent() and consumed in the merge loop above.
    private sealed record EventInterval(
        DateTime Start,
        DateTime End,
        string? Title,
        string? EventType,
        int? HostCount,
        bool HasGuest)
    {
        public int DurationMinutes => (int)(End - Start).TotalMinutes;

        public static EventInterval FromEvent(EventEntity e)
        {
            DateTime start = e.Date.Date.AddHours(e.Hour).AddMinutes(e.StartMinute);
            DateTime end = e.Date.Date.AddHours(e.Hour).AddMinutes(e.EndMinute);

            return new EventInterval(
                Start: start,
                End: end,
                Title: e.Title,
                EventType: e.EventType,
                HostCount: e.HostCount,
                HasGuest: e.HasGuest);
        }

        public EventInterval Merge(EventInterval next)
        {
            return this with { End = next.End };
        }
    }
}

public sealed record PaymentCalculationResult(
    int TotalMinutes,
    decimal HourlyRate,
    decimal BaseAmount,
    int EventCount,
    decimal EventBonusAmount,
    decimal Subtotal,
    decimal VatRate,
    decimal VatAmount,
    decimal TotalIncludingVat);
