# 08. App Layout, Components, and State

## What you’ll learn
- Identify the main components in `App.jsx`.
- Understand **React State** (`useState`) by comparing it to C# Class Fields.
- Trace how data flows down.

## Prerequisites
- `07. Frontend Bootstrap`
- **No previous React State knowledge required.**

---

## Mental model: App as the Game Manager
In a Text Adventure, you have a `Game` class that holds the score, player health, and current location.
In React, `App.jsx` is that `Game` class.

*   **State** = The Class Variables (`score`, `health`).
*   **Components** = The Methods that print to the console (`PrintMap()`, `PrintInventory()`).

When `score` changes in the `Game` class, you call `PrintScore()` again to show the new number.
In React, when **State** changes, the screen **automatically** updates. You don't have to call "Print" manually!

---

## 🟢 Green Coder Tips: `useState` Hook
This is the syntax that confuses every C# developer.

**C# Class:**
```csharp
public class App {
    private int count = 0; // The variable

    public void SetCount(int newValue) {
        count = newValue;
        RepaintScreen(); // Manual update
    }
}
```

**React Component:**
```javascript
const [count, setCount] = useState(0);
```

**Breakdown:**
1.  `useState(0)`: "Create a state variable starting at 0."
2.  `[...]`: Array Destructuring. `useState` returns TWO things:
    *   Item 1 (`count`): The current value (like the `int count`).
    *   Item 2 (`setCount`): A function to update it (like the `SetCount` method).
3.  **Rule:** NEVER say `count = 5`. ALWAYS say `setCount(5)`.
    *   Why? Because `setCount` triggers the "RepaintScreen" magic.

---

## Guided walkthrough

### 1. Survey the components in `App.jsx`
1.  Open `frontend/src/App.jsx`.
2.  Look at the `return (...)` block at the bottom.
    *   This is the HTML (JSX) that App renders.
    *   Find `<WeekGrid ... />` and `<AdminPanel ... />`.
    *   Notice how they take "arguments" (attributes). These are called **Props**.

### 2. Map out App’s state
1.  Find the `useState` lines at the top of the `App` function.
    *   `today`: The data for the Today card.
    *   `week`: The data for the 7-day grid.
    *   `form`: The text currently typed in the inputs.
2.  **Connect to C#:** Imagine these are just private fields at the top of your class.

### 3. See how State flows (Lifting State Up)
1.  Look at `<WeekGrid week={week} />`.
    *   We are passing the `week` state **down** to the child.
    *   In C#, this is `weekGrid.Render(this.week);`.
2.  What if a child needs to change the state?
    *   We pass the **setter** function down!
    *   React passes functions as arguments just like C# Delegates/Actions.

---

## Try it
1.  In `App.jsx`, change the default state of `today` (if it has a default) or the form.
    *   Example: `useState({ ... default data ... })`
2.  Run the app and see if it starts differently.

---

## Check yourself
-   Why can't we just write `count = count + 1` in React?
-   What does `[x, setX]` actually mean? (Hint: It's unpacking a package of two items).

---

## 📚 External Resources

**For React Logic:**
*   [React.dev - State: A Component's Memory](https://react.dev/learn/state-a-components-memory) (Read this first!)
*   [React.dev - Your First Component](https://react.dev/learn/your-first-component)

**Next lesson:** `09. Fetching Data and Normalizing Shapes`
How do we fill that state with real data from the API?
