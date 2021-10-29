﻿using System;
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
            result.Append(timePeriod.Hours).Append(":");
            result.Append(timePeriod.Minutes).Append(":");
            result.Append(timePeriod.Seconds).Append(":");
            result.Append(timePeriod.Milliseconds);

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
