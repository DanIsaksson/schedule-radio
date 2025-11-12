// --- FILE: Models/EventEntity.cs
// What this is (beginner view)
// - One DB row = one booking (date + hour + minute range).
// - EF maps this class to the 'Events' table because DbContext exposes DbSet<EventEntity> Events.
// How I use it
// - Actions/EventActionsDb.cs creates/updates/deletes these rows via EF Core.
// - ScheduleProjectionDb.cs reads them and paints minutes onto a 7-day grid.
namespace API.Models
{
    public class EventEntity
    {
        // Primary key (auto-increment). EF sees 'Id' and treats it as the key.
        public int Id { get; set; }

        // Booking date (keep only yyyy-MM-dd). Querying by day becomes straightforward.
        public DateTime Date { get; set; }

        // Hour of day (0–23). Separate field helps fast overlap checks for the same hour.
        public int Hour { get; set; }

        // Start minute (inclusive, 0–59). We use half-open ranges [start, end).
        public int StartMinute { get; set; }

        // End minute (exclusive, 1–60). Must be > StartMinute; 60 means "up to the end of the hour".
        public int EndMinute { get; set; }

        // Optional later: add computed DurationMinutes => EndMinute - StartMinute.
    }
}
