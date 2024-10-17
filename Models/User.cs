namespace ContactSystem.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string? Name { get; set; }    
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public bool IsPasswordValid(string password)
        {
            return password == Password;
        }
    }
}
