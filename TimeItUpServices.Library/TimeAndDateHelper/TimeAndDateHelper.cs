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
            int timerHours = (timePeriod.Days * 24) + timePeriod.Hours;

            if (timerHours < 10)
            {
                result.Append(0).Append(timerHours).Append(":");
            }
            else
            {
                result.Append(timerHours).Append(":");
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

            if (startDate == endDate)
            {
                timePeriod = TimeSpan.Zero;
            }

            return GenerateMagicStringOfElapsedTime(timePeriod);
        }

        public string CalculateDateTimePeriodAsString(TimeSpan timePeriod)
        {
            return GenerateMagicStringOfElapsedTime(timePeriod);
        }

        public TimeSpan CalculateDateTimePeriodAsTimeSpan(DateTime startDate, DateTime endDate)
        {
            if (startDate == endDate)
            {
                return TimeSpan.Zero;
            }

            if (endDate == DateTime.MinValue)
            {
                endDate = DateTime.UtcNow;
            }
            var result = endDate - startDate;

            return new TimeSpan(0, result.Hours, result.Minutes, result.Seconds, result.Milliseconds);
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

        public TimeSpan AddTimeFromMagicStrings(ICollection<string> timeStrings)
        {
            TimeSpan result = TimeSpan.Zero;

            foreach (var time in timeStrings)
            {
                var timeArray = time.Split(":");
                result = result.Add(new TimeSpan(0, Convert.ToInt32(timeArray[0]), Convert.ToInt32(timeArray[1]), Convert.ToInt32(timeArray[2]), Convert.ToInt32(timeArray[3])));
            }

            return result;
        }
    }
}
