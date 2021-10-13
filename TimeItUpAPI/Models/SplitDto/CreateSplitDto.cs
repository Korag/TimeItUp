using System.ComponentModel.DataAnnotations;

namespace TimeItUpAPI.Models
{
    public class CreateSplitDto
    {
        [Required]
        public int TimerId { get; set; }
    }
}
