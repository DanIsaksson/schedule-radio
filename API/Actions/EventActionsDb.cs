// --- FILE: Actions/EventActionsDb.cs
// What this file does (beginner view)
// - I save and change bookings in the SQLite database using EF Core.
// - Each public method is one action I can call from an endpoint (create, delete, reschedule, list).
// How to read this file
// - Skim the method name first, then read the numbered steps inside it (1..5). Each step says exactly why it exists.
// - If you see LINQ like .Where(...).ToList(), read it as: "filter rows like this, then run the query now".

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
                // Do these two time ranges overlap? (half-open [start, end))
                // - startMinute: my new booking start (0..59)
                // - endMinute: my new booking end   (1..60, must be > start)
                // - e.StartMinute: existing row start
                // - e.EndMinute:   existing row end
                // They overlap if NOT (myEnd <= otherStart || otherEnd <= myStart)
                bool overlaps = !(endMinute <= e.StartMinute || e.EndMinute <= startMinute);
                if (overlaps) return -1;
            }

            // 4) Create a new EventEntity (a single DB row for one booking). See Models/EventEntity.cs for fields.
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

    // === Experiments: Database-backed booking actions (EventActionsDb) ===
    // Experiment 1: Overlap rule and conflicts.
    //   Step 1: In CreateEvent, temporarily relax or remove the overlap check in step 3.
    //   Step 2: Use /db/event/post to create two overlapping bookings for the same date and hour.
    //   Step 3: Call /db/schedule/7days and observe how overlapping bookings appear, then restore the original overlap rule.
    // Experiment 2: Input validation and error responses.
    //   Step 1: Temporarily tighten or loosen the validation in step 1 (e.g., disallow late hours).
    //   Step 2: Try to create bookings that violate the new rules and see how /db/event/post responds.
    //   Step 3: Decide which validation shape you prefer and revert or keep the change.
    // Experiment 3: Sorting and listing.
    //   Step 1: In ListEvents, change the query to order events by Date then Hour.
    //   Step 2: Call GET /db/event and inspect the order in the response.
    //   Step 3: Compare unsorted vs sorted lists and choose the version that best serves your clients.
}
