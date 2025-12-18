# 13. Labs: Build, Extend, and Validate

## What you’ll learn
- Apply your new skills to build real features.
- **Pseudo-code** your way through problems before writing syntax.

## Prerequisites
- All previous lessons.

---

## Mental model: Training Wheels
We won't throw you into the deep end.
For each Lab, we provide **Pseudo-code**. Your job is to translate it into real C# or JS.

---

## Lab A – Read-only improvements (Beginner)

**Goal:** Add a `GET /db/event/count` endpoint and show the number on the screen.

**Backend (C#):**
1.  Go to `EventDbEndpoints.cs`.
2.  Add a new `MapGet` inside the group.
    ```csharp
    // Pseudo-code
    group.MapGet("/count", (SchedulerContext db) => {
        return db.Events.Count();
    });
    ```

**Frontend (JS/React):**
1.  Go to `App.jsx`.
2.  Add a new state variable: `const [count, setCount] = useState(0);`
3.  Create a load function (or add to an existing one):
    ```javascript
    // Pseudo-code
    async function loadCount() {
        // fetch from /db/event/count
        // await response
        // setCount(data)
    }
    ```
4.  Call this function in `useEffect` (where the app starts).
5.  Render `{count}` somewhere in the JSX.

---

## Lab B – Booking UX polish (Beginner)

**Goal:** Disable the "Book" button if the time is invalid (e.g., End time is before Start time).

**Frontend (JS/React):**
1.  Find the button in `AdminPanel` (or wherever the form is).
2.  React buttons have a `disabled` attribute.
    ```javascript
    <button disabled={isValid ? false : true}>Book</button>
    ```
3.  Calculate `isValid`.
    *   You have `form.start` and `form.end`.
    *   Logic: `const isValid = form.end > form.start;`
4.  Apply it.

**Check:**
-   Set Start to 10, End to 9. Is the button greyed out?

---

## Lab C – Conflict highlighting (Intermediate)

**Goal:** If the API says "Conflict" (400 Error), turn the grid red.

**Logic Flow:**
1.  **State:** Add `const [conflictHour, setConflictHour] = useState(null);` in `App.jsx`.
2.  **API Call:** In the `book()` function, check the response.
    ```javascript
    if (response.status === 400) {
        // The server said no!
        // Maybe parsing the error message to find WHICH hour failed?
        // For now, just highlight the hour they tried to book.
        setConflictHour(form.hour);
    }
    ```
3.  **Render:** Pass `conflictHour` down to `WeekGrid`.
4.  **Style:** In `WeekGrid`, inside the `.map()` loop:
    ```javascript
    // If this hour == conflictHour, add a CSS class "red-border"
    className={hour === conflictHour ? "cell red-border" : "cell"}
    ```

---

## Lab D – Title field (Intermediate)

**Goal:** Allow users to name their bookings.

**Backend:**
1.  Modify `EventEntity.cs`: Add `public string? Title { get; set; }`
2.  Update the database (If using SQLite, you might need to delete the `.db` file so it recreates, or use Migrations).
3.  Update the POST endpoint to accept `Title`.

**Frontend:**
1.  Add `title` to the `form` state object.
2.  Add an `<input>` field for Title.
3.  Update the `book()` function to send the title.

---

## Check yourself

-   Which lab did you find most challenging?
-   Did the **Pseudo-code** help you focus on logic instead of syntax?

---

## 📚 External Resources

**For Labs:**
*   [React.dev - Conditional Rendering](https://react.dev/learn/conditional-rendering) (For the disabled button)
*   [MDN - Fetch API](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API)
