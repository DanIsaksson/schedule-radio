# Step-by-step 07 – Frontend contributor page

## A. Goal
Meet the assignment’s frontend minimum:
- a personal contributor page showing current data
- payment history list
- forced password change flow

## A.1 Status (implementation + verification)

Legend:
- **Implemented** = code exists in the repo
- **Verified** = you have tested it manually (browser + Swagger)

| Item | Implemented | Verified |
| --- | --- | --- |
| `#/portal/me` shows contributor profile fields (email/phone/address/bio/photo) | Yes | Yes |
| Payment history is loaded from `GET /api/contributor/payments` and rendered in a table | Yes | Yes |
| Forced password change is enforced before accessing the rest of the staff portal | Yes | Yes |

## B. Files we will touch (planned)
- `frontend/src/App.jsx`
- (Optional) `frontend/src/components/*`

### B.1 Related plan documents
 - `../01-requirements.md`
   - Defines what personal contributor data and payment history must be shown.
 - `../04-api-contract.md`
   - Defines the `/api/contributor/me`, `/api/contributor/payments`, and `/api/auth/change-password` calls.
 - `../05-frontend-pages.md`
   - Defines the routes (`#/portal/me`, `#/portal/change-password`) and the route guard rules.
 - `../06-payment-calculation.md`
   - Explains what the numbers in the payment history mean (hours, event count, VAT, totals).
 - `../07-decision-log.md`
   - Captures the reasoning behind the forced password change UX and what data must be surfaced.
 - `../08-codebase-map-and-gotchas.md`
   - Shows where routing/auth state currently lives in the repo (`App.jsx`).
 - `../09-definition-of-done-test-checklist.md`
   - Frontend acceptance checklist for `#/portal`, `#/portal/change-password`, and `#/portal/me`.

## C. Step-by-step
### C.1 My Contributor page
Route:
- `#/portal/me`

Data source:
- `GET /api/contributor/me`

Show:
- Address
- Phone
- Email
- Optional: Bio + Photo

### C.2 Payment history section
Data source:
- `GET /api/contributor/payments`

Display:
- year/month
- hours/minutes
- event count
- base + bonus
- VAT
- total

### C.3 Change password page
Route:
- `#/portal/change-password`

Request:
- `POST /api/auth/change-password`

UX:
- Only shown when `mustChangePassword=true`
- After successful change:
  - refresh `/api/contributor/me`
  - redirect to `#/portal/me`

### C.4 Optional: Admin UI
If we build it:
- route `#/admin/users`
- list users
- create user (show temp password once)
- delete user

## D. Verification checklist
- Contributor can log in and view their profile at `#/portal/me`
- Contributor sees payment history
- First login forces password change before using the rest of the site
