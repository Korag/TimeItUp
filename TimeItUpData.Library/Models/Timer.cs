using System;
using System.Collections.Generic;

namespace TimeItUpData.Library.Models
{
    public class Timer
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public DateTime TotalDuration { get; set; }
        public DateTime TotalPausedTime { get; set; }

        public bool Paused { get; set; }
        public bool Finished { get; set; }

        public int SplitsNumber { get; set; }
        public int AlarmsNumber { get; set; }

        public virtual ICollection<Split> Splits { get; set; }
        public virtual ICollection<Alarm> Alarms { get; set; }
    }
}
