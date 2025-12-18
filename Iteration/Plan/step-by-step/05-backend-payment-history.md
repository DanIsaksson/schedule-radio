# Step-by-step 05 – Backend payment history

## A. Goal
Implement monthly payments:
- calculate payout for previous month
- store payment history
- expose payment history to contributor

## A.1 Status (implementation + verification)

Legend:
- **Implemented** = code exists in the repo
- **Verified** = you have tested it manually (curl/Swagger)

| Item | Implemented | Verified |
| --- | --- | --- |
| `ContributorPaymentEntity` exists + `DbSet<ContributorPaymentEntity>` exists | Yes | Yes |
| DB migration exists and adds `ContributorPayments` table + unique index (year/month/user) | Yes | Yes |
| `PaymentCalculator` merges contiguous rows and calculates totals (base + bonus + VAT) | Yes | Yes |
| `POST /api/admin/payments/calculate-previous-month` exists (Admin only) and stores rows | Yes | Yes |
| `GET /api/contributor/payments` returns the current user’s payment history rows | Yes | Yes |

## B. Files we will touch (planned)
- New: `API/Models/ContributorPaymentEntity.cs`
- `API/Data/SchedulerContext.cs`
- New: `API/Services/PaymentCalculator.cs` (or similar)
- New: endpoints under `API/Endpoints/`

### B.1 Related plan documents
 - `../01-requirements.md`
   - Defines the flat rates, the “previous month” rule, and the VAT requirement.
 - `../02-auth-roles-security.md`
   - Admin-only endpoints are required for triggering calculations.
 - `../03-database-schema.md`
   - Defines the `ContributorPayments` table and the recommended unique key (user + month).
 - `../04-api-contract.md`
   - Specifies `/api/admin/payments/calculate-previous-month` and `/api/contributor/payments`.
 - `../06-payment-calculation.md`
   - The detailed merge-and-calculate algorithm used by `PaymentCalculator`.
 - `../07-decision-log.md`
   - Captures the strict merge identity fields we must use when counting “scheduled events”.
 - `../08-codebase-map-and-gotchas.md`
   - Notes where payment logic should live (service layer) and key repo pitfalls.
 - `../09-definition-of-done-test-checklist.md`
   - Verification checklist for payment calculation correctness (including VAT).

## C. Step-by-step
### C.1 Create payment entity + DbSet
Create a `ContributorPaymentEntity` table with:
- user id
- year/month
- totals + amounts + VAT

Add:
- `DbSet<ContributorPaymentEntity> ContributorPayments`

Create migration and update DB.

### C.2 Implement calculator service
Inputs:
- userId
- year/month
- list of events for that period

Outputs:
- total minutes
- merged event count
- base amount, bonus, VAT, total

Important: merge contiguous hour-rows into logical events (see `../06-payment-calculation.md`).

### C.3 Admin endpoint: calculate previous month
Endpoint:
- `POST /api/admin/payments/calculate-previous-month`

Behavior:
- find all users in role `Contributor`
- compute payout for each contributor
- upsert into `ContributorPayments` (one row per contributor per month)

### C.4 Contributor endpoint: read own payments
Endpoint:
- `GET /api/contributor/payments`

Behavior:
- returns history rows for current user

## D. Verification checklist
- Admin triggers calculation => rows appear in DB
- Contributor sees their own payment history
- VAT is 25% and totals match sample calculations
