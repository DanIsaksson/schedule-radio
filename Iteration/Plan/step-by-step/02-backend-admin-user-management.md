# Step-by-step 02 – Backend admin user management

## A. Goal
Create admin-only endpoints so that:
- only Admins can create accounts (contributors/admins)
- new users get a temporary password
- new users are forced to change password on first login

## A.1 Status (implementation + verification)

Legend:
- **Implemented** = code exists in the repo
- **Verified** = you have tested it manually (curl/Swagger)

| Item | Implemented | Verified |
| --- | --- | --- |
| Block public registration (`POST /api/auth/register`) | Yes | No |
| Admin-only policy exists (`AdminOnly`) | Yes | Yes (admin can access) |
| Admin route group exists (`/api/admin`) | Yes | Yes (admin can access) |
| Non-admin is blocked from `/api/admin/*` | Yes | No |
| Create user (`POST /api/admin/users`) returns temp password once | Yes | Yes |
| List users (`GET /api/admin/users`) returns safe fields only | Yes | Yes |
| Delete user (`DELETE /api/admin/users/{userId}`) | Yes | Yes |
| Prevent self-delete | Yes | Yes |

## B. Files we will touch (planned)
- `API/Program.cs` (or a new endpoints file under `API/Endpoints/`)
- New: request/response DTOs (optional)

### B.1 Related plan documents
 - `../01-requirements.md`
   - Confirms admin-only account creation and protected admin management endpoints.
 - `../02-auth-roles-security.md`
   - Explains why `/api/auth/register` must be blocked and how roles should be enforced.
 - `../03-database-schema.md`
   - Defines `MustChangePassword` and other `ApplicationUser` fields this step relies on.
 - `../04-api-contract.md`
   - Specifies the `/api/admin/users` endpoints and the expected request/response shapes.
 - `../05-frontend-pages.md`
   - Shows how the frontend will react to `MustChangePassword` (forced password change flow).
 - `../07-decision-log.md`
   - Captures the reasoning behind admin-only provisioning, temp passwords, and forced password change.
 - `../08-codebase-map-and-gotchas.md`
   - Notes the practical implementation hotspots and pitfalls (e.g., blocking `/register`).
 - `../09-definition-of-done-test-checklist.md`
   - Verification checklist for admin provisioning and forced password change.

## C. Step-by-step
### C.1 Block public registration
Requirement: no public account creation.

Plan:
- Keep Identity endpoints for login/etc.
- Add a filter/middleware so calls to:
  - `POST /api/auth/register`
  are rejected (return `404` or `403`).

### C.2 Create admin route group
Create a route group:
- `/api/admin`

Protect it:
- `RequireAuthorization()` + role requirement `Admin`

### C.3 Admin creates user endpoint
Endpoint:
- `POST /api/admin/users`

Input fields:
- `email`
- `role` (`Contributor` or `Admin`)
- `address`
- `phone`
- optional: `bio`, `photoUrl`

Backend steps:
- Generate temporary password securely
  - use `RandomNumberGenerator`
- Create `ApplicationUser`
  - set `UserName = email`
  - set fields
  - set `MustChangePassword = true`
- `UserManager.CreateAsync(user, tempPassword)`
- `UserManager.AddToRoleAsync(user, role)`

Return:
- created user id + email + role
- the temporary password (only once)

### C.4 Admin lists users endpoint
Endpoint:
- `GET /api/admin/users`

Optional filter:
- `?role=Contributor|Admin`

Return:
- list of users (no sensitive fields)

### C.5 Admin deletes user endpoint
Endpoint:
- `DELETE /api/admin/users/{userId}`

Rules:
- Admin can delete contributors and admins.
- (Optional safety) prevent deleting yourself.

## D. Verification checklist
- Calling `/api/auth/register` fails (blocked)
- Logged-in admin can create a contributor and receives a temp password
- Admin can list users and see:
  - the new contributor
  - `mustChangePassword = true`
- New contributor can log in with temp password
- Contributor has `MustChangePassword = true`
