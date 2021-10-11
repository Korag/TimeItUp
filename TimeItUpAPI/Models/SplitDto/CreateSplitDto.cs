using System;
using System.ComponentModel.DataAnnotations;

namespace TimeItUpAPI.Models
{
    public class CreateSplitDto
    {
        public int Id { get; set; }
        public int TimerId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartAt { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndAt { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string TotalDuration { get; set; }
    }
}
