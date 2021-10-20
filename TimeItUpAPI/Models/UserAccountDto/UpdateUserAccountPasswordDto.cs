using System.ComponentModel.DataAnnotations;

namespace TimeItUpAPI.Models
{
    public class UpdateUserAccountPasswordDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")]
        [CustomValidation(typeof(UpdateUserAccountPasswordDto), "ArePropertiesEqual")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }

        public static ValidationResult ArePropertiesEqual(UpdateUserAccountPasswordDto myEntity, ValidationContext validationContext)
        {
            if (myEntity.OldPassword != myEntity.NewPassword)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Your password cannot be the same like you are using right now");
        }
    }
}
