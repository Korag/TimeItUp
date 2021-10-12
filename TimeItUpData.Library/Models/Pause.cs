using System;
using System.ComponentModel.DataAnnotations;

namespace TimeItUpData.Library.Models
{
    public class Pause
    {
        [Key]
        public int Id { get; set; }

        public int TimerId { get; set; }
        public virtual Timer Timer { get; set; }

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
        [DataType(DataType.Time)]
        public TimeSpan TotalDurationTimeSpan { get; set; } = TimeSpan.Zero;
    }
}