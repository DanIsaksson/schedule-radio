# Payment calculation plan

## A. Business rules
- **A.1 Period**
  - Payout is calculated for the **previous month**.
- **A.2 Rates**
  - 750 SEK per hour
  - +300 SEK per scheduled event
- **A.3 VAT**
  - VAT rate = 25% of monthly subtotal

## B. Why we need "merge logic"
Events in the current database are stored as **hour rows**:
- `Date`, `Hour`, `StartMinute`, `EndMinute`

A 2-hour show will likely be stored as **2 rows**.
But the assignment says:
- 2 hours => 1500 SEK
- +300 SEK **once** per scheduled event

So we must merge those rows into a single "logical show" before counting the bonus.

## C. Proposed algorithm
### C.1 Build intervals
For each `EventEntity` row:
- Build start time:
  - `start = Date + Hour + StartMinute`
- Build end time:
  - `end = Date + Hour + EndMinute`
  - Remember: `EndMinute` is exclusive (so duration is `EndMinute - StartMinute` minutes)

### C.2 Sort
- Sort by `ResponsibleUserId`, then `start`.

### C.3 Merge contiguous blocks
Two intervals become one "scheduled event" if:
- Same `ResponsibleUserId`
- Same `Title`
- Same `EventType`
- Same `HostCount`
- Same `HasGuest`
- `previous.end == next.start`

Why we are strict here:
- It prevents accidentally merging two different shows that happen to be adjacent in time.

Result:
- `mergedEventCount` = number of merged blocks
- `totalMinutes` = sum of minutes in merged blocks

## D. Payment formula (per contributor)
- `hours = totalMinutes / 60m`
- `base = 750m * hours`
- `bonus = 300m * mergedEventCount`
- `subtotal = base + bonus`
- `vat = subtotal * 0.25m`
- `totalIncludingVat = subtotal + vat`

## E. Storage (payment history)
Create one `ContributorPayments` row per user per month:
- PeriodYear
- PeriodMonth
- Totals (minutes, hours, eventCount)
- Amounts (base, bonus, vat, total)
- CalculatedAt timestamp

## F. Admin workflow
- Admin triggers calculation for previous month.
- Backend stores rows in `ContributorPayments`.
- Contributors view the stored payment history.
