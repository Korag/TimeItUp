using System;
using System.Collections.Generic;

namespace TimeItUpServices.Library
{
    public interface ITimeAndDateHelper
    {
        string CalculateDateTimePeriodAsString(DateTime startDate, DateTime endDate);
        string CalculateDateTimePeriodAsString(TimeSpan timePeriod);
        TimeSpan CalculateDateTimePeriodAsTimeSpan(DateTime startDate, DateTime endDate);
        TimeSpan AddTimeSpans(ICollection<TimeSpan> timespans);
        TimeSpan AddTimeFromMagicStrings(ICollection<string> timeStrings);
    }
}