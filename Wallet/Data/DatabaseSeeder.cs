namespace Wallet.Data;

using Wallet.Models;

public static class DatabaseSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        var adminUser = context.Users.FirstOrDefault(u => u.Email == "admin@wallet.com");
        var regularUser = context.Users.FirstOrDefault(u => u.Email == "user@wallet.com");

        if (adminUser != null && adminUser.WalletBalance == 0)
        {
            adminUser.WalletBalance = 1000.00m;
        }

        if (regularUser != null && regularUser.WalletBalance == 0)
        {
            regularUser.WalletBalance = 500.00m;
        }

        if (adminUser != null || regularUser != null)
        {
            context.SaveChanges();
        }
    }
}
