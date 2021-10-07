using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeItUpAPI.Models
{
    public class UpdateUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
