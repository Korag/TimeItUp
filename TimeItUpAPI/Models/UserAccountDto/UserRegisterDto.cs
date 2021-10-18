using System.ComponentModel.DataAnnotations;

namespace TimeItUpAPI.Models
{
    public class UserRegisterDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

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
