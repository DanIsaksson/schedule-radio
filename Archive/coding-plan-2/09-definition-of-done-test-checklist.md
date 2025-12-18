# Definition of Done + manual test checklist

This is the "finish line" checklist so implementation can be verified without guessing.

## A. Build / run sanity
- `dotnet build` succeeds for the API.
- Frontend starts and can call the backend via the Vite proxy.

## B. Authentication (cookie session)
- **B.1 Login works**
  - `POST /api/auth/login?useCookies=true` returns success.
  - Browser receives an auth cookie.
- **B.2 Session persists**
  - Refresh the page; session still valid.
- **B.3 Logout works**
  - `POST /api/auth/logout` clears cookie.

## C. Registration is blocked (admin-only)
- Calling `POST /api/auth/register` fails (404/403).

## D. Seeded admin user
- Admin user exists:
  - `tobias@test.se` / `supersafe123`
- Admin can log in.

## E. Admin user management
- **E.1 Create contributor**
  - Logged-in admin can call `POST /api/admin/users`.
  - Response includes a temporary password.
- **E.2 Temp password is one-time**
  - If the admin refreshes/reloads later, password is not re-readable from the system.
- **E.3 Delete user**
  - Admin can delete a contributor.

## F. Forced password change
- New contributor logs in with temp password.
- `GET /api/contributor/me` returns `mustChangePassword=true`.
- While `mustChangePassword=true`:
  - Contributor endpoints (except change password) return `403`.
- Contributor changes password via `POST /api/auth/change-password`.
- After change:
  - `mustChangePassword=false`
  - Contributor endpoints work normally.

## G. Event responsibility & security
- Creating an event as contributor sets `ResponsibleUserId` to that user.
- Contributor cannot edit/delete another user’s event (403).
- Admin can edit/delete any event.

## H. Existing events backfill
- After the migration to add `ResponsibleUserId`, old events are assigned to the seeded admin.
- Long-term: `ResponsibleUserId` is NOT NULL.

## I. Payments
- Admin triggers `POST /api/admin/payments/calculate-previous-month`.
- DB contains one payment row per contributor for that month.
- Calculations match the rules:
  - Base: `750 SEK/hour`
  - Bonus: `+300 SEK per scheduled event` (merged blocks)
  - VAT: `25%` of subtotal

## J. Frontend minimum (assignment)
- `#/login` exists and can log in.
- `#/change-password` exists and is enforced on first login.
- `#/me` exists and shows:
  - address/phone/email
  - payment history
