using System;
using System.Collections.Generic;
using System.Text;

namespace TimeItUpServices.Library
{
    public class TimeAndDateHelper : ITimeAndDateHelper
    {
        private string GenerateMagicStringOfElapsedTime(TimeSpan timePeriod)
        {
            StringBuilder result = new StringBuilder();
            result.Append(timePeriod.TotalDays).Append("d");
            result.Append(timePeriod.TotalHours).Append("h");
            result.Append(timePeriod.TotalMinutes).Append("m");
            result.Append(timePeriod.TotalSeconds).Append("s");
            result.Append(timePeriod.TotalMilliseconds).Append("ms");

            return result.ToString();
        }

        public string CalculateDateTimePeriodAsString(DateTime startDate, DateTime endDate)
        {
            var timePeriod = endDate.Subtract(startDate);

            return GenerateMagicStringOfElapsedTime(timePeriod);
        }

        public string CalculateDateTimePeriodAsString(TimeSpan timePeriod)
        {
            return GenerateMagicStringOfElapsedTime(timePeriod);
        }

        public TimeSpan CalculateDateTimePeriodAsTimeSpan(DateTime startDate, DateTime endDate)
        {
            return endDate.TimeOfDay - startDate.TimeOfDay;
        }

        public TimeSpan AddTimeSpans(ICollection<TimeSpan> timespans)
        {
            TimeSpan result = new TimeSpan();

            foreach (var timespan in timespans)
            {
                result = result.Add(timespan);
            }

            return result;
        }
    }
}
