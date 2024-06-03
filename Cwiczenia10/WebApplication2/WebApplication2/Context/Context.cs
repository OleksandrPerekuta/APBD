using WebApplication2.Entities;

namespace WebApplication2.Context;
using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    
    public DbSet<Role> Roles { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public Context()
    {
    }
    
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasMany(e => e.Products)
            .WithMany(e => e.Accounts)
            .UsingEntity<ShoppingCart>();

    }
}