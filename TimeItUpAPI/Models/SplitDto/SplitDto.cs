using System;

namespace TimeItUpAPI.Models
{
    public class SplitDto
    {
        public int Id { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public string TotalDuration { get; set; } = "0d:0h:0m:0s:0ms";
        public TimeSpan TotalDurationTimeSpan { get; set; }
    }
}
