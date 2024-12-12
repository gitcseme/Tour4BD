using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<SystemUser, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<SystemUser> SystemUsers { get; set; }
    public DbSet<Visitor> Visitors { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Spot> Spots { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<TravelAgency> TravelAgencies { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
