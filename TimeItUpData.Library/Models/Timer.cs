using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TimeItUpData.Library.Models
{
    public class Timer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [DataType(DataType.Time)]
        public TimeSpan TotalDurationTimeSpan { get; set; } = TimeSpan.Zero;

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan TotalPausedTimeSpan { get; set; } = TimeSpan.Zero;

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan TotalCountdownTimeSpan { get; set; } = TimeSpan.Zero;

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string TotalDuration { get; set; } = "0:0:0:0";

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string TotalPausedTime { get; set; } = "0:0:0:0";

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string TotalCountdownTime { get; set; } = "0:0:0:0";

        [Required]
        public bool Paused { get; set; } = false;

        [Required]
        public bool Finished { get; set; } = false;

        [Required]
        public int SplitsNumber { get { return this.Splits.Count(); } }

        [Required]
        public int AlarmsNumber { get { return this.Alarms.Count(); } }

        [Required]
        public int PausesNumber { get { return this.Pauses.Count(); } }

        public virtual ICollection<Split> Splits { get; set; } = new List<Split>();
        public virtual ICollection<Alarm> Alarms { get; set; } = new List<Alarm>();
        public virtual ICollection<Pause> Pauses { get; set; } = new List<Pause>();

        public Timer()
        {

        }
    }
}
