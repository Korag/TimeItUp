using System;

namespace TimeItUpAPI.Models
{
    public class PauseDto
    {
        public int Id { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public string TotalDuration { get; set; }
        public TimeSpan TotalDurationTimeSpan { get; set; }
    }
}
