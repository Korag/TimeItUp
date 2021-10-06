using System;

namespace TimeItUpData.Library.Models
{
    public class Alarm
    {
        public string Id { get; set; }

        public string TimerId { get; set; }
        public virtual Timer Timer { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
