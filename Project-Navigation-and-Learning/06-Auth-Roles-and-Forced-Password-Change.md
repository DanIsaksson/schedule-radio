recap - How login (cookies), roles (Admin/Contributor), and the MustChangePassword rule work end-to-end.

# 06) Auth, Roles, and Forced Password-Change

## A) Authentication vs Authorization (beginner definitions)

### Authentication = “Who are you?”

In this project, authentication is done using **cookies**.

- After you log in, the backend sends a cookie.
- The browser stores it.
- On later requests, the browser sends the cookie back.

### Authorization = “What are you allowed to do?”

Authorization decides if the logged-in user can access an endpoint.

This project uses:

- **Roles**: `Admin`, `Contributor`
- A policy called `AdminOnly`

## B) Where Identity is configured (backend)

Identity (the login system) is configured in `API/Program.cs`.

### B1) Add Identity services (DI)

- **File**: `API/Program.cs`
- **Line(s)**: `74-88`
- **Code**:
  - `.AddIdentityApiEndpoints<ApplicationUser>(...)`
  - `.AddRoles<IdentityRole>()`
  - `.AddEntityFrameworkStores<SchedulerContext>()`

### B2) Map Identity endpoints under `/api/auth`

- **File**: `API/Program.cs`
- **Line(s)**: `217-220`
- **Code**:
  - `var authGroup = app.MapGroup("/api/auth");`
  - `authGroup.MapIdentityApi<ApplicationUser>();`

This creates built-in “doors” like:

- `POST /api/auth/login?useCookies=true`

## C) The cookie login flow (frontend → backend)

### C1) Frontend sends login request

- **File**: `frontend/src/auth/AuthContext.jsx`
- **Line(s)**: `94-102`
- **Code**:
  - `fetch('/api/auth/login?useCookies=true', { ..., credentials: 'include' })`

Beginner note:

- `credentials: 'include'` means: “allow cookies to be sent/received”.

### C2) Backend issues a cookie

This is handled by the Identity API endpoints that were mapped in Program.cs:

- **File**: `API/Program.cs`
- **Line(s)**: `217-220`

### C3) Frontend loads profile + roles using `/api/contributor/me`

After login, the UI calls `/api/contributor/me` to learn:

- email
- roles
- MustChangePassword

- **File**: `frontend/src/auth/AuthContext.jsx`
- **Line(s)**: `57-83`
- **Code**:
  - `fetch('/api/contributor/me', { credentials: 'include' })`

Backend implementation:

- **File**: `API/Endpoints/ContributorEndpoints.cs`
- **Line(s)**: `35-55`
- **Code**:
  - `group.MapGet("/me", async (...) => { ... })`

## D) CORS + cookies (“The Bouncer”)

Because the frontend runs on port 5174 and backend on 5219, browsers can block cookies unless CORS is correct.

### D1) Backend allows the frontend origin and allows credentials

- **File**: `API/Program.cs`
- **Line(s)**: `45-56`
- **Code**:
  - `.WithOrigins("http://localhost:5174", ...)`
  - `.AllowCredentials()`

### D2) Vite dev proxy forwards `/api/*` and `/db/*`

- **File**: `frontend/vite.config.js`
- **Line(s)**: `20-32`
- **Code**:
  - `'/api': { target: 'http://localhost:5219', changeOrigin: true }`
  - `'/db':  { target: 'http://localhost:5219', changeOrigin: true }`

## E) Roles and Admin-only authorization

### E1) The AdminOnly policy

- **File**: `API/Program.cs`
- **Line(s)**: `70-73`
- **Code**:
  - `options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));`

### E3) Admin-only UI route: “Hantera konton”

The frontend has an Admin-only page:

- `#/portal/admin/users`

Beginner note:

- The backend enforces Admin access on `/api/admin/*`.
- The frontend also shows an error message if a non-admin tries to open the page.

### E2) Admin endpoints require AdminOnly

Example: Admin user management endpoints.

- **File**: `API/Endpoints/AdminUserEndpoints.cs`
- **Line(s)**: `21-23`
- **Code**:
  - `.MapGroup("/api/admin").RequireAuthorization("AdminOnly")`

Example: Payment calculation endpoint.

- **File**: `API/Endpoints/AdminPaymentsEndpoints.cs`
- **Line(s)**: `19-21`
- **Code**:
  - `.MapGroup("/api/admin").RequireAuthorization("AdminOnly")`

## F) Forced password-change (MustChangePassword)

This project has a business/security rule:

- Admin-created users start with a temporary password.
- They must change it before doing “real work” (like bookings).

### F1) Where MustChangePassword is stored

It is stored on the user in the Identity table `AspNetUsers`.

- **File**: `API/Models/ApplicationUser.cs`
- **Line(s)**: `29-31`
- **Code**:
  - `public bool MustChangePassword { get; set; }`

### F2) Admin-created accounts start with MustChangePassword=true

- **File**: `API/Endpoints/AdminUserEndpoints.cs`
- **Line(s)**: `73-85`
- **Code**:
  - `MustChangePassword = true,`

### F3) The backend enforces the rule (Contributor endpoints)

Contributor endpoints attach an endpoint filter.

- **File**: `API/Endpoints/ContributorEndpoints.cs`
- **Line(s)**: `24-29`
- **Code**:
  - `group.AddEndpointFilter<MustChangePasswordEnforcementFilter>();`

Important exception:

- `/api/contributor/me` is allowed even when MustChangePassword is true.
- Why: the frontend must be able to *read* the flag.

- **File**: `API/Endpoints/ContributorEndpoints.cs`
- **Line(s)**: `95-100`
- **Code**:
  - `if (path == "/api/contributor/me") { return await next(context); }`

If MustChangePassword is true, the filter returns **403**.

- **File**: `API/Endpoints/ContributorEndpoints.cs`
- **Line(s)**: `109-113`
- **Code**:
  - `return Results.StatusCode(StatusCodes.Status403Forbidden);`

### F4) The backend enforces the rule (Booking endpoints)

The booking endpoints add a similar filter.

- **File**: `API/Endpoints/EventDbEndpoints.cs`
- **Line(s)**: `88-90`
- **Code**:
  - `.AddEndpointFilter<MustChangePasswordEnforcementFilter>();`

Filter logic:

- **File**: `API/Endpoints/EventDbEndpoints.cs`
- **Line(s)**: `178-182`
- **Code**:
  - `if (user.MustChangePassword) return 403;`

### F5) The frontend enforces the rule (route guards)

The frontend redirects to the change-password route if you are logged in but blocked.

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1386-1412`
- **Code**:
  - `if (isLoggedIn && mustChangePassword && !isPortalChangePassword) { window.location.hash = '#/portal/change-password' }`

The booking submit handler also handles `403` and redirects:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1490-1494`
- **Code**:
  - `if (mustChangePassword) { window.location.hash = '#/portal/change-password' }`

## G) Changing password (the “unlock” step)

### G1) Backend: change-password “door”

- **File**: `API/Program.cs`
- **Line(s)**: `242-281`
- **Code**:
  - `authGroup.MapPost("/change-password", async (...) => { ... })`

Important: it sets MustChangePassword to false.

- **File**: `API/Program.cs`
- **Line(s)**: `261-270`
- **Code**:
  - `user.MustChangePassword = false;`
  - `await signInManager.RefreshSignInAsync(user);`

### G2) Frontend: calls change-password

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1547-1557`
- **Code**:
  - `fetch('/api/auth/change-password', { method: 'POST', body: JSON.stringify({ OldPassword, NewPassword }), credentials: 'include' })`

Beginner note:

- The JSON properties are PascalCase (`OldPassword`, `NewPassword`) because the backend record uses PascalCase.

- **File**: `API/Program.cs`
- **Line(s)**: `306-308`
- **Code**:
  - `public sealed record ChangePasswordRequest(string? OldPassword, string? NewPassword);`

## H) Logout (a small but important detail)

### H1) Backend logout “door” requires a body (even if empty)

The backend checks `empty is null`.

- **File**: `API/Program.cs`
- **Line(s)**: `222-227`
- **Code**:
  - `if (empty is null) { return Results.Unauthorized(); }`

### H2) Frontend sends `{}` as the body

- **File**: `frontend/src/auth/AuthContext.jsx`
- **Line(s)**: `117-124`
- **Code**:
  - `body: '{}'`

## I) 401 vs 403 vs 404 (how to read API errors)

- **401 Unauthorized**: “You are not logged in.”
  - Example: booking endpoint returns Unauthorized when userId is missing.
  - **File**: `API/Endpoints/EventDbEndpoints.cs`
  - **Line(s)**: `57-61`

- **403 Forbidden**: “You are logged in, but you are not allowed.”
  - Example: MustChangePassword blocks.
  - **File**: `API/Endpoints/ContributorEndpoints.cs`
  - **Line(s)**: `109-113`

- **404 Not Found**: “That door doesn’t exist.”
  - Example: public registration is deliberately blocked.
  - **File**: `API/Program.cs`
  - **Line(s)**: `188-194`

## Next step

Continue to:

- `07-Frontend-Bootstrap-Routing-and-Data-Fetching.md`
