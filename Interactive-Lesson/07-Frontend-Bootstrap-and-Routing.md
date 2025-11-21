# 07. Frontend Bootstrap and Hash Routing

## What you’ll learn
- Explain how the React app boots in the browser.
- Describe how **hash-based routing** works (it's simpler than you think!).
- Understand `const` vs `let`.

## Prerequisites
- `00. Orientation and System Map`
- **No previous React Router knowledge required.** We are using "Vanilla" routing here.

---

## Mental model: The Switchboard
Imagine a big physical switchboard on the wall.
*   **The URL Hash** (`#/admin`) is the label on the incoming wire.
*   **The Router** is the operator who sees the label and plugs it into the right screen.

Unlike complex routing libraries, this app just looks at one thing: `window.location.hash`.
It's literally the text after the `#` in your browser bar.

---

## 🟢 Green Coder Tips: JavaScript Variables
Coming from C# (`int`, `string`, `var`), JS variables might confuse you.

1.  **`const` (Constant):**
    *   Use this for 95% of your variables.
    *   It means "The **reference** won't change".
    *   *C# Equivalent:* `readonly var x = ...`
    *   *Gotcha:* You *can* change the insides of a `const` object, you just can't point `x` to a whole new object.

2.  **`let`:**
    *   Use this only if you plan to reassign it (like in a loop or a counter).
    *   *C# Equivalent:* `var x = ...`

3.  **`var`:**
    *   **Never use this.** It's the old, broken way (pre-2015). It has weird scoping rules. Pretend it doesn't exist.

---

## Guided walkthrough

### 1. See where React mounts
1.  Open `frontend/index.html`.
2.  Find `<div id="root"></div>`.
    *   This is the empty box where React will paint your entire app.
    *   Everything outside this div is just static HTML.

### 2. Follow the bootstrap in `main.jsx`
1.  Open `frontend/src/main.jsx`.
2.  Look for `ReactDOM.createRoot(...)`.
    *   This is the "Big Bang". It tells React: "Take control of the `root` div."
3.  Identify the `<Router>` component wrapping `<App />`.
    *   We wrap App in Router so App knows what URL we are at.

### 3. Understand hash-based routing
1.  In `main.jsx`, look at the `Router` function.
2.  Find `window.location.hash`.
    *   Try typing `window.location.hash` in your browser's Console tab (F12) while running the app. You'll see `"#/"` or `"#admin"`.
3.  See how it passes `route` down to `App`.
    *   In C#, this would be like passing a parameter to a child class constructor.

---

## Try it
1.  Run the frontend.
2.  Manually change the URL in the browser to `http://localhost:5173/#/pizza`.
3.  See what happens. (Likely nothing or a default view, because the "Switchboard" doesn't have a plug for "pizza").

---

## Check yourself
-   Why do we use `const` instead of `var`?
-   If you change the code to look for `window.location.pathname` instead of `hash`, would `#/admin` still work? (Hint: No, hash is special).

---

## 📚 External Resources

**For JavaScript Syntax:**
*   [W3Schools - JS Let](https://www.w3schools.com/js/js_let.asp)
*   [W3Schools - JS Const](https://www.w3schools.com/js/js_const.asp)
*   [W3Schools - Window Location](https://www.w3schools.com/js/js_window_location.asp)

**Next lesson:** `08. App Layout, Components, and State`
Now we will look at `App.jsx` and learn the most important concept in React: **State**.
