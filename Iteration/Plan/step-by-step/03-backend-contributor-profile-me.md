# Step-by-step 03 – Backend contributor profile ("me")

## A. Goal
Add authenticated contributor endpoints:
- contributor can fetch their own profile data
- contributor can fetch payment history
- contributor must change password on first login (forced)

## A.1 Status (implementation + verification)

Legend:
- **Implemented** = code exists in the repo
- **Verified** = you have tested it manually (curl/Swagger)

| Item | Implemented | Verified |
| --- | --- | --- |
| Contributor route group exists (`/api/contributor`) and requires login | Yes | No |
| `GET /api/contributor/me` returns profile + roles + `mustChangePassword` | Yes | Yes |
| `POST /api/auth/change-password` exists and sets `MustChangePassword=false` on success | Yes | Yes |
| While `mustChangePassword=true`, other contributor endpoints return 403 | Yes | Yes |
| `GET /api/contributor/payments` exists (placeholder until Step 05) | Yes | Yes |
| After password change, `GET /api/contributor/payments` returns 200 and `[]` | Yes | Yes |

## B. Files we will touch (planned)
- New endpoint file under `API/Endpoints/` (recommended)
- `API/Program.cs`
- `API/Models/ApplicationUser.cs`

### B.1 Related plan documents
 - `../01-requirements.md`
   - Defines the contributor personal data we must return and the minimum frontend contributor page requirement.
 - `../02-auth-roles-security.md`
   - Explains roles and the forced password change rule (`MustChangePassword`).
 - `../03-database-schema.md`
   - Details the `ApplicationUser` fields (`Address`, `Bio`, `PhotoUrl`, `MustChangePassword`) used by `/me`.
 - `../04-api-contract.md`
   - Specifies `/api/contributor/me`, `/api/contributor/payments`, and `/api/auth/change-password`.
 - `../05-frontend-pages.md`
   - Shows how routes like `#/me` and `#/change-password` depend on these endpoints.
 - `../06-payment-calculation.md`
   - Explains what payment history rows represent (and why the user can’t calculate them from raw events in the UI).
 - `../07-decision-log.md`
   - Explains the rationale behind forced password change and why we store profile fields on `ApplicationUser`.
 - `../08-codebase-map-and-gotchas.md`
   - Shows where contributor endpoints and auth endpoints should live in the repo.
 - `../09-definition-of-done-test-checklist.md`
   - Verification checklist for `/me`, forced password change, and payments visibility.

## C. Step-by-step
### C.1 Create contributor route group
Create route group:
- `/api/contributor`

Protect it:
- Require login
- Allow both roles (`Contributor`, `Admin`) because Admin has all contributor powers

### C.2 Add `GET /api/contributor/me`
Return:
- `email`
- `phone`
- `address`
- optional: `bio`, `photoUrl`
- `roles` (array)
- `mustChangePassword`

### C.3 Add change password endpoint
Create endpoint:
- `POST /api/auth/change-password`

Rules:
- Requires login
- Body: `{ oldPassword, newPassword }`
- On success:
  - set `MustChangePassword = false`

### C.4 Enforce forced password change (backend)
Frontend redirect is good UX, but backend should also enforce.

Plan (choose one):
- Option A: Add an endpoint filter on `/api/contributor/*` that checks `MustChangePassword` and returns `403`, except for `/api/auth/change-password`.
- Option B: Create a custom authorization policy `MustNotRequirePasswordChange`.

### C.5 Payment history endpoints
- `GET /api/contributor/payments`
  - returns stored payment history rows for the logged-in user

## D. Verification checklist
- Not logged in => `GET /api/contributor/me` returns 401
- Logged in contributor => returns profile and `mustChangePassword`
- While `mustChangePassword=true`, other contributor endpoints return 403
- After password change, `mustChangePassword=false` and access is restored
