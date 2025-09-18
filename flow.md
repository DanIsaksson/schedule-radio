# Scheduler-Radio ─ Logical Flow Documentation

## ASCII Flow Diagram

```text
┌──────────────────────────────────────────────────────────┐
│               API/Models/ScheduleData.cs                │
│ (Domain-model classes)                                  │
│  ┌────────────┐   1     ┌────────────┐                  │
│  │ScheduleData│────────▶│DaySchedule │                  │
│  └────────────┘         └────────────┘                  │
│        │  (holds 7)            │ (holds 24)            │
│        │                       ▼                       │
│        │               ┌────────────┐                  │
│        └───────────────│HourSchedule│                  │
│                        └────────────┘                  │
└──────────────────────────────────────────────────────────┘
                │            ▲ (in-memory instance)        ▲
                │  created   │                              │
                ▼            │ used by                     │
┌──────────────────────────────────────────────────────────┐
│                 API/Program.cs (WebHost)                │
│  • Build WebApplication                                 │
│  • `ScheduleData schedule = new ScheduleData()` ────────┘
│  • Demo booking via ScheduleAction.SetBooking           │
│  • Map HTTP routes                                      │
│      ├─ GET /schedule           → returns whole model   │
│      └─ GET /schedule/today     → returns one Day       │
│  • Serves static files (index.html, css, js)            │
└──────────────────────────────────────────────────────────┘
                │   (HTTP JSON)                            ▲
                │                                          │
                ▼                                          │
┌──────────────────────────────────────────────────────────┐
│        Browser  ←─────────  HTTP GET  ────────────────┐  │
│                                                      │  │
│  index.html   +   style.css          +   script.js   │  │
│                                                      │  │
│  User clicks [Load from API] button                  │  │
│          ▼                                           │  │
│  script.js                                          │  │
│  ├─ fetch("/schedule/today")/CORS URL                │  │
│  ├─ await JSON                                       │  │
│  ├─ Update <pre> with pretty JSON                    │  │
│  └─ Build <ul id="scheduleList"> hour rows           │◀─┘
│        • 00..23 with status  (Booked / Free)          
└──────────────────────────────────────────────────────────┘
```

---

## Detailed Explanation

### 1. Domain Model – `ScheduleData.cs`
* **`ScheduleData`** is the root aggregate. Constructor uses `Enumerable.Range(0,7)` to create exactly seven consecutive `DaySchedule` objects starting at _today_.
* **`DaySchedule`** holds 24 `HourSchedule` objects (00-23). Each `HourSchedule` has a `bool[60] Minutes` array where each index represents a minute; `true` = booked.
* The model is **pure in-memory**; no EF, database or persistence. That keeps the sample lightweight and easy to reset on every host restart.

### 2. Web Host & API – `Program.cs`
* Builds an ASP.NET `WebApplication` with Swagger + CORS.
* Instantiates one global `schedule` object (see model above).
* Calls `ScheduleAction.SetBooking` once so the demo returns at least one booked range.
* Configures two `MapGet` routes:
  * `GET /schedule`  → returns the entire `ScheduleData` object.
  * `GET /schedule/today`  → LINQ-filters the `Days` list for _today_ and returns a single `DaySchedule`.
* Also serves static files, which makes `http://localhost:<port>/index.html` load the front-end without an extra server.

### 3. Front-End – `index.html` + `script.js`
* `index.html` is minimal: a header, a button (`#loadBtn`), a `<pre>` for raw JSON and a `<ul id="scheduleList">` where the human-friendly list is rendered.
* `style.css` provides basic readable defaults and some extra rules for the list (flex layout, alternating borders, etc.).
* **`script.js` logic**
  1. Caches element references (`btnLoad`, `preOut`).
  2. Registers an async click handler.
  3. Performs `fetch()` to `/schedule/today` (or absolute API URL when using Live-Server) and handles network / HTTP errors.
  4. Pretty-prints the returned JSON into `<pre>` so developers can inspect the full payload.
  5. Iterates over the payload’s `hours` array, builds `<li>` rows: the left span shows the hour (`HH:00`), the right span shows _Booked_ if **any** minute in that hour is `true`, otherwise _Free_.

### 4. Syntax Influence on Flow
* **LINQ queries** in `Program.cs` (`FirstOrDefault`, `Enumerable.Range`) succinctly express data creation and filtering, driving the logical decisions shown in the diagram.
* **Arrow functions + template literals** in `script.js` allow concise DOM updates, keeping the control flow linear and easy to follow from the click event down to each list item creation.
* ASP.NET **Minimal API** syntax (`app.MapGet(...) => ...`) directly links the HTTP surface to the model with no separate controller class, making the mapping explicit in the diagram.

### 5. Key Interactions & Implementation Notes
| Interaction | Detail |
|-------------|--------|
| `ScheduleAction.SetBooking` → `HourSchedule.Minutes` | Encapsulates booking logic and prevents scattered minute manipulation. |
| Front-end fetch to `/schedule/today` | Relies on the static-file host being **same-origin** or on enabling CORS (already configured). |
| List rendering | Uses `some()` on the minutes array for a quick _any booked_ check—O(60) but negligible. |

The combined approach yields a fully working demo with only three main source files, yet demonstrates complete round-trip data flow from domain model → HTTP API → UI.