using System;
using System.ComponentModel.DataAnnotations;

namespace TimeItUpAPI.Models
{
    public class CreateSplitDto
    {
        [Required]
        public int TimerId { get; set; }

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
