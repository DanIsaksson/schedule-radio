using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleStorage;

// Root object that keeps a rolling seven-day schedule starting from today.
public class ScheduleData
{
    // Declare and initialize Days-list and make it a list of 7 elements starting at 0.
    public List<DaySchedule> Days { get; } = Enumerable.Range(0, 7)
        .Select(offset => new DaySchedule(DateTime.Today.AddDays(offset)))
        .ToList();
}

// Represents a single calendar day and its 24 one-hour slots.
public class DaySchedule
{
    // Initiate DateTime as Date for retrieval only
    public DateTime Date { get; }

    // Instance HourSchedule list as Hours and fill with 24 blocks covering 00-23 from Enumerable.Range
    public List<HourSchedule> Hours { get; } = Enumerable.Range(0, 24)
        .Select(hour => new HourSchedule(hour)) // every item is assigned as instance of HourSchedule.
        .ToList(); // send to List<HourSchedule> Hours


    // Unclear
    public DaySchedule(DateTime date)
    {
        Date = date;
    }
}

// Represents the 60 minutes inside one hour.
public class HourSchedule
{
    public int Hour { get; }

    // true = booked / false = free
    public bool[] Minutes { get; } = new bool[60];

    public HourSchedule(int hour)
    {
        Hour = hour;
    }
}