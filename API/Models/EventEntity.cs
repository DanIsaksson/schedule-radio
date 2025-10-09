// --- FILE: Models/EventEntity.cs
//
// PURPOSE
// - Represents a single booking ("event") we want to persist in the database.
// - This is intentionally simple to match our current in-memory concept.
//
// EF CORE CONVENTIONS (no attributes needed initially)
// - Table name: by default EF maps this class to a table named 'EventEntity' or 'EventEntities'.
//   Because we expose it via DbSet<EventEntity> named 'Events', EF will use table name 'Events'.
// - Primary key: by convention, a property named 'Id' is the primary key.
// - Column names & types: map from property names and CLR types (DateTime, int).
// - You can later add Data Annotations [Required], [MaxLength], etc., but we start simple.
//
// KEY IDEA
// - Instead of storing a full 7-day nested structure, we just store bookings (rows).
// - You can reconstruct a day's schedule by filtering rows by Date and Hour.
//
// WHAT TO CONSIDER WHILE FILLING DETAILS
// - Validation: Hour should be 0-23; minutes 0-59; StartMinute < EndMinute.
// - Overlap logic: same date/hour rows should not overlap the [start,end) minute ranges.
// - Extensions later: add HostName, GuestName, or ShowTitle columns here.

namespace API.Models
{
    public class EventEntity
    {
        // Primary key (auto-incremented by the database)
        // - EF sees 'Id' and treats it as the key. SQLite will auto-increment it when inserting.
        public int Id { get; set; }

        // Date of the booking (only the date part matters for grouping)
        // - Using 'date.Date' when writing ensures no time-of-day creeps in.
        // - This makes it easy to query all bookings for a single calendar day.
        public DateTime Date { get; set; }

        // Hour of the day (0-23) when this booking occurs
        // - Keeping hour separate helps us prevent overlap within the same hour cheaply.
        public int Hour { get; set; }

        // Inclusive start minute (0-59)
        // - We treat ranges as half-open intervals: [StartMinute, EndMinute)
        //   so a booking 0..30 ends exactly at minute 30 and does not include 30.
        public int StartMinute { get; set; }

        // Exclusive end minute (1-60)
        // - Must be > StartMinute. Max is 60 (so 0..60 means the whole hour).
        public int EndMinute { get; set; }

        // TIP: If you want convenience, you can later add a computed property for Duration.
        // public int DurationMinutes => EndMinute - StartMinute;
    }
}
