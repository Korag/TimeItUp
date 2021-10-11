using System.ComponentModel.DataAnnotations;

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
