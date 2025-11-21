# 12. Debugging and Troubleshooting Playbook

## What you’ll learn
- Apply a **repeatable debugging playbook** to backend, frontend, and database issues.
- Break down bugs into **small, testable problems** instead of guessing.
- Recognize common pitfalls in this project (CORS, off‑by‑one minutes, date mismatches) and how to confirm or rule them out.

## Prerequisites
- Familiarity with earlier lessons (API endpoints, EF Core, React state and fetching).
- A basic understanding of browser dev tools (Network, Console) and logs.

---

## Mental model: debugging as fast feedback loops

Effective debugging is about **rapid feedback cycles**:

1. Form a hypothesis: “Maybe the minutes array is the wrong length.”
2. Design a small, concrete test: “Log the array length for one hour.”
3. Run the test and inspect the result.
4. Refine your hypothesis and repeat.

Instead of hunting randomly, you turn a big, confusing bug into **a series of tiny, testable checks**.

---

## 🟢 Green Coder Tips: Don't Leave Traces

Debugging often involves adding `Console.WriteLine()` or `console.log()`. That's fine while you're hunting the bug.

> "Don't leave commented out code in your codebase."
> — *Clean Code Fundamentals*

The same applies to logs. Once you fix the bug:
- **Remove** the temporary logs.
- Don't just comment them out – they are noise!
- If the log is valuable for future debugging (e.g. "System started"), keep it and make it a proper log (using a Logger service).
- If it was just "Here 1", "Here 2" – delete it.

---

## Guided walkthrough

### 1. Backend: narrow the problem and log clearly

1. In an action or endpoint, add temporary logging around:
   - Input parameters (date, hour, startMinute, endMinute).
   - Key decisions (e.g. “overlap detected”, “booking accepted”).
2. Reproduce the bug with a **small case** (e.g. one booking from `0..15`, another from `15..30`).
3. Compare actual values in logs to what you assumed they were.
4. Remove or refine logs once you’ve found the issue.

> Keep logs expressive and focused to avoid mental mapping later.

### 2. Frontend: confirm requests and data shapes

1. Open the browser dev tools → **Network** tab.
2. Trigger the failing action (e.g. loading the week or submitting a booking).
3. Check:
   - The URL and HTTP method.
   - Status codes (200, 400, 500, CORS errors).
   - The response payload shape.
4. In the **Console**, log the data passed into components or into state setters.
   - Confirm that normalization (e.g. `Hours` → `hours`) is working as expected.
   - If not, fix it in the translator functions before touching UI components.

### 3. EF/SQL: see what’s really in the database

1. Ensure the database is created (e.g. `EnsureCreated` during boot, if applicable).
2. Open the SQLite DB with a viewer.
3. Inspect:
   - Rows in the `Events` table (dates, hours, minute ranges).
   - Whether recent POSTs actually created or updated rows.
4. If performance is an issue with many rows, consider where an index on `(Date, Hour)` would help.

### 4. Common gotchas checklist

- **CORS blocked**: error in console about CORS?
  - Confirm dev proxy configuration or `AddCors` policy.
- **Minutes off‑by‑one**:
  - Remember the `[start, end)` convention and verify loops use `< end`.
- **Today mismatch**:
  - Ensure comparisons use `DateTime.Date` consistently on both sides.

Use this list as a quick “sanity check” before diving into deeper theories.

---

## Fun snippet – how pros debug

Experienced developers often debug in similar ways:

- They **reproduce** the problem with the smallest possible input.
- They **instrument** the code with logs or temporary assertions.
- They use **tests or scripts** to lock in the fix and prevent regressions.

The goal is not to be a genius guesser, but to design a process that exposes the bug step by step.

---

## Check yourself

- When you hit a bug, what is the **first question** you now ask yourself?
- How can you turn “the app is broken” into two or three smaller, testable questions?
- Which tools will you reach for first on the:
  - Backend?
  - Frontend?
  - Database?

---

## 📚 External Resources

- [Chrome DevTools Documentation](https://developer.chrome.com/docs/devtools/)
- [Rubber Duck Debugging](https://rubberduckdebugging.com/) - Explain your code line-by-line to a duck (or AI).

---

## Where to go next

**Next lesson:** `13. Labs – Build, Extend, and Validate`
You’ll apply the patterns and debugging techniques you’ve learned to a set of hands‑on lab challenges.
