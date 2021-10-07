using System;
using System.ComponentModel.DataAnnotations;

namespace TimeItUpData.Library.Models
{
    public class Split
    {
        [Key]
        public int Id { get; set; }

        public int TimerId { get; set; }
        public virtual Timer Timer { get; set; }

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
    }
}
