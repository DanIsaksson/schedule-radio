recap - A beginner-friendly guide to the JavaScript + React patterns used in this repo (useState/useEffect, Context, fetch, common syntax).

# 10) JavaScript and React Patterns Used in This Repo

## A) React state = “Memory” (useState)

When you see:

- `useState(...)`

Think:

- “This component remembers a value between renders.”

Example (route memory):

- **File**: `frontend/src/main.jsx`
- **Line(s)**: `25`
- **Code**:
  - `const [route, setRoute] = React.useState(window.location.hash || '#/')`

Example (schedule memory):

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1067-1068`
- **Code**:
  - `const [today, setToday] = useState(null)`
  - `const [week, setWeek] = useState(null)`

## B) Effects = “Side effects with the outside world” (useEffect)

Examples of outside world:

- network calls (fetch)
- browser events (hashchange)

### B1) Listening to hash changes

- **File**: `frontend/src/main.jsx`
- **Line(s)**: `30-34`
- **Code**:
  - `window.addEventListener('hashchange', onHashChange)`

### B2) Loading data on startup

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1357-1359`
- **Code**:
  - `useEffect(() => { loadWeek() }, [])`

## C) React Context (global state without prop drilling)

Auth state is shared using a Context.

### C1) Creating the context

- **File**: `frontend/src/auth/AuthContext.jsx`
- **Line(s)**: `9`
- **Code**:
  - `const AuthContext = createContext(null)`

### C2) Providing the context

- **File**: `frontend/src/auth/AuthContext.jsx`
- **Line(s)**: `33-38`

### C3) Consuming the context

- **File**: `frontend/src/auth/AuthContext.jsx`
- **Line(s)**: `160-169`
- **Code**:
  - `export function useAuth() { ... }`

## D) fetch() basics (HTTP calls)

### D1) GET schedule

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1235-1238`
- **Code**:
  - `const res = await fetch('/db/schedule/7days')`
  - `const data = await res.json()`

### D2) Include cookies

When calling protected endpoints, use:

- `credentials: 'include'`

Example:

- **File**: `frontend/src/auth/AuthContext.jsx`
- **Line(s)**: `57-59`

## E) Common JS syntax used in this repo

### E1) Arrow functions

- `const loadWeek = async () => { ... }`

Example:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1232-1244`

### E2) Template strings

- Uses backticks: `` `...${value}...` ``

Example:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1174-1178`

### E3) Destructuring

- Pull properties out of an object.

Example:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1440-1440`
- **Code**:
  - `const { name, value } = e.target`

### E4) Spread operator (copy object/array)

Example:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1444-1447`
- **Code**:
  - `setForm(currentForm => ({ ...currentForm, [name]: ... }))`

Beginner translation:

- “Create a new object that is a copy of the old one, but change one field.”

### E5) Computed property names: `[name]: value`

Example:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1445-1447`

Beginner translation:

- If `name` is `"title"`, then this updates `form.title`.

### E6) Nullish coalescing: `a ?? b`

Example:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `507`
- **Code**:
  - `const dateObject = new Date(day.date ?? day.Date)`

Beginner translation:

- “Use `day.date` if it exists, otherwise use `day.Date`.”

### E7) Optional chaining: `week?.days`

Example:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `553-555`
- **Code**:
  - `const days = week?.days ?? week?.Days ?? []`

Beginner translation:

- “If week is null, don’t crash — just give me undefined.”

### E8) Array.map / filter

Example:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `468-469`
- **Code**:
  - `minutes.filter(Boolean).length`

Beginner translation:

- “Count how many minutes are booked.”

## F) Controlled components (forms)

Example:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `970-974`
- **Code**:
  - `<input ... value={form.title} onChange={onChange} />`

Controlled means:

- React state is the source of truth for the input.

## G) Next step

Return to:

- `00-INDEX.md` (pick your next reading path)
