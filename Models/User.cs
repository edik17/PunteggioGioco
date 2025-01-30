namespace TopSecret.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Pin { get; set; }
        public int Points { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}