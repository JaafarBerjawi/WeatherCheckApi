using System.ComponentModel.DataAnnotations;

namespace Security.Entities
{
    public class GenerateTokeInput
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
