using System;

namespace TimeItUpAPI.Models
{
    public class TimerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public string TotalDuration { get; set; }
        public string TotalPausedTime { get; set; }
        public string TotalCountdownTime { get; set; }
        public bool Paused { get; set; }
        public bool Finished { get; set; }
        public int SplitsNumber { get; set; }
        public int AlarmsNumber { get; set; }
        public int PausesNumber { get; set; }
    }
}
