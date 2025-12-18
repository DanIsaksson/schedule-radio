# 06. Legacy In-Memory System

## What you’ll learn
- Compare the original **in-memory** booking system with the **database-backed** version.
- Understand when an in-memory approach is useful and when persistence is required.
- Trace the legacy booking flow and see how it mirrors the newer DB-backed logic.

## Prerequisites
- `04. Booking – Database CRUD Endpoints` – you know how DB-backed bookings are created and validated.
- `05. Schedule Projection` – you know how bookings become a 7-day grid.

---

## Mental model: whiteboard vs notebook

Think of two ways to track bookings:

- **Whiteboard (in-memory)**:
  - Fast to write and erase.
  - Completely cleared if someone wipes it or the power goes out.
- **Notebook/database**:
  - Slower to change but **persists** across restarts.
  - Better for real users who expect their bookings to stick.

The legacy system is the whiteboard version. The DB-backed system is the notebook.

---

## 🟢 Green Coder Tips: Legacy Code & Refactoring

You might ask, "Why do we keep this legacy code?"

> "Refactoring is a controlled technique for improving the design of an existing code base. Its essence is applying a series of small behavior-preserving transformations..."
> — *Clean Code Fundamentals*

Legacy code isn't "bad" – it's just the previous version. It can be useful:
- As a **reference** (to see how logic worked before).
- As a **fallback** or for testing.
- As a starting point for the **Boy Scout Rule**: "Leave the campground cleaner than you found it."

When you see legacy code, don't just delete it blindly. Understand it, and if you replace it, ensure the new version preserves the important behaviors.

---

## Guided walkthrough

### 1. Find the legacy schedule endpoints

1. Open `API/Endpoints/ScheduleEndpoints.cs`.
2. Identify the endpoints that serve schedule data from the in-memory structure (e.g. `/schedule/today`).
3. Note how their URLs differ from the DB-backed endpoints (like `/db/schedule/today`).

### 2. See how legacy booking works

1. Open `API/Program.cs` and look for the legacy `/schedule/book` endpoint.
2. If `API/Actions/Actions.cs` (or a similarly named actions file) exists, open it.
3. Compare the legacy booking logic to the DB-backed version:
   - What validation rules are shared?
   - How are overlaps checked?
   - Where is data stored instead of a database table?

> Focus on the **similarities** in rules, and the **differences** in where the data lives.

### 3. Compare behaviour: legacy vs DB

1. Use the legacy path:
   - `POST /schedule/book` to add a booking.
   - `GET /schedule/today` to view today’s schedule.
2. Use the DB-backed path:
   - Create a booking through the admin UI or via `/db/event/post`.
   - Call `/db/schedule/today` or `/db/schedule/7days`.
3. Restart the API.
   - Check which bookings still exist.
   - Notice that in-memory bookings disappear, but DB-backed bookings persist.

---

## Fun snippet – in-memory vs persistent storage in the wild

Real systems often mix in-memory and persistent storage:

- **Web apps**: use an in-memory cache (like Redis or in-process memory) for speed, but a database for durability.
- **Game servers**: keep the current match state in memory, but write player progress to disk.
- **Build tools**: store temporary results in memory while writing build artifacts to files.

The legacy scheduler is like a pure cache: fast and simple, but not something you trust for real, long-lived bookings.

---

## Try it

1. Create a booking using the legacy `/schedule/book` endpoint.
2. Confirm it appears when you call `/schedule/today` (legacy).
3. Restart the API.
4. Call `/schedule/today` again and notice that the booking is gone.
5. Compare with a DB-backed booking that **survives** the restart.

---

## Check yourself

- What are the main pros and cons of an in-memory booking system?
- In what situations would you still choose an in-memory approach on purpose?
- Why does the UI now rely on the DB-backed endpoints instead of the legacy ones?

---

## 📚 External Resources

- [Refactoring.com - What is Refactoring?](https://refactoring.com/)
- [Boy Scout Rule (O'Reilly)](https://www.oreilly.com/library/view/97-things-every/9780596809515/ch08.html)

---

## Where to go next

**Next lesson:** `07. Frontend Bootstrap and Hash Routing`
You’ve explored how backend data flows. Next you’ll look at how the frontend boots up and uses hash-based routing to decide which screen to render.
