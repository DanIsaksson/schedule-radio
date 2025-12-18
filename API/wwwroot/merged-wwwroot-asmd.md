<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>Scheduler Radio · Demo UI</title>
  <!--
    Very light-weight styling is kept in style.css so the HTML stays clean.
    You can safely replace the stylesheet with something richer later.
  -->
  <link rel="stylesheet" href="style.css" />
</head>
<body>
  <!--
    ---------------
    Header Section
    ---------------
    Only a heading for now – enough to show the user what this page is.
  -->
  <header>
    <h1>Today’s Schedule</h1>
  </header>

  <!--
    ---------------
    Action Section
    ---------------
    A single button that triggers a fetch() to the backend.
    The "id" is referenced in script.js.
  -->
  <section>
    <button id="loadBtn">Load from API</button>
  </section>

  <!--
    ---------------
    Output Section
    ---------------
    <pre> preserves whitespace so our JSON is readable.
  -->
  <section>
    <pre id="output">Click the button to load data...</pre>
    <ul id="scheduleList" class="schedule">
      <li>Click the button to load data...</li>
    </ul>
  </section>

  <!--
    Front-end logic loaded as plain JS; no bundler required.
    Keep the <script> tag at the end so HTML renders first.
  -->
  <script src="script.js"></script>
</body>
</html>

// script.js – minimal front-end logic for Scheduler Radio
// -------------------------------------------------------
// 1. Wait for the button click
// 2. Call the back-end endpoint /schedule/today
// 3. Render the JSON (or error) into the <pre id="output"> element
// -------------------------------------------------------
// TEST FOR JS SCRIPT WORKING: console.log("Hello from script.js!");
// Helper => Reference the static elements once so we don’t query every time
const btnLoad = document.getElementById("loadBtn");
const preOut = document.getElementById("output");

// Click handler – uses async/await for clarity
btnLoad.addEventListener("click", async () => {
  // TEST, WORKED: console.log("Button was clicked!");
  // Reset UI to a loading state
  preOut.textContent = "Loading…";

  try {
    // NOTE: relative URL means “same origin / same port”
    // That works as long as index.html is served by the ASP-NET host (not via file://).
    // const response = await fetch("/schedule/today");
    const response = await fetch("http://localhost:5219/schedule/today");

    // Handle HTTP error statuses explicitly so the catch{} only handles network errors
    if (!response.ok) {
      // Build a friendlier message for the user
      throw new Error(`Server responded ${response.status} ${response.statusText}`);
    }

    // Parse JSON into an object
    const payload = await response.json();

    // Since payload is equal to the result from the constant "response" which fetches schedule/today
    // the console.log(payload); below provides the... data of the object, it expands and inspects the object.
    // THIS^: console.log(payload);

    // Pretty-print the object – JSON.stringify spacing = 2
    preOut.textContent = JSON.stringify(payload, null, 2);

    // Render simple list
    const list = document.getElementById("scheduleList");
    list.innerHTML = "";
    payload.hours = (payload.hours || payload.Hours || [])
      // ensure every object has a lowercase .hour property for easier JS work
      .map(h => ({ ...h, hour: h.hour ?? h.Hour }))
      // keep only afternoon hours
      .filter(h => h.hour >= 12);
    payload.hours.forEach(h => {
      // TEST FOR OUTPUTTING EACH HOUR (h) IN BROWSER CONSOLE>>>>>>: console.log(h);
      const li = document.createElement("li");
      const time = document.createElement("span");
      time.className = "time";
      time.textContent = `${String(h.hour ?? h.Hour).padStart(2, "0")}:00`;
      const status = document.createElement("span");
      status.className = "status";
      const booked = (h.minutes ?? h.Minutes).some(m => m);
      status.textContent = booked ? "Booked" : "Free";
      li.appendChild(time);
      li.appendChild(status);
      list.appendChild(li);
    });
  } catch (err) {
    // Any fetch/network exception lands here
    preOut.textContent = `Error: ${err.message || err}`;
  }
});


/*
 * API Static Stylesheet
 *
 * Purpose: Provides minimal styling for static HTML pages served directly by the API
 * (e.g., fallback pages or simple test views).
 *
 * Design Choice: Kept ultra-minimal to be lightweight and distinct from the rich React frontend.
 */

/* Global Reset & Typography */
body {
  font-family: system-ui, sans-serif;
  margin: 2rem;
  background: #f7f7f7;
  /* Light grey background for readability */
  color: #222;
  /* Dark grey text for contrast */
}

button {
  padding: 0.5rem 1rem;
  font-size: 1rem;
  cursor: pointer;
}

/* Code/Debug Output Styling */
pre {
  background: #fff;
  padding: 1rem;
  border: 1px solid #ddd;
  border-radius: 4px;
  overflow-x: auto;
}

/*
 * Simple Schedule List
 * Used for displaying a list of events in a non-grid format.
 */
.schedule {
  list-style: none;
  padding: 0;
  margin-top: 1rem;
  background: #fff;
  border: 1px solid #ddd;
  border-radius: 4px;
}

.schedule li {
  padding: 0.5rem 1rem;
  border-bottom: 1px solid #eee;
  display: flex;
  justify-content: space-between;
  /* Aligns time left, status right */
}

.schedule li:last-child {
  border-bottom: none;
}

.schedule .time {
  font-weight: 600;
}

.schedule .status {
  font-size: 0.9rem;
  color: #888;
  /* Muted color for secondary info */
}


