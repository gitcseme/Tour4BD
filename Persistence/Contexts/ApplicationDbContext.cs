using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IJwtProvider _jwtProvider;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IJwtProvider jwtProvider)
        : base(options)
    {
        _jwtProvider = jwtProvider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        

        // TODO: dynamic tenant baased connection string
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(connectionString: _jwtProvider.GetConnectionStringFromToken(),
                builder =>
                {
                    builder.CommandTimeout(30);
                    builder.EnableRetryOnFailure(3);
                });
        }

        base.OnConfiguring(optionsBuilder);
    }

    public async Task<int> SaveAsync()
    {
        return await base.SaveChangesAsync();
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Spot> Spots { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
