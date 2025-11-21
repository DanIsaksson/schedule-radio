# Scheduler Frontend (React + Vite)

A minimal React app that talks to your ASP.NET Minimal API.

## Prerequisites
- Node.js LTS (18+)
- ASP.NET backend running at http://localhost:5219 (from API project)

## Run (development)
```bash
npm install
npm run dev
```
- Vite dev server runs on http://localhost:5173
- API calls are proxied to http://localhost:5219 so no CORS issues

## Build (optional)
```bash
npm run build
```
- Outputs to `frontend/dist` by default.
- If you want ASP.NET to serve the built React app, you can set `build.outDir` in `vite.config.js` to `../API/wwwroot` and rebuild (this will clear/replace existing static files there).

## Endpoints used
- GET `/db/schedule/today` to render today’s hours and booked/free state (read-only)
- GET `/db/schedule/7days` to render the 7-day projection (read-only)
- POST `/db/event/post?date=YYYY-MM-DD&hour=H&startMinute=S&endMinute=E` to create a booking (admin only)

## Routing
- Hash-based routes (no extra libs):
  - `#/` consumer homepage
  - `#/schedule` consumer full schedule (read-only)
  - `#/admin` admin booking tools

## Notes
- Keep the API project running while using the frontend.
- Vite proxy routes `/db/*` to `http://localhost:5219` in dev to avoid CORS.
