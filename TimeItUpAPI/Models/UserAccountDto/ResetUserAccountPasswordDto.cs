using System.ComponentModel.DataAnnotations;

namespace TimeItUpAPI.Models
{
    public class ResetUserAccountPasswordDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")]
        [CustomValidation(typeof(UpdateUserAccountPasswordDto), "ArePropertiesEqual")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
