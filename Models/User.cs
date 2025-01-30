using System.ComponentModel.DataAnnotations;

namespace TopSecret.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty; // Imposta un valore di default

        [Required]
        public string Pin { get; set; } = string.Empty; // Imposta un valore di default

        public int Points { get; set; } = 0;
    }
}
