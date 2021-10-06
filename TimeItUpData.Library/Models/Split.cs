﻿using System;

namespace TimeItUpData.Library.Models
{
    public class Split
    {
        public string Id { get; set; }

        public string TimerId { get; set; }
        public virtual Timer Timer { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime TotalDuration { get; set; }
    }
}
