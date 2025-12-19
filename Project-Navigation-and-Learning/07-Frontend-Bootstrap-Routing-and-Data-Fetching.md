recap - Learn how the frontend starts (index.html → main.jsx), how hash routing works, and how React fetches data from the backend.

# 07) Frontend Bootstrap, Routing, and Data Fetching (React)

## A) The frontend entry point (HTML → JS)

### A1) `index.html` loads the React app

- **File**: `frontend/index.html`
- **Line(s)**: `14-17`
- **Code**:
  - `<div id="root"></div>`
  - `<script type="module" src="/src/main.jsx"></script>`

Beginner note:

- The `<div id="root">` is an “empty container”. React takes over and renders inside it.

### A2) Why `type="module"` matters

`type="module"` means:

- the browser supports ES modules (`import ... from ...`)
- Vite can load and bundle your React files

## B) `main.jsx`: mount React into the DOM

- **File**: `frontend/src/main.jsx`

### B1) createRoot + render

- **Line(s)**: `46-52`
- **Code**:
  - `createRoot(document.getElementById('root')).render(...)`

This is the React equivalent of:

- “Take control of #root, and start rendering my app there.”

### B2) AuthProvider wraps the app

- **File**: `frontend/src/main.jsx`
- **Line(s)**: `47-50`
- **Code**:
  - `<AuthProvider> ... </AuthProvider>`

Why:

- It makes auth state available everywhere using `useAuth()`.

Auth context lives here:

- **File**: `frontend/src/auth/AuthContext.jsx`
- **Line(s)**: `33-37`
- **Code**:
  - `const [user, setUser] = useState(null)`

## C) Hash routing (no react-router)

This project uses hash routing:

- `#/` (home)
- `#/schedule`
- `#/portal`
- `#/bookings`
- `#/portal/me`
- `#/portal/change-password`
- `#/portal/admin/users` (Admin only)

### C1) Router “memory” uses useState

- **File**: `frontend/src/main.jsx`
- **Line(s)**: `25`
- **Code**:
  - `const [route, setRoute] = React.useState(window.location.hash || '#/')`

Analogy:

- `useState` is the component’s **Memory**.

### C2) Router listens to `hashchange` using useEffect

- **File**: `frontend/src/main.jsx`
- **Line(s)**: `30-34`
- **Code**:
  - `window.addEventListener('hashchange', onHashChange)`

Analogy:

- `useEffect` is a **Side Effect**: it listens to the outside world (browser events).

### C3) Router passes the route into App.jsx

- **File**: `frontend/src/main.jsx`
- **Line(s)**: `36-38`
- **Code**:
  - `return <App route={route} />`

## D) App.jsx: the “main controller-view”

`App.jsx` is the big file that:

- holds main state
- fetches schedule data
- renders different “pages” based on route

### D1) State (“Memory”) Example (schedule memory):

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1067-1071`
- **Code**:
  - `const [today, setToday] = useState(null)`
  - `const [week, setWeek] = useState(null)`

### D2) Side effects: loading schedule data

Load week schedule:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1232-1239`
- **Code**:
  - `const res = await fetch('/db/schedule/7days')`
  - `setWeek(data)`

Auto-load once on startup:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1357-1359`
- **Code**:
  - `useEffect(() => { loadWeek() }, [])`

Beginner note:

- `[]` means “run only once” (like a constructor).

### D3) Fetching one selected day (used by booking UI)

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1171-1174`
- **Code**:
  - `fetch(`/db/schedule/day?date=${encodeURIComponent(dateIso)}`)`

### D4) Payments fetching requires credentials (cookie)

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1309-1311`
- **Code**:
  - `fetch('/api/contributor/payments', { credentials: 'include' })`

## E) How the frontend talks to the backend (Vite proxy)

### E1) Dev ports

- Frontend (Vite): 5174
  - **File**: `frontend/vite.config.js`
  - **Line(s)**: `14`

- Backend (ASP.NET): 5219
  - **File**: `API/Properties/launchSettings.json`
  - **Line(s)**: `8`

### E2) Proxy rules

- **File**: `frontend/vite.config.js`
- **Line(s)**: `20-36`
- **Code**:
  - `/db → http://localhost:5219`
  - `/api → http://localhost:5219`
  - `/images → http://localhost:5219`

Beginner note:

- Because of the proxy, the frontend can call `fetch('/db/...')` without hardcoding the backend URL.

## F) Route guards (protect staff routes)

The app blocks staff routes when:

- you are not logged in
- or you must change password

Concrete reference:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1391-1407`
- **Code**:
  - `if (!isLoggedIn && isProtectedStaffRoute) window.location.hash = '#/portal'`

And the MustChangePassword redirect:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1399-1401`

## G) Controlled forms (booking form)

A controlled form means:

- The input value comes from React state (`form`).
- `onChange` updates `form`.

Concrete reference:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1439-1446`
- **Code**:
  - `setForm(currentForm => ({ ...currentForm, [name]: ... }))`

## H) Debugging tips (very practical)

### H1) “Frontend says HTTP 404/500”

- Check backend is running.
- Check backend port is 5219.
  - **File**: `API/Properties/launchSettings.json:8`

### H2) “Login works, but requests act like I’m logged out”

- Ensure `credentials: 'include'` is set for `/api/*` requests.
  - **File**: `frontend/src/auth/AuthContext.jsx:57-59`

### H3) “CORS errors in console”

- Ensure backend CORS allows credentials.
  - **File**: `API/Program.cs:45-56`

## Next step

Continue to:

- `08-Glossary.md`
