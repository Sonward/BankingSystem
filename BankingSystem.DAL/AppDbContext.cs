using BankingSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasKey(a => a.Id);
        modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
    }
}
