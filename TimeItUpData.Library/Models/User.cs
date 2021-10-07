using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeItUpData.Library.Models
{
    public class User
    {
        public string Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        public virtual ICollection<Timer> Timers { get; set; }
    }
}
