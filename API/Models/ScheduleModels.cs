// --- FILE: Models/ScheduleModels.cs ---
// What this file is (beginner view)
// - Read model used by endpoints and frontend. It’s a 7-day grid: Days → Hours → Minutes[60].
// - I keep it simple and compatible with old in-memory code so both admin and consumer UIs can reuse it.
namespace API.Models // A new namespace to avoid conflicts
{
    /// <summary>
    /// Represents the entire 7-day rolling schedule for the radio station.
    /// </summary>
    public class ScheduleData
    {
        // I pre-create 7 days starting from today so the frontend always has a full window.
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

        // Constructor
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

        // Minutes[60]: true = booked, false = free. We treat bookings as half-open ranges [start, end).
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
    }
}