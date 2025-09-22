// --- FILE: Models/ScheduleModels.cs ---
//
// This file contains the data models for our radio station schedule.
// We are defining them in a single file for simplicity and to avoid
// any potential conflicts with the existing Models/ScheduleData.cs file.
//
// CONCEPT: CLASSES & PROPERTIES
// A 'class' is a blueprint for creating objects. It defines the data (properties)
// and behavior (methods) that objects of that type will have.
//
// A 'property' is a member of a class that provides a way to read, write, or compute
// the value of a private field. Here, we use auto-implemented properties (e.g.,
// `public List<DaySchedule> Days { get; } = ...;`) which automatically create
// a private, anonymous backing field that can only be accessed through the property's
// get and set accessors.
//

namespace Scheduler.Models // A new namespace to avoid conflicts
{
    /// <summary>
    /// Represents the entire 7-day rolling schedule for the radio station.
    /// </summary>
    public class ScheduleData
    {
        // This property is initialized when the object is created.
        // It creates 7 DaySchedule objects, one for each day starting from today.
        public List<DaySchedule> Days { get; } = new List<DaySchedule>();

        // Constructor: A special method that is called when an object is created.
        // It's used to set up the object with initial data.
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
        // The date for this day
        public DateTime Date { get; }

        // A list of 24 HourSchedule objects, one for each hour of the day
        public List<HourSchedule> Hours { get; } = new List<HourSchedule>();

        // Constructor
        public DaySchedule(DateTime date)
        {
            Date = date.Date; // Ensure we only store the date part, not time

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
        // The hour of the day (0-23)
        public int Hour { get; }

        // An array of 60 booleans to represent each minute.
        // true = booked, false = free
        public bool[] Minutes { get; } = new bool[60];

        // Constructor
        public HourSchedule(int hour)
        {
            Hour = hour;
            // All minutes are initially free (false)
            for (int i = 0; i < 60; i++)
            {
                Minutes[i] = false;
            }
        }
    }
}