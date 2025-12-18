# 04. Booking: Database CRUD Endpoints

## Introduction
This lesson covers the **API Endpoints** for reading and writing bookings.
You will learn how the "Doors" of the application work.

**Linked Code**:
- `API/Endpoints/ScheduleDbEndpoints.cs` (Reading)
- `API/Endpoints/EventDbEndpoints.cs` (Writing - *Coming Soon*)

---

## [Endpoint.Schedule.1] Schedule Endpoints
These endpoints are responsible for **Reading** data.
- They don't change anything in the database.
- They just ask the database for data and return it to the frontend.

---

## [Endpoint.Schedule.2] Get Today's Schedule
`GET /db/schedule/today`
- **Goal**: Get the schedule for just the current day.
- **Usage**: The frontend uses this to show the "Today" card on the dashboard.
- **Logic**: Calls `ScheduleProjectionDb.BuildToday`.

---

## [Endpoint.Schedule.3] Get 7-Day Schedule
`GET /db/schedule/7days`
- **Goal**: Get the schedule for the next 7 days.
- **Usage**: The frontend uses this to draw the big weekly grid.
- **Logic**: Calls `ScheduleProjectionDb.BuildSevenDaySchedule`.

---

## Mental model: a guarded gate for bookings
Think of the booking system as a **gatekeeper**:
- When you **create** or **reschedule**, the gatekeeper checks:
  - Are the hour/minute values in a valid range?
  - Does this booking **overlap** any existing booking for that date and hour?
- Only if the checks pass does it **persist** the change to the database.

---

## Check yourself
- What is the difference between `MapGet` and `MapPost`?
- Why do we need two different endpoints for the schedule (Today vs 7Days)?
