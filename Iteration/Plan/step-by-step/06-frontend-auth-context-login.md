# Step-by-step 06 – Frontend auth context + login

## A. Goal
Add frontend authentication support:
- login form
- store session (cookie)
- global auth state in React
- route guards for contributor/admin pages

## A.1 Status (implementation + verification)

Legend:
- **Implemented** = code exists in the repo
- **Verified** = you have tested it manually (browser + Swagger)

| Item | Implemented | Verified |
| --- | --- | --- |
| Vite proxy forwards `/api` to the backend | Yes | Yes |
| Vite dev server runs on `http://localhost:5174` (port change due to local port collision) | Yes | Yes |
| `AuthProvider` wraps the app so auth state is global | Yes | Yes |
| `AuthContext` supports `login`, `logout`, and `refreshMe` using cookie auth (`credentials: 'include'`) | Yes | Yes |
| Staff portal routes exist: `#/portal`, `#/portal/me`, `#/portal/change-password` | Yes | Yes |
| Route guards: logged out users are redirected from staff routes to `#/portal` | Yes | Yes |
| Forced password change flow: when `mustChangePassword=true` user is redirected to `#/portal/change-password` | Yes | Yes |
| Booking UI is protected under `#/bookings` and `#/admin` redirects to `#/bookings` | Yes | Yes |
| Logout clears session and redirects to `#/portal` | Yes | Yes |

## B. Files we will touch (planned)
- `frontend/vite.config.js` (proxy)
- `frontend/src/App.jsx` (routes + pages)
- New: `frontend/src/auth/AuthContext.jsx` (recommended)

### B.1 Related plan documents
 - `../02-auth-roles-security.md`
   - Cookie auth rules, CORS/proxy notes, and the forced password change flow concept.
 - `../04-api-contract.md`
   - The exact `/api/auth/*` and `/api/contributor/me` endpoints the frontend must call.
 - `../05-frontend-pages.md`
   - Defines the routes (`#/login`, `#/change-password`, `#/me`) and the route guard rules.
 - `../07-decision-log.md`
   - Captures why we use cookies (not JWT) and why `/api/auth/register` is blocked.
 - `../08-codebase-map-and-gotchas.md`
   - Highlights the Vite proxy `/api` requirement and cookie credential gotchas.
 - `../09-definition-of-done-test-checklist.md`
   - Verification checklist for login/session persistence/logout.

## C. Step-by-step
### C.1 Extend the Vite proxy
Add proxy for `/api` => backend (same as `/db`).

### C.2 Create `AuthContext`
State:
- `user`
- `roles`
- `mustChangePassword`
- `isLoading`

Functions:
- `login(email, password)`
- `logout()`
- `refreshMe()` (calls `GET /api/contributor/me`)

Important:
- All requests must use cookies:
  - `credentials: 'include'`

### C.3 Add login page route
Route:
- `#/portal`

Form:
- email + password

Request:
- `POST /api/auth/login?useCookies=true`

On success:
- call `refreshMe()`
- redirect:
  - if `mustChangePassword` => `#/portal/change-password`
  - else => `#/bookings`

### C.4 Add logout
Call:
- `POST /api/auth/logout`
Then clear auth state and redirect to public home.

### C.5 Route guards
Rules:
- If not logged in => staff routes redirect to `#/portal`
- If `mustChangePassword=true` => redirect to `#/portal/change-password`

## D. Verification checklist
- Login sets cookie
- Refresh page keeps session
- Logout clears cookie (server instructs browser)
