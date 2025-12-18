# Dual Frontends Plan – Admin Booking and Public Consumer Site (React)

Based on: `Iteration/radio-station-website-assignment.md`
Design refs: `design-context/BASIC-RADIOSCHEDULE-STARTINGOUTLINE.jpg` (scale/position), `design-context/RadioSchedule-Frontend-logic-flow-basic-sketch.jpg` (logic flow)
Inspiration: https://radioplay.se/nrj, https://www.viaplayradio.se/rixfm/

## Objectives
- Provide two clear frontends, using the same backend:
  - Public Consumer Site (read-only, polished UI, schedule preview + full schedule page).
  - Admin Booking UI (create/reschedule/delete bookings; today/week views).
- Stay within lessons scope (components, props, state, effects, lists, simple routing, plain fetch, plain CSS).

## Approach (beginner-friendly)
- Single Vite app with hash-based routes to avoid extra libraries:
  - `#/` Home (consumer)
  - `#/schedule` Full schedule (consumer)
  - `#/admin` Admin booking (internal)
- Shared CSS theme with orange → orange-red accents; separate section classes for public vs admin.
- All schedule data via DB endpoints; no in-memory writes on consumer pages.

## Pages & Routes
- Consumer
  - `#/` Home: SiteHeader, HeroNowPlaying, FeaturedRow, SchedulePreview, SiteFooter.
  - `#/schedule` Full read-only week grid (tooltips on hover, today highlighted).
- Admin
  - `#/admin` Week grid + Today list + Booking form (POST to DB) + refresh; optional delete/reschedule later.

## API usage (codemap-aligned)
- Read schedule (both UIs): GET `/db/schedule/7days`.
- Admin writes only:
  - POST `/db/event/post?date=YYYY-MM-DD&hour=H&startMinute=S&endMinute=E`.
  - Later optional: POST `/db/event/{id}/delete`, POST `/db/event/{id}/reschedule`.

## Components (keep inline in App.jsx initially)
- Public: `SiteHeader`, `HeroNowPlaying`, `FeaturedRow`, `SchedulePreview`, `SchedulePage`, `SiteFooter`.
- Shared: `WeekGrid`, `DayColumn`, `HourCell` (already present; reuse for both UIs; preview uses compact styles).
- Admin: `AdminPanel` (wraps Today list, Week grid, Booking form).

## Styling
- `frontend/src/styles.css` additions (already started):
  - Accent variables `--accent`, `--accent2` and gradient utilities (buttons, badge-live).
  - Consumer layout: header, hero, featured cards, schedule preview.
  - Admin layout: basic form controls (existing), card layout, alerts.

## Tasks and File Changes
- Routing
  - Edit `frontend/src/main.jsx`: minimal hash router (state from `window.location.hash`), pass `route` to `App`.
- Consumer UI
  - Edit `frontend/src/App.jsx`: render Home and Schedule screens when `route` is `#/` or `#/schedule`.
  - Use existing week fetch for both preview and full schedule; no write actions.
- Admin UI
  - Edit `frontend/src/App.jsx`: move booking Today/Week + form into `AdminPanel` and render when `route` is `#/admin`.
  - Ensure POST uses `/db/event/post`, then refresh week + today.
- Styles
  - Edit `frontend/src/styles.css`: keep accent theme and add any missing consumer/admin classes.
- Dev proxy (optional)
  - Add Vite proxy for `/db` during dev to avoid CORS.

## Milestones
1) Router + shared theme in styles.
2) Consumer Home (hero, featured, schedule preview).
3) Consumer Schedule page (full read-only week).
4) AdminPanel route using existing booking UI (moved, minimal tweaks).
5) Polish (tooltips, emphasize today, responsive).

## Lesson alignment checklist
- Components/props and list rendering with `.map()`.
- State/effects for data fetching and route state.
- Children wrappers for cards/sections if helpful.
- Plain fetch for `/db/schedule/7days` and admin POST.
- Plain CSS; no external UI libs.
- Routing kept minimal (hash-based; no router lib unless explicitly practicing it).

## Codemap cross-check
- Reads: `/db/schedule/7days`.
- Writes (admin only): `/db/event/post`, optional delete/reschedule endpoints.
- In-memory endpoints kept separate; not used by consumer.

## Run & test (quick)
- API: run from `API/` (dotnet run --urls http://localhost:5219).
- Frontend: run from `frontend/` (npm install; npm run dev). Add dev proxy for `/db` to `http://localhost:5219` if needed.

## Implementation locations (file:lines)
- Router
  - `frontend/src/main.jsx`: lines 6–18 (hash router comments) and Router render.
- Consumer UI (App.jsx)
  - `SiteHeader`: lines 74–84
  - `SiteFooter`: lines 84–91
  - `HeroNowPlaying`: lines 93–105
  - `FeaturedRow`: lines 107–126
  - `SchedulePreview`: lines 133–161
  - Route flags and conditional render: lines 315–325 (flags/render)
- Admin UI (App.jsx)
  - `AdminPanel` component: lines 164–221
  - Render `AdminPanel` on `#/admin`: lines 329–341
- Shared schedule components
  - `WeekGrid`/`DayColumn`/`HourCell`: lines 62–72, 44–56, 16–42
- Styles
  - `frontend/src/styles.css`: consumer section label and today highlight at lines 37, 63–65

## Commenting passes (done)
- API core
  - Program.cs: top header and sections adjusted to beginner-first style (lines 1–6, 16–26, 28–37, 43–67, 69–83, 85–95)
  - Endpoints/ScheduleDbEndpoints.cs: beginner header + endpoint hints
  - Endpoints/EventDbEndpoints.cs: write-flow one-liners with file refs
  - Endpoints/ScheduleEndpoints.cs: legacy-read note, concise hints
- Data + Models
  - Data/SchedulerContext.cs: header + DI/DbSet notes (lines 1–8, 16, 22–32)
  - Models/EventEntity.cs: entity purpose/properties
  - Models/ScheduleModels.cs: read model, minutes semantics
- Frontend
  - main.jsx: Router comments (lines 6–18)
  - App.jsx: route split, fetch notes, tooltip ranges, admin prefill
  - styles.css: consumer section label, today emphasis
