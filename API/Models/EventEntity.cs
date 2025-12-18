// [Db.Schema.4] Event Entity
// This class represents a single row in the "Events" table.
// It describes the shape of one booking.
//
// -> See Lesson: Interactive-Lesson/02-EFCore-DbContext-Entities.md

namespace API.Models
{
    public class EventEntity
    {
        // [Db.Schema.5] Primary Key
        // EF Core automatically treats a property named "Id" as the Primary Key.
        public int Id { get; set; }

        // [Db.Schema.6] Data Properties
        // These properties map directly to columns in the database table.
        
        // The date of the booking (yyyy-MM-dd)
        public DateTime Date { get; set; }

        // The hour (0-23)
        public int Hour { get; set; }

        // Start minute (0-59, inclusive)
        public int StartMinute { get; set; }

        // End minute (1-60, exclusive)
        // "Exclusive" means if EndMinute is 30, the booking goes up to minute 29.
        public int EndMinute { get; set; }

        // [Db.Schema.7] New Fields for Visitor/Admin Requirements
        public string? Title { get; set; }
        public string? EventType { get; set; } // "PreRecorded" or "Live"
        public int? HostCount { get; set; }
        public bool HasGuest { get; set; }

        // B.8 [Booking.Ownership] The Identity user id that owns this booking (AspNetUsers.Id).
        // Why: Used for edit/delete permissions and for monthly payment calculation (Events.ResponsibleUserId -> ContributorPayments).
        // Where:
        // - Set on create by POST /db/event/post (Endpoints/EventDbEndpoints.cs).
        // - Queried during payroll by POST /api/admin/payments/calculate-previous-month (Endpoints/AdminPaymentsEndpoints.cs).
        // Invariant: New bookings should always have an owner; legacy rows may be backfilled in Program.cs at startup.
        public string? ResponsibleUserId { get; set; }
    }
}
