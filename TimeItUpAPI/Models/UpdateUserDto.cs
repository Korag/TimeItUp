using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeItUpAPI.Models
{
    public class UpdateUserDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        [DataType(DataType.EmailAddress)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
    }
}
