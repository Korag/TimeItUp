using System;
using System.ComponentModel.DataAnnotations;

namespace TimeItUpAPI.Models
{
    public class UpdateAlarmDto
    {
        [Required]
        [DataType(DataType.Text)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [StringLength(255, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ActivationTime { get; set; }
    }
}
