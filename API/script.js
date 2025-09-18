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