# 11. Styling: Theme, Today Emphasis, Tooltips

## What you’ll learn
- Identify the main styling choices that make the schedule easy to read (theme, Today emphasis, tooltips).
- Explain how styles communicate important state (e.g. current day, booked vs free time) without extra text.
- Make small, safe styling tweaks while preserving readability and intent.

## Prerequisites
- `05. Schedule Projection` – you know how booked minutes are represented in data.
- `10. Admin Booking Flow` – you’ve seen how bookings appear in the UI.
- Basic CSS familiarity (classes, selectors, variables).

---

## Mental model: styling as visual comments

Think of your styles as **comments for the user’s eyes**:

- Colors, backgrounds, and emphasis tell the user what matters **at a glance**.
- The layout and tooltips explain how time is sliced and what is booked.

Good styling makes the UI self‑explanatory so users don’t have to mentally decode raw data.

---

## 🟢 Green Coder Tips: Expressive Styles

Clean Code rules apply to CSS too!

> "Choose an expressive name... that accurately and clearly describes its purpose..."
> — *Clean Code Fundamentals*

In `styles.css`:
- A class named `.today` is expressive. It tells you *what* it is, not just *how* it looks (unlike `.red-box`).
- Using **CSS Variables** like `--color-primary` is better than hardcoding `#ff0000` everywhere. It creates a **Single Source of Truth** for your theme.

---

## Guided walkthrough

### 1. Explore theme variables and base styles

1. Open `frontend/src/styles.css`.
2. Find the root variables and theme definitions (colors, gradients, etc.).
3. Note which colors are used for:
   - General background.
   - Accents (e.g. orange → orange‑red gradients).

Consider how these choices help visually separate key areas of the screen.

### 2. See how Today is emphasized

1. In `styles.css`, locate the `.day.today` rules.
2. Observe how Today’s column is styled differently (background, border, heading accent).
3. In the running app, compare Today’s column to other days:
   - Is it obvious which day is Today?
   - Does the emphasis help your eyes land in the right place first?

This is the visual equivalent of a clear, expressive variable name.

### 3. Inspect tooltips for booked ranges

1. Open `frontend/src/App.jsx`.
2. Find where `HourCell` or related components construct tooltip strings.
3. Notice how the tooltip summarizes:
   - Start and end times.
   - Possibly a title or description.
4. Think about how this helps communicate the **half‑open interval** convention (e.g. `12:00–12:30` representing `[start, end)`).

> Tooltips serve as inline documentation for how the schedule works.

---

## Fun snippet – visual semantics in other interfaces

You’ll see similar styling patterns everywhere:

- **Calendar apps**: Today is highlighted; current time is a bold line; busy slots are shaded.
- **Transport apps**: the current step in a journey is emphasized; delays use strong colors.
- **Monitoring dashboards**: warnings and alerts use consistent colors and icons to stand out.

In all of these, color and layout act as a shared language between the system and the user.

---

## Try it

1. In `styles.css`, tweak the `.day.today` background alpha or color slightly.
2. Reload the app and check:
   - Is Today still clearly visible?
   - Is text still readable (contrast)?
3. (Optional) Add a small badge or subtle visual indicator near any "LIVE" label or key UI element to reinforce status.

Keep changes modest; aim to preserve clarity rather than create a new theme.

---

## Check yourself

- How do styles make it obvious which day is Today and which slots are booked?
- Where in the UI is the half‑open interval convention hinted at or explained?
- If you changed a color, how would you verify that accessibility (contrast/readability) is still acceptable?

---

## 📚 External Resources

- [MDN - Using CSS custom properties (variables)](https://developer.mozilla.org/en-US/docs/Web/CSS/Using_CSS_custom_properties)
- [BEM (Block Element Modifier)](https://getbem.com/naming/) - A naming convention for cleaner CSS (optional reading).

---

## Where to go next

**Next lesson:** `12. Debugging and Troubleshooting`
You’ve seen how data, interactions, and styling fit together. Next you’ll learn a systematic approach to debugging issues across the backend and frontend.
