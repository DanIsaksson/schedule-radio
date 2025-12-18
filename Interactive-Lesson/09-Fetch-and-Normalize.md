# 09. Fetching Data and Normalizing Shapes

## What you’ll learn
- Explain how `App` fetches data (Async/Await).
- Understand `.map()` (The cooler version of `foreach`).
- Why we "Normalize" (clean up) data before using it.

## Prerequisites
- `08. App State`
- Basic C# Loops (`foreach`).

---

## Mental model: Ordering Pizza (Async/Await)
Fetching data from an API is not instant. It takes time.

**Synchronous (Bad UX):**
You call the pizza place. You sit on the phone in silence for 30 minutes until they deliver. You can't do anything else.

**Asynchronous (Good UX):**
You call, place the order, and hang up. You watch TV (Run other code). When the doorbell rings (Promise resolves), you get the pizza.

**The Code:**
```javascript
async function loadPizza() {
    // "await" means "Pause THIS function, but let the rest of the app keep running"
    const response = await fetch("/api/pizza"); 
    const data = await response.json();
    setPizza(data); // Update state when it arrives
}
```

---

## 🟢 Green Coder Tips: `.map()`
You will see this everywhere in React. It is how we transform lists.

**C# `foreach`:**
```csharp
var numbers = new List<int> { 1, 2, 3 };
var doubled = new List<int>();
foreach(var n in numbers) {
    doubled.Add(n * 2);
}
```

**JavaScript `.map()`:**
```javascript
const numbers = [1, 2, 3];
const doubled = numbers.map(n => n * 2); // Returns [2, 4, 6]
```
*   It takes a list.
*   It runs the function on **every item**.
*   It returns a **new list** with the results.
*   We use this to turn a list of Data (`[{id:1}, {id:2}]`) into a list of HTML (`[<div>1</div>, <div>2</div>]`).

---

## Guided walkthrough

### 1. Find `loadToday` and `loadWeek`
1.  Open `frontend/src/App.jsx`.
2.  Locate `loadToday` or `loadWeek`.
3.  See the `async` keyword? That tells JS "This function will have pauses".
4.  See `await fetch(...)`? That's the pause.

### 2. Inspect the normalization
1.  The API returns C# style data: `PascalCase` (`Hours`, `Minutes`).
2.  React prefers JS style: `camelCase` (`hours`, `minutes`).
3.  Look for the code that transforms the data. It often uses `.map()` to loop through the C# list and create a new JS object for each item.

> **Why?** If the backend changes `Hours` to `HourList`, we only have to fix it here in the "Translator" function. The rest of the app is safe.

---

## Try it
1.  In `App.jsx`, find where `Minutes` is mapped.
2.  Break it intentionally: `minutes: h.Minutes` -> `minutes: h.WrongName`.
3.  Run the app. What happens? (You probably get an error or empty list).

---

## Check yourself
-   Why do we use `await` when fetching?
-   If you have a list of 5 bookings, and you `.map()` them, how many items are in the new list? (Hint: Always 5).

---

## 📚 External Resources

**For Async Logic:**
*   [JavaScript.info - Async/Await](https://javascript.info/async-await) (Excellent tutorial)
*   [JavaScript.info - Promises](https://javascript.info/promise-basics) (The underlying tech)

**For Array Methods:**
*   [MDN - Array.prototype.map()](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/map)

**Next lesson:** `10. Admin Booking Flow` (Skipped in this interactive edit, proceed to Labs).
