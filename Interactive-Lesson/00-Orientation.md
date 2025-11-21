# 00. Orientation and System Map

## What you’ll learn
- Describe the three main parts of this project: **browser UI**, **API**, and **database**.
- Map a **URL in the browser** to **React code** and to **API endpoints**.
- **How to use the "Split-View" learning system.**

---

## The "Split-View" Learning System
This codebase is designed to be read alongside these lessons.
1.  **Find IDs in Code**: You will see tags like `[Startup.Backend.1]` in the code comments.
2.  **Match with Lesson**: Open the corresponding lesson file (e.g., `01-Program-Startup.md`) and find the matching header.
3.  **Learn**: Read the explanation in the lesson while looking at the real code.

---

## Prerequisites
- Basic C# familiarity: you know what a class (`public class`) and a method (`public void Run()`) are.
- **No previous React/Web experience required.** We will translate everything to C# concepts you already know.

---

## Mental model: The Restaurant Metaphor
Since you've built console apps, you're used to the program running on *your* computer and asking *you* for input. Web apps are different because the parts are split up. Think of it like a restaurant:

1.  **The Client (React/Browser) = The Customer**
    *   The customer sits at the table and looks at the *Menu* (UI).
    *   They decide what they want but *cannot* go into the kitchen to cook it.
    *   They just say: "I want the Steak (Data)."

2.  **The API (.NET) = The Waiter**
    *   The waiter takes the order from the customer.
    *   They check if the order makes sense ("We don't serve rocks").
    *   They run to the kitchen, shout the order, grab the food, and bring it back.
    *   **Crucial:** The waiter *doesn't cook*. They just move messages back and forth.

3.  **The Database (SQLite) = The Kitchen**
    *   This is where the raw ingredients (Data) live.
    *   It's a cold, dark room full of shelves (Tables).
    *   Only the Waiter (API) is allowed in here.

**In this project:**
*   You (in the browser) click "Book Slot".
*   React sends a message to .NET (The Waiter).
*   .NET saves the booking in SQLite (The Kitchen).
*   .NET tells React "It's done!".
*   React shows you the green checkmark.

---

## 🟢 Green Coder Tips: Glossary
Terms you will see everywhere that might look scary but aren't:

*   **Endpoint:** A specific "function" the Waiter can perform. Like `GetMenu()` or `PlaceOrder()`. In code, it looks like a URL: `/api/menu`.
*   **Request:** The Customer asking for something.
*   **Response:** The Waiter bringing it back (or saying "We're out of that").
*   **JSON:** The format of the "ticket" the waiter writes on. It's just text.
    *   C#: `public class Dog { public string Name = "Rex"; }`
    *   JSON: `{ "Name": "Rex" }`
    *   It's just a way to send objects as text strings.

---

## Guided walkthrough

### 1. Run everything and just observe
1.  Start the backend API project.
2.  Start the frontend (Vite dev server).
3.  Visit `http://localhost:5173/`.

### 2. Try the ID System
1.  Open `API/Program.cs`.
2.  Find the comment `// [Startup.Backend.1] Main Switch`.
3.  Open `Interactive-Lesson/01-Program-Startup-and-Pipeline.md`.
4.  Find the header `## [Startup.Backend.1] Main Switch`.
5.  Read the explanation.

### 3. Explore the Frontend Entry Point
1.  Open `frontend/src/main.jsx`.
2.  Find `[Startup.Frontend.1]`.
3.  Note that it links to `07-Frontend-Bootstrap-and-Routing.md`.

---

## Check yourself
-   Can you explain, in 2–3 sentences, how the browser, API, and database work together?
-   Did you successfully find the `[Startup.Backend.1]` tag in the code?
-   Did you find the matching explanation in the lesson?

---

## Next Steps
Start with **Lesson 01** to understand how the backend boots up.
-> `Interactive-Lesson/01-Program-Startup-and-Pipeline.md`
