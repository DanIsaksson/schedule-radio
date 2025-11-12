// App: I render the public consumer site (read-only) and an admin panel (booking) based on route.
import React, { useEffect, useState } from 'react'

function HourRow({ hour, minutes }) {
  const booked = minutes.some(m => m)
  const hh = String(hour).padStart(2, '0')
  return (
    <li className="row">
      <span className="time">{hh}:00</span>
      <span className={`status ${booked ? 'booked' : 'free'}`}>{booked ? 'Booked' : 'Free'}</span>
      <span className="count">{minutes.filter(Boolean).length}/60</span>
    </li>
  )
}

// HourCell: one hour cell; tooltip shows compact booked ranges like "00–15, 30–45"; click can prefill admin form
function HourCell({ date, hour, minutes, onPick }) {
  const bookedCount = minutes.filter(Boolean).length
  const frac = bookedCount / 60
  const level = frac === 0 ? 'free' : frac < 0.5 ? 'partial' : 'busy'
  // Build tooltip with booked ranges in minutes, e.g. "00–15, 30–45"
  let ranges = []
  let i = 0
  while (i < 60) {
    if (minutes[i]) {
      const start = i
      while (i < 60 && minutes[i]) i++
      const end = i
      const fmt = (n) => String(n).padStart(2, '0')
      ranges.push(`${fmt(start)}–${fmt(end)}`)
    } else {
      i++
    }
  }
  const hh = String(hour).padStart(2, '0')
  const tip = ranges.length
    ? `${hh}:00  ${bookedCount}/60 booked · ${ranges.join(', ')}`
    : `${hh}:00  ${bookedCount}/60 booked`
  return (
    <div className={`cell ${level}`} title={tip} onClick={() => onPick(date, hour)} />
  )
}

// DayColumn: mark today for emphasis; render 24 HourCells
function DayColumn({ day, onPick }) {
  const d = new Date(day.date ?? day.Date)
  const isToday = d.toDateString() === new Date().toDateString()
  const dateStr = d.toLocaleDateString(undefined, { weekday: 'short', month: 'short', day: 'numeric' })
  const hours = (day.hours ?? day.Hours ?? []).map(h => ({ hour: h.hour ?? h.Hour, minutes: h.minutes ?? h.Minutes }))
  return (
    <div className={`day ${isToday ? 'today' : ''}`}>
      <div className="day-header">{dateStr}</div>
      <div className="day-grid">
        {hours.map(h => (
          <HourCell key={h.hour} date={day.date ?? day.Date} hour={h.hour} minutes={h.minutes} onPick={onPick} />
        ))}
      </div>
    </div>
  )
}

// WeekGrid: map 7 days → DayColumn. For consumer, onPick is a no-op; for admin, it pre-fills the form.
function WeekGrid({ week, onPick }) {
  const days = week?.days ?? week?.Days ?? []
  return (
    <div className="week-grid">
      {days.map(d => (
        <DayColumn key={d.date ?? d.Date} day={d} onPick={onPick} />
      ))}
    </div>
  )
}

// Consumer header & nav (public pages only)
function SiteHeader() {
  return (
    <header className="site-header">
      <div className="brand">Radio<span className="accent">Play</span></div>
      <nav className="nav">
        <a href="#/">Home</a>
        <a href="#/schedule">Schedule</a>
        <a href="#/shows" aria-disabled>Shows</a>
        <a href="#/podcasts" aria-disabled>Podcasts</a>
      </nav>
    </header>
  )
}

function SiteFooter() {
  return (
    <footer className="site-footer">
      <div>© {new Date().getFullYear()} RadioPlay</div>
      <div className="socials">•</div>
    </footer>
  )
}

function HeroNowPlaying() {
  return (
    <section className="hero">
      <div className="hero-art" />
      <div className="hero-meta">
        <div className="badge-live">LIVE</div>
        <h1>Morning Energy with Alex</h1>
        <p className="muted">Weekdays 06:00–10:00 · Host: Alex</p>
        <a className="btn-accent" href="#/">▶ Play</a>
      </div>
    </section>
  )
}

function FeaturedRow() {
  const items = [
    { id: 1, title: 'Drive Time', time: '16:00', img: '' },
    { id: 2, title: 'Late Night Chill', time: '22:00', img: '' },
    { id: 3, title: 'Top 40', time: '12:00', img: '' },
  ]
  return (
    <section className="featured container-wide">
      {items.map(i => (
        <div className="card card-shadow" key={i.id}>
          <div className="thumb" />
          <div className="card-meta">
            <h3>{i.title}</h3>
            <p className="muted">Today {i.time}</p>
          </div>
        </div>
      ))}
    </section>
  )
}

// SchedulePreview (consumer home): compact 7-day grid; link to full schedule
function SchedulePreview({ week }) {
  const todayIdx = 0
  const days = week?.days ?? week?.Days ?? []
  const daysShort = days.slice(0, 7)
  return (
    <section className="schedule-preview container-wide">
      <div className="preview-header">
        <h2>On Air This Week</h2>
        <a href="#/schedule" className="link">Full schedule →</a>
      </div>
      <div className="week-grid preview">
        {daysShort.map((d, idx) => (
          <div className={`day ${idx===todayIdx?'today':''}`} key={d.date ?? d.Date}>
            <div className="day-header">
              {new Date(d.date ?? d.Date).toLocaleDateString(undefined,{weekday:'short'})}
            </div>
            <div className="day-grid preview">
              {(d.hours ?? d.Hours ?? []).map(h => {
                const minutes = h.minutes ?? h.Minutes
                const booked = minutes.some(Boolean)
                return <div key={h.hour ?? h.Hour} className={`cell ${booked?'busy':'free'}`} />
              })}
            </div>
          </div>
        ))}
      </div>
    </section>
  )
}

// AdminPanel: internal page (#/admin). Click a cell to prefill date+hour; submit posts a booking then refreshes.
function AdminPanel({ week, today, form, onPickHour, onChange, submitBooking, loading, loadToday, error, msg }) {
  return (
    <>
      <section className="controls">
        <button onClick={loadToday} disabled={loading}>
          {loading ? 'Loading…' : 'Refresh Today'}
        </button>
      </section>

      {error && <div className="alert error">Error: {error}</div>}
      {msg && <div className="alert ok">{msg}</div>}

      <section className="grid">
        {week && (
          <div className="card" style={{ gridColumn: '1 / -1' }}>
            <h2>Week (Today + 6 days)</h2>
            <WeekGrid week={week} onPick={onPickHour} />
          </div>
        )}
        <div className="card">
          <h2>Today</h2>
          {!today ? (
            <p>Loading…</p>
          ) : (
            <>
              <p className="date">{new Date(today.date ?? Date.now()).toLocaleDateString()}</p>
              <ul className="schedule">
                {today.hours.map(h => (
                  <HourRow key={h.hour} hour={h.hour} minutes={h.minutes} />
                ))}
              </ul>
            </>
          )}
        </div>

        <div className="card">
          <h2>New Booking</h2>
          <form onSubmit={submitBooking} className="form">
            <label>
              Date
              <input type="date" name="date" value={form.date} onChange={onChange} />
            </label>
            <label>
              Hour
              <input type="number" name="hour" min="0" max="23" value={form.hour} onChange={onChange} />
            </label>
            <label>
              Start minute
              <input type="number" name="startMinute" min="0" max="59" value={form.startMinute} onChange={onChange} />
            </label>
            <label>
              End minute
              <input type="number" name="endMinute" min="1" max="60" value={form.endMinute} onChange={onChange} />
            </label>
            <div className="form-actions">
              <button type="submit">Book</button>
            </div>
          </form>
          <p className="hint">Tip: End minute must be greater than start minute, max 60.</p>
        </div>
      </section>
    </>
  )
}

export default function App({ route = '#/' }) {
  const [today, setToday] = useState(null)
  const [week, setWeek] = useState(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')
  const [form, setForm] = useState({
    date: new Date().toISOString().slice(0,10),
    hour: 12,
    startMinute: 0,
    endMinute: 30
  })
  const [posting, setPosting] = useState(false)
  const [msg, setMsg] = useState('')

  // Load today's schedule (DB-backed). Normalizes minute arrays for the Today card.
  const loadToday = async () => {
    setLoading(true); setError(''); setMsg('')
    try {
      const res = await fetch('/db/schedule/today')
      if (!res.ok) throw new Error(`HTTP ${res.status}`)
      const data = await res.json()
      const Hours = data.hours ?? data.Hours ?? []
      const normalized = Hours.map(h => ({
        hour: h.hour ?? h.Hour,
        minutes: (h.minutes ?? h.Minutes) ?? Array(60).fill(false)
      }))
      setToday({ date: data.date ?? data.Date, hours: normalized })
    } catch (e) {
      setError(e.message || String(e))
    } finally {
      setLoading(false)
    }
  }

  // Load the 7-day schedule projection (DB-backed) used by both consumer and admin views.
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

  // On mount, fetch both today and week so both routes are fast to show
  useEffect(() => { loadToday(); loadWeek() }, [])

  // Keep booking form in state; cast numeric inputs to numbers
  const onChange = e => {
    const { name, value } = e.target
    setForm(f => ({ ...f, [name]: name === 'hour' || name.includes('Minute') ? Number(value) : value }))
  }

  // Post a booking via query params (beginner-friendly binding). Then refresh Today + Week.
  const submitBooking = async (e) => {
    e.preventDefault()
    setPosting(true); setError(''); setMsg('')
    try {
      // Use query parameters to bind primitives in Minimal APIs without a DTO
      const qs = new URLSearchParams({
        date: form.date,
        hour: String(form.hour),
        startMinute: String(form.startMinute),
        endMinute: String(form.endMinute)
      }).toString()
      const res = await fetch(`/db/event/post?${qs}`, { method: 'POST' })
      if (!res.ok) {
        const text = await res.text()
        throw new Error(text || `HTTP ${res.status}`)
      }
      const result = await res.json()
      setMsg(`Created booking with id ${result.EventId}`)
      await Promise.all([loadToday(), loadWeek()])
    } catch (e2) {
      setError(e2.message || String(e2))
    } finally {
      setPosting(false)
    }
  }

  // Route flags (set by main.jsx Router)
  const isHome = route === '#/'
  const isSchedule = route.startsWith('#/schedule')
  const isAdmin = route.startsWith('#/admin')

  return (
    <div className="container">
      <SiteHeader />
      {isHome && (
        <>
          <HeroNowPlaying />
          <FeaturedRow />
          {week && <SchedulePreview week={week} />}
        </>
      )}
      {isSchedule && (
        <header><h1>Full Schedule</h1></header>
      )}

      {isSchedule && (
        <section className="grid">
          <div className="card" style={{ gridColumn: '1 / -1' }}>
            <WeekGrid week={week} onPick={() => {}} />
          </div>
        </section>
      )}

      {isAdmin && (
        <AdminPanel
          week={week}
          today={today}
          form={form}
          onPickHour={(date, hour) => setForm(f => ({ ...f, date: (date || new Date()).toString().slice(0,10), hour }))}
          onChange={onChange}
          submitBooking={submitBooking}
          loading={loading}
          loadToday={loadToday}
          error={error}
          msg={msg}
        />
      )}
    </div>
  )
}
