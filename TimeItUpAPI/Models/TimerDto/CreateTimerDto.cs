using System;
using System.ComponentModel.DataAnnotations;

namespace TimeItUpAPI.Models
{
    public class CreateTimerDto
    {
        public string UserId { get; set; }

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
        public bool Paused { get; set; } = false;

        [Required]
        public bool Finished { get; set; } = false;
    }
}