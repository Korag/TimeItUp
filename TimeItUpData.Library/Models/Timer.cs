using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public DateTime StartAt { get; set; } = DateTime.UtcNow;

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndAt { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string TotalDuration { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string TotalPausedTime { get; set; }

        [Required]
        public bool Paused { get; set; }

        [Required]
        public bool Finished { get; set; }

        [Required]
        public int SplitsNumber { get; set; }

        [Required]
        public int AlarmsNumber { get; set; }

        public virtual ICollection<Split> Splits { get; set; }
        public virtual ICollection<Alarm> Alarms { get; set; }
    }
}
