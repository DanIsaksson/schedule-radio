# 10. Admin Booking Flow (POST + refresh)

## What you’ll learn
- Trace the full admin booking flow: from form input → POST request → backend validation → UI refresh.
- Explain how `submitBooking` builds the request and handles success and error cases.
- Understand why both `loadToday()` and `loadWeek()` are called after a successful booking.

## Prerequisites
- `04. Booking – Database CRUD Endpoints` – you know how bookings are validated on the backend.
- `09. Fetching Data and Normalizing Shapes` – you know how `loadToday` and `loadWeek` populate state.

---

## Mental model: a conversation between form and schedule

Imagine the admin form and the schedule view having a **conversation**:

1. The form says: “Here is a new booking I’d like to add.”
2. The backend responds: “That’s valid/invalid; here is the result.”
3. If valid, the frontend says: “Great, I’ll **refresh** my picture of today and the week so everything is up‑to‑date.”

`submitBooking` is the function that coordinates this conversation.

---

## 🟢 Green Coder Tips: Single Responsibility & State

In Clean Code, **Single Responsibility** applies to functions like `submitBooking`.

> "Functions should do one thing and do it well."
> — *Clean Code Fundamentals*

Ideally, `submitBooking` should only handle the submission. But wait, it also refreshes the UI!
- Is that a violation? Maybe.
- A cleaner approach might be to have `submitBooking` *only* submit, and then call a separate `refreshData()` function.
- Notice how we use `Promise.all([loadToday(), loadWeek()])`. This groups the refresh logic nicely, so `submitBooking` doesn't have to know the details of *how* to refresh, just *that* it needs to refresh.

Also, naming state variables like `posting` (boolean) is a good example of a **Positive Boolean Name** (instead of `notDone`).

---

## Guided walkthrough

### 1. Inspect `submitBooking` in `App.jsx`

1. Open `frontend/src/App.jsx`.
2. Find the `submitBooking` function.
3. Identify:
   - Which form values it reads.
   - How it builds the query string or request body.
   - Which endpoint it calls (e.g. `POST /db/event/post`).

Connect this to the backend POST endpoint you saw in `EventDbEndpoints` and `EventActionsDb`.

### 2. See how errors and success are handled

1. In `submitBooking`, locate the logic that:
   - Checks the HTTP status.
   - Sets an error message when the booking is invalid (e.g. overlaps).
   - Sets a success message when the booking is accepted.
2. Note how it uses state variables like `posting`, `error`, and `msg` to control what the UI shows during and after the request.

> Clear, expressive state names reduce mental mapping: `posting`, `error`, and `msg` describe their purpose directly.

### 3. Understand the refresh with `Promise.all`

1. Find where `loadToday()` and `loadWeek()` are called after a successful POST.
2. See how `Promise.all` (or similar) is used to refresh both pieces of data in parallel.
3. Think through why both need to be updated:
   - Today’s card must show the new booking.
   - The week grid must also reflect the change.

This ensures the UI stays consistent without you manually updating multiple pieces of state.

---

## Fun snippet – similar admin flows elsewhere

Admin flows like this show up in many apps:

- **CMS dashboards**: create/edit a post, then refresh the list and detail views.
- **Inventory systems**: add stock, then refresh current inventory and recent activity.
- **Issue trackers**: create a ticket, then refresh both the board and the “my issues” view.

In all of them, a single action triggers **both a server change and a UI refresh** so the user sees an accurate, up‑to‑date picture.

---

## Try it

1. In the admin view, create an overlapping booking.
   - Observe the error message and confirm no UI data is refreshed incorrectly.
2. Adjust the form to a valid time range.
3. Submit again and confirm:
   - The booking appears in the Today card.
   - The week grid also shows the new booking.
4. Watch the Network tab while submitting to see the POST and subsequent GET requests.

---

## Check yourself

- Why is it helpful to centralize booking submission logic in a single function like `submitBooking`?
- Why do we refresh both `today` and `week` instead of trying to manually patch them?
- In which situations should `submitBooking` set an error vs a success message?

---

## 📚 External Resources

- [MDN - Promise.all()](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/all)
- [React Docs - State Management](https://react.dev/learn/managing-state)

---

## Where to go next

**Next lesson:** `11. Styling – Theme, Today Emphasis, Tooltips`
You’ve worked through the data and booking flows. Next you’ll explore how styling choices communicate state and behavior to the user.
