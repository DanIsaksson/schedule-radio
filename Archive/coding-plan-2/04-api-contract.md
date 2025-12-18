# API contract (planned endpoints)

## A. Route groups
- `/api/auth/*` – authentication
- `/api/admin/*` – admin-only user + finance management
- `/api/contributor/*` – contributor authenticated endpoints
- `/db/*` – existing schedule + event endpoints (we will secure parts of this)

## B. Auth endpoints (`/api/auth`)
Provided by Identity API endpoints (cookie mode):
- `POST /api/auth/login?useCookies=true`
- `POST /api/auth/logout` (custom, we add)

Blocked/disabled:
- `POST /api/auth/register`

Planned custom endpoints:
- `POST /api/auth/change-password`
  - Requires login
  - Body: `{ oldPassword, newPassword }`
  - On success: sets `MustChangePassword=false`

## C. Contributor endpoints (`/api/contributor`)
- `GET /api/contributor/me`
  - Requires login
  - Returns contributor personal data and payment history
  - Includes `mustChangePassword` so UI can enforce first-login flow

- `GET /api/contributor/payments`
  - Requires login
  - Returns payment history list (or filtered by year/month)

## D. Admin endpoints (`/api/admin`) – role: Admin only
### D.1 User management
- `GET /api/admin/users`
  - Query params: `role=Contributor|Admin` (optional)

- `POST /api/admin/users`
  - Creates Contributor or Admin
  - Generates a temporary password and returns it once
  - Body (example):
    - `email`
    - `role` (`Contributor` or `Admin`)
    - `address`
    - `phone`
    - `bio` (optional)
    - `photoUrl` (optional)

- `DELETE /api/admin/users/{userId}`
  - Deletes user

### D.2 Payments (admin triggers calculation)
- `POST /api/admin/payments/calculate-previous-month`
  - Calculates and stores payment rows for all contributors

## E. Event endpoints (existing `/db/event/*` but secured)
We will gradually secure and extend these.

### E.1 Create event
- Existing: `POST /db/event/post`
- Planned behavior change:
  - Requires login (`Contributor` or `Admin`)
  - Sets `ResponsibleUserId` to the logged-in user by default
  - Admin may optionally assign to another contributor (either via separate assign endpoint or an admin-only parameter)

### E.2 Reschedule / delete
- Existing:
  - `POST /db/event/{eventId}/reschedule`
  - `POST /db/event/{eventId}/delete`
- Planned behavior change:
  - Requires login
  - Contributor can only edit/delete events they own (`ResponsibleUserId == currentUserId`)
  - Admin can edit/delete any event

### E.3 Assign responsibility (Admin)
Option A (recommended):
- `POST /db/event/{eventId}/assign?userId=...`

Option B:
- Add `responsibleUserId` param to create/reschedule (admin-only)

## F. Schedule endpoints
- `/db/schedule/today` and `/db/schedule/7days` remain public (read-only schedule view).
