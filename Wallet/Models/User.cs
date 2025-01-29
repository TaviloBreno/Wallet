namespace Wallet.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string Role { get; set; } = "User";
    }
}
