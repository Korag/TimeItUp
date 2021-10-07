﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TimeItUpData.Library.Models
{
    public class Alarm
    {
        [Key]
        public int Id { get; set; }

        public int TimerId { get; set; }
        public virtual Timer Timer { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [StringLength(255, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Time { get; set; } = DateTime.UtcNow;
    }
}
