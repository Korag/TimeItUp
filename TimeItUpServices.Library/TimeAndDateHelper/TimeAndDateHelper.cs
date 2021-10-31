using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimeItUpServices.Library
{
    public class TimeAndDateHelper : ITimeAndDateHelper
    {
        private string GenerateMagicStringOfElapsedTime(TimeSpan timePeriod)
        {
            StringBuilder result = new StringBuilder();

            if (timePeriod.Hours < 10)
            {
                result.Append(0).Append(timePeriod.Hours).Append(":");
            }
            else
            {
                result.Append(timePeriod.Hours).Append(":");
            }

            if (timePeriod.Minutes < 10)
            {
                result.Append(0).Append(timePeriod.Minutes).Append(":");
            }
            else
            {
                result.Append(timePeriod.Minutes).Append(":");
            }

            if (timePeriod.Seconds < 10)
            {
                result.Append(0).Append(timePeriod.Seconds).Append(":");
            }
            else
            {
                result.Append(timePeriod.Seconds).Append(":");
            }

            if (timePeriod.Milliseconds < 10)
            {
                result.Append(00).Append(timePeriod.Milliseconds);
            }
            else if (timePeriod.Milliseconds >= 10 && timePeriod.Milliseconds < 100)
            {
                result.Append(0).Append(timePeriod.Milliseconds);
            }
            else
            {
                result.Append(timePeriod.Milliseconds);
            }

            return result.ToString();
        }

        public string CalculateDateTimePeriodAsString(DateTime startDate, DateTime endDate)
        {
            if (endDate == DateTime.MinValue)
            {
                endDate = DateTime.UtcNow;
            }

            var timePeriod = endDate.Subtract(startDate);

            return GenerateMagicStringOfElapsedTime(timePeriod);
        }

        public string CalculateDateTimePeriodAsString(TimeSpan timePeriod)
        {
            return GenerateMagicStringOfElapsedTime(timePeriod);
        }

        public TimeSpan CalculateDateTimePeriodAsTimeSpan(DateTime startDate, DateTime endDate)
        {
            if (endDate == DateTime.MinValue)
            {
                endDate = DateTime.UtcNow;
            }
            var result = endDate - startDate;

            return new TimeSpan(result.Days, result.Hours, result.Minutes, result.Seconds, result.Milliseconds);
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
