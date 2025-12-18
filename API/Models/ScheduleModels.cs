// A.1 [Schedule.Model] Shared schedule read model (Days → Hours → Minutes[60]).
// What: DTOs returned by both legacy (/schedule/*) and DB-backed (/db/schedule/*) read endpoints.
// Why: Keeps one consistent schedule shape where "no booking" means default music (filler) for the hour/minute.
// Where:
// - Painted from DB rows by API/Actions/ScheduleProjectionDb.cs.
// - Consumed by the React UI in frontend/src/App.jsx (loadToday/loadWeek).
//
// --- FILE: Models/ScheduleModels.cs ---
// A.3a Minute-grid structure: 7-day schedule grid model: Days → Hours → Minutes[60] (true = booked, false = free).
// - This is the concrete data shape behind Program.cs A.3 (7 days of minute-level radio schedule).
// - Used by ScheduleProjectionDb when building /db/schedule/today and /db/schedule/7days responses [A.3b].
// - Kept compatible with the legacy in-memory structure so both admin and consumer UIs can reuse it.
namespace API.Models // A new namespace to avoid conflicts
{
    /// <summary>
    /// Represents the entire 7-day rolling schedule for the radio station.
    /// </summary>
    public class ScheduleData
    {
        // Pre-create 7 days starting from today so the frontend always has a full window.
        // ScheduleProjectionDb then paints bookings into these DaySchedule objects.
        public List<DaySchedule> Days { get; } = new List<DaySchedule>();

        // Constructor: set up the rolling 7-day list.
        public ScheduleData()
        {
            // Create 7 days starting from today
            for (int i = 0; i < 7; i++)
            {
                Days.Add(new DaySchedule(DateTime.Today.AddDays(i)));
            }
        }
    }

    /// <summary>
    /// Represents a single day in the schedule, containing 24 hours.
    /// </summary>
    public class DaySchedule
    {
        // Calendar date for this DaySchedule
        public DateTime Date { get; }

        // 24 HourSchedule entries (0..23)
        public List<HourSchedule> Hours { get; } = new List<HourSchedule>();

        // Constructor: set up this day's 24 HourSchedule entries.
        public DaySchedule(DateTime date)
        {
            Date = date.Date; // Keep only yyyy-MM-dd (drop time-of-day)

            // Create 24 hours (0 to 23)
            for (int i = 0; i < 24; i++)
            {
                Hours.Add(new HourSchedule(i));
            }
        }
    }

    /// <summary>
    /// Represents a single hour in the schedule, containing 60 minutes.
    /// </summary>
    public class HourSchedule
    {
        // Hour of day (0–23)
        public int Hour { get; }

        // A.3a Minutes[60]: true = booked, false = free. We treat bookings as half-open ranges [start, end).
        // - Matches the meaning described in Program.cs A.3 (true = show/host booked, false = default music).
        public bool[] Minutes { get; } = new bool[60];

        // Constructor
        public HourSchedule(int hour)
        {
            Hour = hour;
            // Start free; projection code flips booked minutes to true (Actions/ScheduleProjectionDb.cs).
            for (int i = 0; i < 60; i++)
            {
                Minutes[i] = false;
            }
        }

        // List of bookings in this hour (for Visitor UI to show titles)
        public List<BookingDetail> Bookings { get; set; } = new List<BookingDetail>();
    }

    /// <summary>
    /// Public view of a booking (safe for visitors).
    /// </summary>
    public class BookingDetail
    {
        public string Title { get; set; } = "Untitled";
        public int StartMinute { get; set; }
        public int EndMinute { get; set; }
    }
}