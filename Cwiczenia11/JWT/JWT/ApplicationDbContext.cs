using JWT.Controllers;
using JWT.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ApplicationDbContext = JWT.Context.ApplicationDbContext;

namespace JWT.Context;


public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    private DbSet<User> Users { get; set; }

}