using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeItUpData.Library.Models
{
    public class Alarm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TimerId { get; set; }
        public virtual Timer Timer { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ActivationTime { get; set; } = DateTime.UtcNow.AddDays(1);

        public Alarm()
        {

        }
    }
}
