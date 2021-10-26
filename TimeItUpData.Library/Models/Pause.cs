using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeItUpData.Library.Models
{
    public class Pause
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public string TotalDuration { get; set; } = "0:0:0:0";

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan TotalDurationTimeSpan { get; set; } = TimeSpan.Zero;

        public Pause()
        {

        }
    }
}