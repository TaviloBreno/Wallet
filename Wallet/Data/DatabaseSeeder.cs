namespace Wallet.Data;

using Wallet.Models;

public static class DatabaseSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Name = "Admin", Email = "admin@wallet.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123") },
                new User { Name = "User", Email = "user@wallet.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123") }
            );
            context.SaveChanges();
        }
    }
}
