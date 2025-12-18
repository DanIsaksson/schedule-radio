// A.1 App.jsx – Main "View + Controller" for the Radio Scheduler frontend.
// - Renders the public website (Home, Schedule) and the internal Admin panel for bookings.
// - Talks to the ASP.NET Core backend using fetch('/db/...') to load schedules and create bookings.
// - Ties into codemaps:
//   * "React Radio Scheduler Application Bootstrap and Navigation Flow" (how Router passes route here) [B.5*].
//   * "Schedule Data Loading Flow - React Frontend to Database Backend" (how we call /db/schedule/* and update state) [B.10*].
//   * "Radio Scheduler Booking Creation Flow" (admin form → /db/event/post → DB → UI refresh) [B.15*].

// C.1 React hooks analogy for this file:
// - useState = the component's **memory** about changing data (today, week, form fields, errors).
// - useEffect = doing things with the **Outside World** (HTTP calls to the backend) after React draws the UI.
import React, { useEffect, useState } from 'react'
import { useAuth } from './auth/AuthContext.jsx'

/**
 * A.2 HourRow Component
 * 
 * Represents a single row in the Admin "Today" view.
 * - Shows one hour label (e.g., 09:00) and how many of its 60 minutes are booked.
 * - Uses the same minute-grid idea as the backend ScheduleData (7 days × 24 hours × 60 minutes).
 * - [A.3c] UI view of the minute grid defined in Program.cs A.3 and Models/ScheduleModels.cs A.3a, painted by ScheduleProjectionDb A.3b.
 * 
 * B.1 Data flow:
 * - Props come from the `today` object in App component.
 * - `today` is built from the /db/schedule/today endpoint (codemap: Schedule Data Loading Flow → Today Schedule API Call).
 * - `minutes` is an array of 60 booleans:
 *     false = default music is playing (no program booked).
 *     true  = some show/host is booked for that minute in the studio.
 * 
 * Props (Parameters):
 * - hour: The hour number (0-23).
 * - minutes: The 60-minute booking array described above.
 */
function MinuteMatrix({ hour, minutes, onMinuteClick, selection }) {
  // Helper to check if a minute is in the current selection range
  const isSelected = (m) => {
    if (!selection) return false
    const { start, end } = selection
    if (start === null) return false
    if (end === null) return m === start
    // Fix: Use <= to include the last minute in the visual selection
    return m >= Math.min(start, end) && m <= Math.max(start, end)
  }

  return (
    <div className="minute-matrix-container">
      <h3>Välj tid för kl {String(hour).padStart(2, '0')}:00</h3>
      <div className="minute-matrix">
        {minutes.map((isBooked, i) => (
          <div
            key={i}
            className={`minute-cell ${isBooked ? 'booked' : 'free'} ${isSelected(i) ? 'selected' : ''}`}
            onClick={() => onMinuteClick(hour, i)}
            title={`Minut ${i}`}
          >
            <span className="minute-number">{i}</span>
          </div>
        ))}
      </div>
    </div>
  )
}

/**
 * A.3 HourCell Component
 * 
 * One square in the weekly grid that represents a single hour for a given day.
 * - Visually encodes how "busy" the hour is (free/partial/busy) so producers see density at a glance.
 * - Generates a tooltip listing the booked ranges within the hour (e.g., 00–15, 30–45).
 * 
 * B.2 Domain tie-in:
 * - Minutes that are not booked fall back to the station's default music.
 * - Booked ranges represent shows/segments/guests in a studio, using the same minute grid as the backend.
 * - [A.3c] Visualizes the same minute-grid structure as HourRow and the backend A.3/A.3a/A.3b.
 */
function HourCell({ date, hour, minutes, bookings = [], onPick }) {
  // Calculate how many minutes are booked.
  const bookedMinutesCount = minutes.filter(Boolean).length
  const bookedFraction = bookedMinutesCount / 60
  const statusLevel = bookedFraction === 0 ? 'free' : bookedFraction < 0.5 ? 'partial' : 'busy'

  // If we have bookings with titles, show them
  const hasBookings = bookings && bookings.length > 0

  return (
    <div
      className={`cell ${statusLevel} ${hasBookings ? 'has-content' : ''}`}
      onClick={() => onPick?.(date, hour)}
    >
      {hasBookings ? (
        bookings.map((b, i) => (
          <div key={i} className="booking-label small" title={`${b.title} (${b.startMinute}-${b.endMinute})`}>
            <span className="time-range">{String(b.startMinute).padStart(2, '0')}-{String(b.endMinute).padStart(2, '0')}</span>
            <span className="booking-title"> {b.title}</span>
          </div>
        ))
      ) : (
        <div className="empty-label">Musik</div>
      )}
    </div>
  )
}

/**
 * A.4 DayColumn Component
 * 
 * Renders one vertical column for a specific calendar day, containing 24 HourCell components.
 * 
 * B.5 Data flow:
 * - `day` comes from the 7-day schedule structure fetched by App.loadWeek (codemap: Schedule Data Loading Flow).
 * - It may use either `day.date` or `day.Date` depending on how the backend serialized the JSON.
 */
function DayColumn({ day, onPick }) {
  // Create a Date object from the day's data.
  // The '??' operator (Null Coalescing) works exactly like in C#.
  // It checks 'day.date', and if it's null, it uses 'day.Date' (handles case sensitivity differences).
  const dateObject = new Date(day.date ?? day.Date)

  // Check if this column represents the current actual date (Today).
  const isToday = dateObject.toDateString() === new Date().toDateString()

  // Format the date for display (e.g., "Mon, Nov 18").
  const formattedDateString = dateObject.toLocaleDateString('sv-SE', { weekday: 'short', month: 'short', day: 'numeric' })

  // B.6 Normalize the hours data from the API.
  // - We use Array.map (like LINQ Select) to transform raw hour objects into a consistent structure.
  // - Handles capitalization differences from the backend (hour vs Hour, minutes vs Minutes).
  const hours = (day.hours ?? day.Hours ?? []).map(hourData => ({
    hour: hourData.hour ?? hourData.Hour,
    minutes: (hourData.minutes ?? hourData.Minutes) ?? Array(60).fill(false),
    bookings: (hourData.bookings ?? hourData.Bookings) ?? []
  }))

  return (
    <div className={`day ${isToday ? 'today' : ''}`}>
      <div className="day-header">{formattedDateString}</div>
      <div className="day-grid">
        {hours.map(hourData => (
          <HourCell
            key={hourData.hour}
            date={day.date ?? day.Date}
            hour={hourData.hour}
            minutes={hourData.minutes}
            bookings={hourData.bookings}
            onPick={onPick}
          />
        ))}
      </div>
    </div>
  )
}

/**
 * A.5 WeekGrid Component
 * 
 * Renders the 7-day schedule view by mapping the week's data to DayColumn components.
 * - This visualizes the same 7-day window the backend builds in ScheduleProjectionDb.BuildSevenDaySchedule.
 * 
 * B.7 Data source:
 * - `week` comes from App.loadWeek, which calls /db/schedule/7days on the backend.
 */
function WeekGrid({ week, onPick }) {
  // C.9 Safe navigation: week?.days is like C#'s ?. operator; if week is null, result is undefined.
  const days = week?.days ?? week?.Days ?? []

  return (
    <div className="week-grid">
      {/* Iterate over each day and render a DayColumn */}
      {days.map(dayData => (
        <DayColumn
          key={dayData.date ?? dayData.Date}
          day={dayData}
          onPick={onPick}
        />
      ))}
    </div>
  )
}

function DayBookingsColumn({ day }) {
  const dateObject = new Date(day.date ?? day.Date)
  const isToday = dateObject.toDateString() === new Date().toDateString()
  const formattedDateString = dateObject.toLocaleDateString('sv-SE', { weekday: 'short', month: 'short', day: 'numeric' })

  const rawHours = day.hours ?? day.Hours ?? []

  const pad2 = (n) => String(n).padStart(2, '0')

  const formatTimeRange = (hour, startMinute, endMinute) => {
    const startText = `${pad2(hour)}:${pad2(startMinute)}`

    const endHour = endMinute === 60 ? hour + 1 : hour
    const endMinuteFixed = endMinute === 60 ? 0 : endMinute
    const endText = endHour === 24 ? '24:00' : `${pad2(endHour)}:${pad2(endMinuteFixed)}`

    return `${startText}-${endText}`
  }

  const bookings = rawHours
    .flatMap(h => {
      const hour = h.hour ?? h.Hour
      const list = h.bookings ?? h.Bookings ?? []
      return list.map((b, i) => ({
        key: `${hour}-${b.startMinute ?? b.StartMinute}-${b.endMinute ?? b.EndMinute}-${i}`,
        hour,
        title: b.title ?? b.Title ?? 'Untitled',
        startMinute: b.startMinute ?? b.StartMinute ?? 0,
        endMinute: b.endMinute ?? b.EndMinute ?? 0
      }))
    })
    .sort((a, b) => (a.hour - b.hour) || (a.startMinute - b.startMinute))

  return (
    <div className={`day ${isToday ? 'today' : ''}`}>
      <div className="day-header">{formattedDateString}</div>
      <div className="day-bookings">
        {bookings.map(b => (
          <div key={b.key} className="booking-item">
            <span className="booking-time">{formatTimeRange(b.hour, b.startMinute, b.endMinute)}</span>
            <span className="booking-title">{b.title}</span>
          </div>
        ))}
      </div>
    </div>
  )
}

function WeekBookingsOnly({ week }) {
  const days = week?.days ?? week?.Days ?? []

  return (
    <div className="week-grid week-bookings-grid">
      {days.map(dayData => (
        <DayBookingsColumn
          key={dayData.date ?? dayData.Date}
          day={dayData}
        />
      ))}
    </div>
  )
}

/**
 * SiteHeader Component
 * 
 * Navigation bar visible on public pages.
 * It uses simple anchor tags (#/...) for navigation, which our Router (in main.jsx) listens to.
 */
function SiteHeader() {
  return (
    <header className="site-header">
      <div className="brand">Radio<span className="accent">Play</span></div>
      <nav className="nav">
        <a href="#/">Hem</a>
        <a href="#/schedule">Tablå</a>
        {/* aria-disabled: Tells screen readers that these links are not active yet */}
        <a href="#/shows" aria-disabled>Program</a>
        <a href="#/podcasts" aria-disabled>Poddar</a>
      </nav>
    </header>
  )
}

function StaffHeader({ user, onLogout }) {
  return (
    <header className="site-header">
      <div className="brand">Radio<span className="accent">Portal</span></div>
      <nav className="nav">
        <a href="#/">Hem</a>
        {user ? (
          <>
            <a href="#/bookings">Bokningar</a>
            <a href="#/portal/me">Konto</a>
            <a href="#/portal/change-password">Byt lösenord</a>
            <a
              href="#/"
              onClick={(e) => {
                e.preventDefault()
                onLogout?.()
              }}
            >
              Logga ut
            </a>
          </>
        ) : (
          <a href="#/portal">Logga in</a>
        )}
      </nav>
    </header>
  )
}

/**
 * SiteFooter Component
 * 
 * Simple footer with dynamic year.
 */
function SiteFooter() {
  return (
    <footer className="site-footer">
      {/* JavaScript expression inside {} to get the current year */}
      <div>© {new Date().getFullYear()} RadioPlay</div>
      <div className="socials">•</div>
    </footer>
  )
}

/**
 * HeroNowPlaying Component
 * 
 * A static "Hero" section (banner) showing what's currently playing.
 * In a real app, this data would likely come from an API (props), but here it's hardcoded.
 */
function HeroNowPlaying() {
  return (
    <section className="hero">
      <div className="hero-art" />
      <div className="hero-meta">
        <div className="badge-live">LIVE</div>
        <h1>Morning Energy with Alex</h1>
        <p className="muted">Vardagar 06:00–10:00 · Värd: Alex</p>
        <a className="btn-accent" href="#/">▶ Spela</a>
      </div>
    </section>
  )
}

function FeaturedRow() {
  const featuredItems = [
    { id: 1, title: 'Drive Time', time: '16:00', img: '' },
    { id: 2, title: 'Late Night Chill', time: '22:00', img: '' },
    { id: 3, title: 'Top 40', time: '12:00', img: '' },
  ]

  return (
    <section className="featured container-wide">
      {/* Render a list of cards. 'key' is required by React for performance (like a primary key). */}
      {featuredItems.map(item => (
        <div className="card card-shadow" key={item.id}>
          <div className="thumb" />
          <div className="card-meta">
            <h3>{item.title}</h3>
            <p className="muted">Idag {item.time}</p>
          </div>
        </div>
      ))}
    </section>
  )
}

/**
 * SchedulePreview Component
 * 
 * A compact view of the schedule for the home page.
 */
function SchedulePreview({ week }) {
  // Local state for collapsing the preview
  const [showWeek, setShowWeek] = useState(true)

  return (
    <section className="schedule-preview container-wide">
      <div
        className="preview-header collapsible-header"
      >
        <h2>Sänds denna vecka</h2>
        <a href="#/schedule" className="link">Hela tablån →</a>
      </div>

      <button
        type="button"
        className="preview-toggle"
        onClick={() => setShowWeek(v => !v)}
        aria-expanded={showWeek}
      >
        <span className="preview-toggle-icon" aria-hidden="true">{showWeek ? '−' : '+'}</span>
      </button>

      {showWeek && (
        <>
          <div className="schedule-note">Har vi inget som händer på studion så kör vi musik. Kom och lyssna dygnet runt!</div>
          <div className="schedule-scroll-container">
            <WeekBookingsOnly week={week} />
          </div>
        </>
      )}
    </section>
  )
}

/**
 * AdminPanel Component
 * 
 * The "Dashboard" for internal users to manage bookings.
 * It receives data (week, today) and actions (submitBooking, loadToday) as props from the App component.
 * This is a "Controlled Component" because the form state is managed by React (passed down via 'form' prop).
 */
function AdminPanel({ week, today, form, onPickHour, onMinuteClick, onChange, submitBooking, loading, loadToday, error, successMessage, selection }) {
  // Derived logic for Admin details
  const isLive = form.eventType === 'Live'
  const studio = isLive ? (form.hostCount === 1 ? 'Studio 1 (Billigare)' : 'Studio 2') : 'Standard'
  const extraCost = isLive && form.hasGuest ? 'Extra Kostnad: Gästtransport' : ''

  // Local state for collapsing the week view
  const [showWeek, setShowWeek] = useState(false)

  return (
    <>
      {/* Toolbar Section */}
      <section className="controls">
        <button onClick={loadToday} disabled={loading}>
          {loading ? 'Laddar…' : 'Uppdatera Idag'}
        </button>
      </section>

      {/* Feedback Section: Show error or success messages if they exist */}
      {error && <div className="alert error">Fel: {error}</div>}
      {successMessage && <div className="alert ok">{successMessage}</div>}

      <section className="grid">
        {/* Week View: Spans full width */}
        {week && (
          <div className="card" style={{ gridColumn: '1 / -1' }}>
            <div
              className="collapsible-header"
              onClick={() => setShowWeek(!showWeek)}
              style={{ cursor: 'pointer', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}
            >
              <h2 style={{ margin: 0 }}>Vecka (Idag + 6 dagar)</h2>
              <span style={{ fontSize: '1.5rem' }}>{showWeek ? '−' : '+'}</span>
            </div>
            {showWeek && (
              <div style={{ marginTop: '1rem' }}>
                <WeekGrid week={week} onPick={onPickHour} />
              </div>
            )}
          </div>
        )}

        {/* Today's Detailed View */}
        <div className="card">
          <h2>Idag (Välj timme för att boka)</h2>
          {!today ? (
            <p>Laddar…</p>
          ) : (
            <>
              <p className="date">{new Date(today.date ?? Date.now()).toLocaleDateString()}</p>

              {/* Hour Selection List */}
              <div className="hour-list">
                {today.hours.map(hourData => {
                  const bookedMinutesCount = hourData.minutes.filter(Boolean).length
                  const isBooked = bookedMinutesCount > 0
                  const isFullyBooked = bookedMinutesCount === 60
                  const isSelected = form.hour === hourData.hour
                  return (
                    <button
                      key={hourData.hour}
                      className={`hour-btn ${isBooked ? 'has-bookings' : ''} ${isSelected ? 'active' : ''}`}
                      onClick={() => onChange({ target: { name: 'hour', value: hourData.hour } })}
                    >
                      <span className="hour-time">{String(hourData.hour).padStart(2, '0')}:00</span>
                      {isBooked && (
                        <span className="hour-status">{isFullyBooked ? 'HELBOKAD' : `${bookedMinutesCount}/60`}</span>
                      )}
                    </button>
                  )
                })}
              </div>

              {/* Minute Matrix for Selected Hour */}
              {today.hours.find(h => h.hour === form.hour) && (
                <MinuteMatrix
                  hour={form.hour}
                  minutes={today.hours.find(h => h.hour === form.hour).minutes}
                  onMinuteClick={onMinuteClick}
                  selection={selection && selection.hour === form.hour ? selection : null}
                />
              )}
            </>
          )}
        </div>

        {/* Booking Form */}
        <div className="card">
          <h2>Ny bokning</h2>
          <form onSubmit={submitBooking} className="form">
            <label>
              Titel
              <input type="text" name="title" value={form.title} onChange={onChange} required />
            </label>
            <label>
              Typ
              <select name="eventType" value={form.eventType} onChange={onChange}>
                <option value="PreRecorded">Förinspelat</option>
                <option value="Live">Live</option>
              </select>
            </label>

            {isLive && (
              <>
                <label>
                  Antal Värdar
                  <input type="number" name="hostCount" min="1" value={form.hostCount} onChange={onChange} />
                </label>
                <label className="checkbox">
                  <input type="checkbox" name="hasGuest" checked={form.hasGuest} onChange={onChange} />
                  Har Gäst?
                </label>
                <div className="info-box">
                  <p><strong>Studio:</strong> {studio}</p>
                  {extraCost && <p className="warn">{extraCost}</p>}
                </div>
              </>
            )}

            <label>
              Datum
              {/* Controlled Input: value comes from state, onChange updates state */}
              <input type="date" name="date" value={form.date} onChange={onChange} />
            </label>
            <label>
              Timme
              <input type="number" name="hour" min="0" max="23" value={form.hour} onChange={onChange} />
            </label>
            <label>
              Startminut
              <input type="number" name="startMinute" min="0" max="59" value={form.startMinute} onChange={onChange} />
            </label>
            <label>
              Slutminut
              <input type="number" name="endMinute" min="1" max="60" value={form.endMinute} onChange={onChange} />
            </label>
            <div className="form-actions">
              <button type="submit">Boka</button>
            </div>
          </form>
        </div>
      </section>
    </>
  )
}

/**
 * A.6 App Component
 * 
 * The Root Controller-View of the React side.
 * - Owns all main state for the scheduler UI (today, week, booking form, loading/error flags).
 * - Decides which "page" to show (Home, Schedule, Admin) based on the current route from the Router in main.jsx.
 * 
 * Codemap connections:
 * - "React Radio Scheduler Application Bootstrap and Navigation Flow":
 *   Router listens to hash changes and passes `route` down into this component.
 * - "Schedule Data Loading Flow - React Frontend to Database Backend":
 *   useEffect → loadToday/loadWeek → fetch('/db/schedule/...') → backend → DbContext → ScheduleProjectionDb → JSON → setToday/setWeek.
 * - "Radio Scheduler Booking Creation Flow":
 *   Admin form → submitBooking → fetch('/db/event/post') → EventDbEndpoints + EventActionsDb → SQLite → Promise.all reloads schedule.
 * 
 * Props:
 * - route: The current URL hash ("#/", "#/schedule", "#/admin"), passed from Router in main.jsx.
 */
export default function App({ route = '#/' }) {
  const { user, roles, mustChangePassword, isLoading: authLoading, refreshMe, login, logout } = useAuth()
  const isLoggedIn = Boolean(user)

  // B.8 State definitions (the component's "memory").
  // - today: one DaySchedule for the Admin "Today" list (built from /db/schedule/today).
  // - week: full 7-day schedule window for grids and previews (from /db/schedule/7days).
  // - loading: whether we are currently fetching data from the backend.
  // - error / successMessage: feedback banners shown at the top of the Admin panel.
  const [today, setToday] = useState(null)
  const [week, setWeek] = useState(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')
  const [successMessage, setSuccessMessage] = useState('')

  const [portalEmail, setPortalEmail] = useState('')
  const [portalPassword, setPortalPassword] = useState('')
  const [portalError, setPortalError] = useState('')
  const [portalIsSubmitting, setPortalIsSubmitting] = useState(false)

  const [oldPassword, setOldPassword] = useState('')
  const [newPassword, setNewPassword] = useState('')
  const [changePasswordError, setChangePasswordError] = useState('')
  const [changePasswordSuccessMessage, setChangePasswordSuccessMessage] = useState('')
  const [isChangingPassword, setIsChangingPassword] = useState(false)

  // B.9 [Portal.Me] Payment history state (the contributor "payment ledger" view).
  // What: Stores the rows returned by GET /api/contributor/payments (one row per month).
  // Why: Payment rows are persisted in the backend (ContributorPayments table) and the contributor needs a read-only overview.
  // Where:
  // - Loaded by loadPayments() [B.12d] in this file.
  // - Rendered in the "Konto" page (route #/portal/me) near the bottom of this file.
  const [payments, setPayments] = useState([])
  const [paymentsLoading, setPaymentsLoading] = useState(false)
  const [paymentsError, setPaymentsError] = useState('')

  // B.30 [Root] Form state: booking request we will send to the backend.
  // - Mirrors the parameters of the /db/event/post endpoint [B.15a/B.15b]:
  //   date, hour, startMinute, endMinute.
  // - These correspond directly to the minute grid used on the backend [A.3*]:
  //   * hour 0..23, minutes 0..60 where true = booked, false = default music.
  const [form, setForm] = useState({
    date: new Date().toISOString().slice(0, 10), // Today's date as YYYY-MM-DD
    hour: 12,
    startMinute: 0,
    endMinute: 30,
    title: '',
    eventType: 'PreRecorded',
    hostCount: 1,
    hasGuest: false
  })
  const [isPosting, setIsPosting] = useState(false)

  // Selection state for the "two-step loop"
  const [selection, setSelection] = useState(null) // { hour, start, end, step: 1|2 }

  const handleMinuteClick = (hour, minute) => {
    setSelection(prev => {
      // If starting new selection or switching hours, reset to step 1
      if (!prev || prev.step === 2 || prev.hour !== hour) {
        // Step 1: Set start minute
        // Fix: Set endMinute to same as startMinute (inclusive 1-minute duration visual)
        setForm(f => ({ ...f, hour, startMinute: minute, endMinute: minute }))
        return { hour, start: minute, end: null, step: 1 }
      } else {
        // Step 2: Set end minute
        // Ensure start < end
        const start = Math.min(prev.start, minute)
        const end = Math.max(prev.start, minute)
        // Fix: Set endMinute to inclusive index (user sees 0-19, not 0-20)
        setForm(f => ({ ...f, hour, startMinute: start, endMinute: end }))
        return { hour, start, end: minute, step: 2 }
      }
    })
  }

  // B.10 [Root] Load today's schedule (Admin "Today" list) in the schedule data loading lane.
  // - This is the frontend half of the Today flow in the "Schedule Data Loading Flow" codemap.
  // - End-to-end story [B.10]:
  //   App.loadToday [B.10] -> fetch('/db/schedule/today') -> Program.cs route mapping -> ScheduleDbEndpoints.BuildToday [B.10a]
  //   -> ScheduleProjectionDb.BuildSevenDaySchedule/BuildToday [B.10c] (paint minute grid from DB Events) -> JSON response -> setToday(...) here.
  //
  // C.10 'async' and 'await' work exactly like in C# async methods.
  const loadToday = async () => {
    setLoading(true); setError(''); setSuccessMessage('')
    try {
      // C.11 fetch: native browser call, equivalent to C# HttpClient.GetAsync().
      const res = await fetch('/db/schedule/today')
      if (!res.ok) throw new Error(`HTTP ${res.status}`)

      // C.12 Parse JSON (like JsonSerializer.DeserializeAsync<T>()).
      const data = await res.json()

      // B.11 Normalize data (handle inconsistent capitalization from API / backend).
      // - Depending on how ASP.NET serialized, properties may be `hours` or `Hours`, `minutes` or `Minutes`.
      // - We reshape into the structure our components expect:
      //   { date, hours: [ { hour, minutes: bool[60] }, ... ] }.
      const Hours = data.hours ?? data.Hours ?? []
      const normalizedHours = Hours.map(h => ({
        hour: h.hour ?? h.Hour,
        minutes: (h.minutes ?? h.Minutes) ?? Array(60).fill(false),
        bookings: (h.bookings ?? h.Bookings) ?? []
      }))

      setToday({ date: data.date ?? data.Date, hours: normalizedHours })
    } catch (e) {
      setError(e.message || String(e))
    } finally {
      // C.13 finally: ensure loading flag is turned off whether we succeeded or failed.
      setLoading(false)
    }
  }

  // B.12 Load the 7-day schedule window (used for week grids and home preview).
  // - Mirrors the /db/schedule/7days flow in the "Schedule Data Loading Flow" codemap.
  // - End-to-end story [B.10b]: App.loadWeek -> fetch('/db/schedule/7days') [B.10b] -> ScheduleDbEndpoints 7days handler [B.10b]
  //   -> ScheduleProjectionDb.BuildSevenDaySchedule [B.10c] -> JSON -> setWeek(data).
  const loadWeek = async () => {
    setLoading(true); setError('')
    try {
      const res = await fetch('/db/schedule/7days')
      if (!res.ok) throw new Error(`HTTP ${res.status}`)
      const data = await res.json()
      setWeek(data)
    } catch (e) {
      setError(e.message || String(e))
    } finally {
      setLoading(false)
    }
  }

  // B.12c [Portal.Me] Normalize payment rows from the backend (shape + casing).
  // What: Converts the API response into a consistent camelCase object for React rendering.
  // Why: ASP.NET may serialize properties as PascalCase (e.g. PeriodYear) while JS code typically uses camelCase.
  // Where: Used by loadPayments() [B.12d] when parsing GET /api/contributor/payments.
  const normalizePaymentRow = (row) => ({
    id: row.id ?? row.Id ?? null,
    periodYear: row.periodYear ?? row.PeriodYear ?? 0,
    periodMonth: row.periodMonth ?? row.PeriodMonth ?? 0,
    totalMinutes: row.totalMinutes ?? row.TotalMinutes ?? 0,
    eventCount: row.eventCount ?? row.EventCount ?? 0,
    baseAmount: row.baseAmount ?? row.BaseAmount ?? 0,
    eventBonusAmount: row.eventBonusAmount ?? row.EventBonusAmount ?? 0,
    vatAmount: row.vatAmount ?? row.VatAmount ?? 0,
    totalIncludingVat: row.totalIncludingVat ?? row.TotalIncludingVat ?? 0
  })

  // C.18 [Portal.Me] Presentation helpers for the payment history table.
  // What: Format raw values (year/month, minutes, money) into strings that are easy to scan.
  // Why: Keeps JSX clean so the "table view" stays focused on layout, not string math.
  // Where: Used inside the payment table rendering in the #/portal/me section.
  const formatYearMonth = (year, month) => {
    if (!year || !month) {
      return ''
    }

    return `${year}-${String(month).padStart(2, '0')}`
  }

  const formatDurationMinutes = (totalMinutes) => {
    const minutes = Number(totalMinutes) || 0
    const hours = Math.floor(minutes / 60)
    const remainder = minutes % 60

    if (hours <= 0) {
      return `${remainder} min`
    }

    if (remainder <= 0) {
      return `${hours} h`
    }

    return `${hours} h ${remainder} min`
  }

  const formatSek = (amount) => {
    const value = Number(amount) || 0
    return value.toLocaleString('sv-SE', { style: 'currency', currency: 'SEK' })
  }

  // B.12d [Portal.Me] Load payment history for the currently logged-in contributor.
  // What: Calls GET /api/contributor/payments and stores the result in `payments`.
  // Why: The backend calculates monthly payment summaries and persists them in SQLite; this page just reads and displays.
  // Where:
  // - API "door": API/Endpoints/ContributorEndpoints.cs (MapGet("/payments")).
  // - Trigger: useEffect [B.14c] when you navigate to #/portal/me (and auth is OK).
  // Failure modes:
  // - 401 => session expired -> refreshMe() clears auth -> redirect to #/portal.
  // - 403 => often must-change-password gate -> refreshMe() and redirect to #/portal/change-password.
  const loadPayments = async () => {
    setPaymentsLoading(true)
    setPaymentsError('')

    try {
      const res = await fetch('/api/contributor/payments', {
        credentials: 'include'
      })

      if (res.status === 401) {
        setPayments([])
        await refreshMe().catch(() => {})
        window.location.hash = '#/portal'
        return
      }

      if (res.status === 403) {
        setPayments([])
        const text = await res.text()

        const me = await refreshMe().catch(() => null)
        if (!me) {
          window.location.hash = '#/portal'
          return
        }

        if (me.mustChangePassword) {
          window.location.hash = '#/portal/change-password'
        }

        setPaymentsError(text || 'Du saknar behörighet.')
        return
      }

      if (!res.ok) {
        const text = await res.text()
        throw new Error(text || `HTTP ${res.status}`)
      }

      const data = await res.json()
      const rows = Array.isArray(data) ? data : []
      setPayments(rows.map(normalizePaymentRow))
    } catch (e2) {
      setPaymentsError(e2.message || String(e2))
    } finally {
      setPaymentsLoading(false)
    }
  }

  // B.13 Initial data load effect.
  // - Runs once when App is first created (like a constructor), because dependency array is [].
  // - Kicks off both schedule-loading flows so the UI can show Today + Week without a manual refresh.
  // - This ties into the codemap step: "Initial Schedule Data Loading from Backend".
  useEffect(() => {
    loadToday();
    loadWeek()
  }, [])

  useEffect(() => {
    if (route.startsWith('#/admin')) {
      window.location.hash = '#/bookings'
    }
  }, [route])

  useEffect(() => {
    if (authLoading) {
      return
    }

    const isPortalChangePassword = route.startsWith('#/portal/change-password')
    const isProtectedStaffRoute =
      route.startsWith('#/bookings') ||
      route.startsWith('#/admin') ||
      route.startsWith('#/portal/me') ||
      route.startsWith('#/portal/change-password')

    if (isLoggedIn && mustChangePassword && !isPortalChangePassword) {
      window.location.hash = '#/portal/change-password'
      return
    }

    if (!isLoggedIn && isProtectedStaffRoute) {
      window.location.hash = '#/portal'
      return
    }

    if (isLoggedIn && !mustChangePassword && route === '#/portal') {
      window.location.hash = '#/bookings'
    }
  }, [route, authLoading, isLoggedIn, mustChangePassword])

  // B.14c [Portal.Me] Side effect: auto-load payments when entering the contributor account page.
  // Why: Payments are "outside world" data (network) and should load automatically once the user is allowed past route guards.
  // Where: This effect runs when `route` or auth flags change; it calls loadPayments() [B.12d].
  useEffect(() => {
    if (authLoading) {
      return
    }

    if (!route.startsWith('#/portal/me')) {
      return
    }

    if (!isLoggedIn) {
      return
    }

    if (mustChangePassword) {
      return
    }

    loadPayments()
  }, [route, authLoading, isLoggedIn, mustChangePassword])

  // B.30a Handler: updates the booking form state when the user types (controlled inputs lane).
  // - Keeps the <input> values in sync with our React memory so the form UI always reflects `form`.
  const onChange = e => {
    const { name, value } = e.target
    // C.14 Special logic: convert hour and minute fields to numbers.
    // - [name]: dynamic property key, so this line updates the specific field that changed.
    // - We use functional state update (currentForm => ...) to ensure we see the latest state value.
    setForm(currentForm => ({
      ...currentForm, // Spread operator: Copies all existing properties (like new Object { ...oldObject })
      [name]: name === 'hasGuest' ? e.target.checked : (name === 'hour' || name.includes('Minute') || name === 'hostCount' ? Number(value) : value)
    }))
  }

  // B.15 [Root] Frontend handler for the booking creation flow.
  // - Part of the "Radio Scheduler Booking Creation Flow" codemap.
  // - Consumes the form state from the form/control lane [B.30*] and sends it as a POST to /db/event/post [B.15a]
  //   with the same parameters used by EventActionsDb.CreateEvent on the backend [B.15b].
  const submitBooking = async (e) => {
    // C.15 Prevent default browser form submission (which would reload the page).
    e.preventDefault()

    setIsPosting(true); setError(''); setSuccessMessage('')
    try {
      // C.16 Build query string to match Minimal API parameter binding.
      // - The keys here (date, hour, startMinute, endMinute) map 1:1 to the endpoint signature in EventDbEndpoints.cs.
      const queryString = new URLSearchParams({
        date: form.date,
        hour: String(form.hour),
        startMinute: String(form.startMinute),
        // Fix: Add +1 to endMinute because backend expects exclusive end (0-20 for 0-19 booking)
        endMinute: String(form.endMinute + 1),
        title: form.title,
        eventType: form.eventType,
        hostCount: String(form.hostCount),
        hasGuest: String(form.hasGuest)
      }).toString()

      // C.17 POST request: send the booking to /db/event/post.
      const res = await fetch(`/db/event/post?${queryString}`, {
        method: 'POST',
        credentials: 'include'
      })

      if (res.status === 401) {
        window.location.hash = '#/portal'
        throw new Error('Du måste logga in för att boka.')
      }

      if (res.status === 403) {
        if (mustChangePassword) {
          window.location.hash = '#/portal/change-password'
          throw new Error('Du måste byta lösenord innan du kan boka.')
        }

        const text = await res.text()
        throw new Error(text || 'Du saknar behörighet för att boka.')
      }

      if (!res.ok) {
        const text = await res.text()
        throw new Error(text || `HTTP ${res.status}`)
      }

      const result = await res.json()
      setSuccessMessage(`Skapade bokning med id ${result.EventId}`)

      // B.16 Refresh both Today and Week to show the newly created booking.
      // - Promise.all is like Task.WhenAll in C#: wait for both loads to finish before considering the refresh done.
      await Promise.all([loadToday(), loadWeek()])
    } catch (e2) {
      setError(e2.message || String(e2))
    } finally {
      setIsPosting(false)
    }
  }

  const handlePortalLoginSubmit = async (e) => {
    e.preventDefault()
    setPortalError('')
    setPortalIsSubmitting(true)

    try {
      const me = await login(portalEmail, portalPassword)
      setPortalPassword('')

      if (!me) {
        window.location.hash = '#/portal'
        return
      }

      window.location.hash = me.mustChangePassword ? '#/portal/change-password' : '#/bookings'
    } catch (e2) {
      setPortalError(e2.message || String(e2))
    } finally {
      setPortalIsSubmitting(false)
    }
  }

  const handleChangePasswordSubmit = async (e) => {
    e.preventDefault()
    setChangePasswordError('')
    setChangePasswordSuccessMessage('')
    setIsChangingPassword(true)

    try {
      const res = await fetch('/api/auth/change-password', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          OldPassword: oldPassword,
          NewPassword: newPassword
        }),
        credentials: 'include'
      })

      if (res.status === 401) {
        window.location.hash = '#/portal'
        throw new Error('Du måste logga in.')
      }

      if (!res.ok) {
        const text = await res.text()
        throw new Error(text || `HTTP ${res.status}`)
      }

      setChangePasswordSuccessMessage('Lösenordet är uppdaterat.')
      setOldPassword('')
      setNewPassword('')

      const me = await refreshMe()
      if (!me) {
        window.location.hash = '#/portal'
        return
      }

      window.location.hash = me.mustChangePassword ? '#/portal/change-password' : '#/bookings'
    } catch (e2) {
      setChangePasswordError(e2.message || String(e2))
    } finally {
      setIsChangingPassword(false)
    }
  }

  const handleLogout = async () => {
    setPortalError('')

    try {
      await logout()

      // B.21 [Portal.Me] Cleanup on logout.
      // Why: Payment rows are user-specific; clearing avoids "flash" of the previous user's data if someone logs in next.
      // Where: The navigation link lives in StaffHeader (top of this file) and calls this handler.
      setPayments([])
      setPaymentsError('')
      setPaymentsLoading(false)
      window.location.hash = '#/portal'
    } catch (e2) {
      const message = e2.message || String(e2)
      setPortalError(message)
      setError(message)
    }
  }

  // B.5c Derived state: decide which "page" to show based on the current route (navigation lane).
  // - Mirrors the Router flow in main.jsx [B.5/B.5b] (codemap: Hash Navigation Setup and Route Management).
  const isHome = route === '#/'
  const isSchedule = route.startsWith('#/schedule')
  const isPortalLogin = route === '#/portal'
  const isPortalMe = route.startsWith('#/portal/me')
  const isPortalChangePasswordRoute = route.startsWith('#/portal/change-password')
  const isBookings = route.startsWith('#/bookings') || route.startsWith('#/admin')
  const isStaffRoute = route.startsWith('#/portal') || isBookings

  return (
    <div className="container">
      {isStaffRoute ? (
        <StaffHeader user={user} onLogout={handleLogout} />
      ) : (
        <SiteHeader />
      )}

      {/* Conditional Rendering: Only show if isHome is true */}
      {isHome && (
        <>
          <HeroNowPlaying />
          <FeaturedRow />
          {week && <SchedulePreview week={week} />}
        </>
      )}

      {isSchedule && (
        <>
          <header><h1>Hela tablån</h1></header>
          <section className="grid">
            <div className="card" style={{ gridColumn: '1 / -1' }}>
              <div className="schedule-note">Har vi inget som händer på studion så kör vi musik. Kom och lyssna dygnet runt!</div>
              <div className="schedule-scroll-container">
                <WeekBookingsOnly week={week} />
              </div>
            </div>
          </section>
        </>
      )}

      {isPortalLogin && (
        <section className="grid">
          <div className="card" style={{ gridColumn: '1 / -1' }}>
            <header><h1>Personalportal</h1></header>

            {portalError && <div className="alert error">Fel: {portalError}</div>}

            <form onSubmit={handlePortalLoginSubmit} className="form">
              <label>
                E-post
                <input
                  type="email"
                  value={portalEmail}
                  onChange={(e) => setPortalEmail(e.target.value)}
                  autoComplete="username"
                  required
                />
              </label>
              <label>
                Lösenord
                <input
                  type="password"
                  value={portalPassword}
                  onChange={(e) => setPortalPassword(e.target.value)}
                  autoComplete="current-password"
                  required
                />
              </label>
              <div className="form-actions">
                <button type="submit" disabled={portalIsSubmitting}>
                  {portalIsSubmitting ? 'Loggar in…' : 'Logga in'}
                </button>
              </div>
            </form>
          </div>
        </section>
      )}

      {isPortalMe && (
        <section className="grid">
          <div className="card" style={{ gridColumn: '1 / -1' }}>
            <header><h1>Konto</h1></header>

            {authLoading && <p>Laddar…</p>}

            {!authLoading && !isLoggedIn && (
              <div className="alert error">Du är inte inloggad.</div>
            )}

            {!authLoading && isLoggedIn && (
              <>
                <div className={`account-profile ${user?.photoUrl ? '' : 'no-photo'}`.trim()}>
                  {user?.photoUrl && (
                    <div className="account-photo">
                      <img src={user.photoUrl} alt="Profilbild" />
                    </div>
                  )}

                  <div className="account-details">
                    <p><strong>E-post:</strong> {user?.email ?? ''}</p>
                    <p><strong>Telefon:</strong> {user?.phone ? user.phone : '-'}</p>
                    <p><strong>Adress:</strong> {user?.address ? user.address : '-'}</p>
                    <p><strong>Bio:</strong> {user?.bio ? user.bio : '-'}</p>
                    <p><strong>Roll:</strong> {(roles ?? []).join(', ')}</p>
                    <p><strong>Måste byta lösenord:</strong> {mustChangePassword ? 'Ja' : 'Nej'}</p>
                  </div>
                </div>

                <div className="form-actions">
                  <button
                    type="button"
                    onClick={() => {
                      refreshMe().catch(() => {})
                      loadPayments().catch(() => {})
                    }}
                    disabled={paymentsLoading}
                  >
                    Uppdatera
                  </button>
                </div>

                <h2>Betalningshistorik</h2>

                {paymentsLoading && <p>Laddar betalningar…</p>}

                {paymentsError && <div className="alert error">Fel: {paymentsError}</div>}

                {!paymentsLoading && !paymentsError && payments.length === 0 && (
                  <p>Inga betalningar ännu.</p>
                )}

                {!paymentsLoading && payments.length > 0 && (
                  <div className="payments-table-wrapper">
                    <table className="payments-table">
                      <thead>
                        <tr>
                          <th>Period</th>
                          <th>Tid</th>
                          <th className="num">Event</th>
                          <th className="num">Bas</th>
                          <th className="num">Bonus</th>
                          <th className="num">Moms</th>
                          <th className="num">Totalt</th>
                        </tr>
                      </thead>
                      <tbody>
                        {payments
                          .slice()
                          .sort(
                            (a, b) =>
                              (Number(b.periodYear) - Number(a.periodYear)) ||
                              (Number(b.periodMonth) - Number(a.periodMonth))
                          )
                          .map((p) => (
                            <tr key={p.id ?? `${p.periodYear}-${p.periodMonth}`}>
                              <td>{formatYearMonth(p.periodYear, p.periodMonth)}</td>
                              <td>{formatDurationMinutes(p.totalMinutes)}</td>
                              <td className="num">{p.eventCount}</td>
                              <td className="num">{formatSek(p.baseAmount)}</td>
                              <td className="num">{formatSek(p.eventBonusAmount)}</td>
                              <td className="num">{formatSek(p.vatAmount)}</td>
                              <td className="num">{formatSek(p.totalIncludingVat)}</td>
                            </tr>
                          ))}
                      </tbody>
                    </table>
                  </div>
                )}
              </>
            )}
          </div>
        </section>
      )}

      {isPortalChangePasswordRoute && (
        <section className="grid">
          <div className="card" style={{ gridColumn: '1 / -1' }}>
            <header><h1>Byt lösenord</h1></header>

            {mustChangePassword && (
              <div className="alert error">Du måste byta lösenord innan du kan fortsätta.</div>
            )}

            {changePasswordError && <div className="alert error">Fel: {changePasswordError}</div>}
            {changePasswordSuccessMessage && <div className="alert ok">{changePasswordSuccessMessage}</div>}

            <form onSubmit={handleChangePasswordSubmit} className="form">
              <label>
                Nuvarande lösenord
                <input
                  type="password"
                  value={oldPassword}
                  onChange={(e) => setOldPassword(e.target.value)}
                  autoComplete="current-password"
                  required
                />
              </label>
              <label>
                Nytt lösenord
                <input
                  type="password"
                  value={newPassword}
                  onChange={(e) => setNewPassword(e.target.value)}
                  autoComplete="new-password"
                  required
                />
              </label>
              <div className="form-actions">
                <button type="submit" disabled={isChangingPassword}>
                  {isChangingPassword ? 'Sparar…' : 'Spara'}
                </button>
              </div>
            </form>
          </div>
        </section>
      )}

      {isBookings && authLoading && (
        <section className="grid">
          <div className="card" style={{ gridColumn: '1 / -1' }}>
            <p>Laddar…</p>
          </div>
        </section>
      )}

      {isBookings && !authLoading && !isLoggedIn && (
        <section className="grid">
          <div className="card" style={{ gridColumn: '1 / -1' }}>
            <p>Du måste logga in.</p>
          </div>
        </section>
      )}

      {isBookings && !authLoading && isLoggedIn && mustChangePassword && (
        <section className="grid">
          <div className="card" style={{ gridColumn: '1 / -1' }}>
            <p>Du måste byta lösenord.</p>
          </div>
        </section>
      )}

      {isBookings && !authLoading && isLoggedIn && !mustChangePassword && (
        <AdminPanel
          week={week}
          today={today}
          form={form}
          // Callback for clicking a cell: Update form state
          onPickHour={(date, hour) => setForm(f => ({
            ...f,
            date: (date || new Date()).toString().slice(0, 10),
            hour
          }))}
          onMinuteClick={handleMinuteClick}
          selection={selection}
          onChange={onChange}
          submitBooking={submitBooking}
          loading={loading}
          loadToday={loadToday}
          error={error}
          successMessage={successMessage}
        />
      )}
    </div>
  )
}

// === Experiments: Frontend behaviour (App.jsx) ===
// Experiment 1: API failures and error UI.
//   Step 1: Temporarily change one fetch URL (e.g. '/db/schedule/today') to an invalid path.
//   Step 2: Load the app and observe the error message and loading states.
//   Step 3: Restore the original URL and confirm the UI recovers.
// Experiment 2: Schedule density visuals.
//   Step 1: Create many overlapping or adjacent bookings through the admin form.
//   Step 2: Observe how HourCell colours and tooltips change as booked minutes increase.
//   Step 3: Adjust the level thresholds in HourCell if you want different visual cut-offs.
// Experiment 3: Routes and layout.
//   Step 1: Add a new route flag derived from `route` (e.g. `isLab`).
//   Step 2: Render a small diagnostic section when that flag is true.
//   Step 3: Navigate via hash and confirm that Router + App work together to show the new view.
