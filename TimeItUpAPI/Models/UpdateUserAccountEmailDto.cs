using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeItUpAPI.Models
{
    public class UpdateUserAccountEmailDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
     
        [Required]
        [DataType(DataType.EmailAddress)]
        public string NewEmail { get; set; }
    }
}
