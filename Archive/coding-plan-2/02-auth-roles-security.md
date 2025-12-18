# Auth, Roles & Security

## A. Authentication approach
- **A.1 Technology choice**
  - Use **ASP.NET Core Identity** with the .NET 8 **Identity API endpoints**.
  - Use **cookies** (SPA-friendly for React when using a Vite proxy).
- **A.2 Analogy (beginner-friendly)**
  - Endpoints are **"doors"**: if a request knocks on a URL, the backend applies the rule behind that door.
  - CORS is **"the bouncer"**: the browser refuses to talk to the backend unless the bouncer allows it.

## B. Authorization (roles)
- **B.1 Roles**
  - `Admin`
  - `Contributor`
- **B.2 Access rules**
  - Admin:
    - Can do everything a contributor can
    - Can manage users (create/delete)
  - Contributor:
    - Can only access contributor endpoints
    - Can only manage events they are responsible for

## C. Admin-only account creation (disable public register)
- **C.1 Requirement**
  - We do **not** want the public `POST /register` endpoint to be usable.
- **C.2 Plan**
  - Map Identity API endpoints under `/api/auth`.
  - Add a filter/middleware that **blocks requests to `/api/auth/register`**.
  - Admin user provisioning is done through our own endpoints under `/api/admin/*`.

## D. Temporary password + forced change
- **D.1 Plan**
  - Add `MustChangePassword` boolean on `ApplicationUser`.
  - When admin creates a user:
    - Generate random temp password
    - Set `MustChangePassword = true`
  - On first login:
    - Frontend checks `MustChangePassword` from `/api/contributor/me`
    - If true:
      - redirect user to `#/change-password`
      - hide/block other contributor UI until changed
- **D.2 Backend enforcement (recommended)**
  - Add a policy/filter so that if `MustChangePassword == true`, contributor endpoints return `403` except the password-change endpoint.

## E. Seeded default admin
- **E.1 Seed**
  - Create roles (`Admin`, `Contributor`) if missing.
  - Create default admin if missing:
    - `tobias@test.se` / `supersafe123`
  - Assign role `Admin`.

## F. CORS + Vite proxy
- **F.1 Dev approach**
  - Use the Vite proxy so frontend calls look same-origin.
  - Frontend must send cookies:
    - `fetch(..., { credentials: 'include' })`
- **F.2 If direct cross-origin is needed**
  - Configure a CORS policy:
    - Allow `http://localhost:5173`
    - Allow credentials
    - Restrict methods/headers as needed

## G. Middleware order (important)
- **G.1 Minimal API pipeline**
  - `UseRouting`
  - `UseCors`
  - `UseAuthentication`
  - `UseAuthorization`

## H. Security checklist
- Password policy (minimum length)
- Protect `/api/admin/*` with `RequireAuthorization("Admin")`
- Never return hashed passwords
- Temporary password is returned only once (to the admin who created it)
