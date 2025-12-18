# Frontend pages (planned)

## A. Routing strategy
- Keep the existing hash routing approach.
- Add new routes for auth and contributor pages.

## B. Pages
### B.1 Public pages (already exist)
- `#/` Home
- `#/schedule` Full schedule

### B.2 Auth pages (new)
- `#/login`
  - Email + password
  - Calls `POST /api/auth/login?useCookies=true`
  - Uses `credentials: 'include'`

- `#/change-password`
  - Forced on first login when `MustChangePassword=true`
  - Calls `POST /api/auth/change-password`

### B.3 Contributor pages (new)
- `#/me` (My Contributor Page)
  - Shows:
    - Address
    - Phone
    - Email
    - Optional: bio/photo
    - Payment history list

### B.4 Admin pages (optional UI, backend is required)
- `#/admin/users`
  - List contributors/admins
  - Create user form
  - Show generated temp password once
  - Delete user

## C. Frontend state architecture
- Add an `AuthContext` (global state):
  - `user` (profile)
  - `roles`
  - `mustChangePassword`
  - `login()`, `logout()`, `refreshMe()`

## D. UX rules
- If not logged in:
  - redirect to `#/login` for `#/me`, `#/admin/*`
- If logged in but `mustChangePassword`:
  - redirect to `#/change-password`
- Admin navigation only visible if role is Admin.

## E. Vite proxy
- Extend `vite.config.js` to also proxy `/api` to the backend.

## F. Minimal done (assignment requirement)
- Login + My Contributor page.
- Password change flow (first login).
