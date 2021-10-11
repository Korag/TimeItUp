using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TimeItUpData.Library.Models
{
    public class Timer
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [StringLength(255, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartAt { get; set; } = DateTime.MinValue;

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndAt { get; set; } = DateTime.MinValue;

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string TotalDuration { get; set; } = "0d:0h:0m:0s:0ms";

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string TotalPausedTime { get; set; } = "0d:0h:0m:0s:0ms";

        [Required]
        public bool Paused { get; set; } = true;

        [Required]
        public bool Finished { get; set; } = false;

        [Required]
        public int SplitsNumber { get { return this.Splits.Count(); } }

        [Required]
        public int AlarmsNumber { get { return this.Alarms.Count(); }}

        [Required]
        public int PausesNumber { get { return this.Pauses.Count(); } }

        public virtual ICollection<Split> Splits { get; set; }
        public virtual ICollection<Alarm> Alarms { get; set; }
        public virtual ICollection<Pause> Pauses { get; set; }
    }
}
