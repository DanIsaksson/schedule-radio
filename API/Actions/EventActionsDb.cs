// --- FILE: Actions/EventActionsDb.cs
//
// PURPOSE
// - Database-backed equivalents of the current in-memory event actions.
// - These methods are intentionally left as guided skeletons for you to implement.
//
// HOW TO USE
// - Inject SchedulerContext (db) into endpoints and call these methods.
// - Use simple, synchronous EF calls (ToList, Find, Add, Remove, SaveChanges).
//
// SYNTAX PRIMER (read once, then implement TODOs below)
// - DbContext & DbSet<T> basics:
//     db.Events.Add(entity);   // stage an INSERT of one row
//     db.Events.Remove(entity);// stage a DELETE of one row
//     db.SaveChanges();        // COMMIT all staged changes to the database
//
// - LINQ filter pattern (requires 'using System.Linq;'):
//     var rows = db.Events.Where(e => e.Hour == 10).ToList();
//     // e => e.Hour == 10  means: for each row e, keep it if this expression is true.
//     // ToList() executes the query and returns a List<EventEntity>.
//
// - Time range overlap rule using half-open intervals [start, end):
//     Two ranges A=[a1,a2) and B=[b1,b2) DO NOT overlap if a2 <= b1 OR b2 <= a1.
//     Therefore, they DO overlap if NOT (a2 <= b1 || b2 <= a1).
//     We'll re-use that exact boolean expression below.
//
// - Minimal APIs + DI:
//     If an endpoint lambda has a parameter 'SchedulerContext db', ASP.NET injects it for you.
//     You do NOT 'new' the context yourself; the framework manages its lifetime per request.
//
// STEP-BY-STEP HINTS ARE INCLUDED INSIDE EACH METHOD.

using System;
using System.Collections.Generic;
using System.Linq; // For .Where and .ToList
using API.Data;
using API.Models;

namespace API.Actions
{
    public static class EventActionsDb
    {
        // Create a new booking row. Return new Id on success; -1 if conflict/invalid.
        public static int CreateEvent(SchedulerContext db, DateTime date, int hour, int startMinute, int endMinute)
        {
            // 1) Validate input (keep it simple):
            //    - hour must be 0..23; minutes 0..59; end > start.
            if (hour < 0 || hour > 23) return -1;
            if (startMinute < 0 || startMinute > 59) return -1;
            if (endMinute <= startMinute || endMinute > 60) return -1;
            
            // 2) Query existing bookings for the same date and hour:
            var day = date.Date;
            var sameHour = db.Events
                .Where(e => e.Date == day && e.Hour == hour)
                .ToList();

            // 3) Check overlap with any existing booking in sameHour:
            foreach (var e in sameHour)
            {
                // Overlap if NOT (end <= otherStart || otherEnd <= start)
                bool overlaps = !(endMinute <= e.StartMinute || e.EndMinute <= startMinute);
                if (overlaps) return -1;
            }

            // 4) Create a new EventEntity, add it, and SaveChanges().
            var entity = new EventEntity
            {
                Date = day,
                Hour = hour,
                StartMinute = startMinute,
                EndMinute = endMinute
            };
            db.Events.Add(entity);
            db.SaveChanges();

            // 5) Return the generated Id.
            return entity.Id;
        }

        // Delete a booking by Id. Return true on success; false if not found.
        public static bool DeleteEvent(SchedulerContext db, int eventId)
        {
            // 1) Find the entity: var entity = db.Events.Find(eventId);
            // 2) If null => return false.
            // 3) Remove(entity); SaveChanges(); return true.
            var entity = db.Events.Find(eventId);
            if (entity is null) return false;
            db.Events.Remove(entity);
            db.SaveChanges();
            return true;
        }

        // Change the time of an existing booking. Return true on success.
        public static bool RescheduleEvent(
            SchedulerContext db,
            int eventId,
            DateTime newDate,
            int newHour,
            int newStartMinute,
            int newEndMinute)
        {
            // 1) Find existing entity (db.Events.Find(eventId)); if null => return false.
            // 2) Check conflicts at (newDate, newHour) with the same overlap rule, BUT ignore this event's own Id.
            // 3) If conflict => return false.
            // 4) Update the entity's fields and SaveChanges(); return true.
            var entity = db.Events.Find(eventId);
            if (entity is null) return false;

            // Basic validation for the new time
            if (newHour < 0 || newHour > 23) return false;
            if (newStartMinute < 0 || newStartMinute > 59) return false;
            if (newEndMinute <= newStartMinute || newEndMinute > 60) return false;

            var day = newDate.Date;
            var sameHour = db.Events
                .Where(e => e.Date == day && e.Hour == newHour && e.Id != eventId)
                .ToList();

            foreach (var e in sameHour)
            {
                bool overlaps = !(newEndMinute <= e.StartMinute || e.EndMinute <= newStartMinute);
                if (overlaps) return false;
            }

            entity.Date = day;
            entity.Hour = newHour;
            entity.StartMinute = newStartMinute;
            entity.EndMinute = newEndMinute;
            db.SaveChanges();
            return true;
        }

        // Read all bookings. Simple helper to test reads early.
        public static List<EventEntity> ListEvents(SchedulerContext db)
        {
            // TIP: Start simple. You can sort later if you want to.
            return db.Events.ToList();
        }
    }
}
