using System.ComponentModel.DataAnnotations;

namespace TimeItUpAPI.Models
{
    public class AddUserDto
    {
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
    }
}
