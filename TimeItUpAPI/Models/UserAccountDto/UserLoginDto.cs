using System.ComponentModel.DataAnnotations;

namespace TimeItUpAPI.Models
{
    public class UserLoginDto
    {
        [Required]
        [StringLength(40, MinimumLength = 2)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")]
        public string Password { get; set; }
    }
}
