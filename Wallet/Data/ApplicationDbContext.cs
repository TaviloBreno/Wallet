using Microsoft.EntityFrameworkCore;
using Wallet.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
}
