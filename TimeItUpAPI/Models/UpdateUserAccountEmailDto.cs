using System.ComponentModel.DataAnnotations;

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
