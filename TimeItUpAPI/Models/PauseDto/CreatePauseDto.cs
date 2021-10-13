using System.ComponentModel.DataAnnotations;

namespace TimeItUpAPI.Models
{
    public class CreatePauseDto
    {
        [Required]
        public int TimerId { get; set; }
    }
}
