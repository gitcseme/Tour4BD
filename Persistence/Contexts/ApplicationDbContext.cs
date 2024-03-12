using Application.Interfaces;
using Domain.Entities;
using Persistence.EntityConfigurations;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: dynamic tenant baased connection string
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(connectionString: "",
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
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new CompanyConfiguration());
        builder.ApplyConfiguration(new PackageConfiguration());
        builder.ApplyConfiguration(new SpotConfiguration());
        builder.ApplyConfiguration(new RatingConfiguration());
        builder.ApplyConfiguration(new DiscountConfiguration());
        builder.ApplyConfiguration(new CommentConfiguration());

        base.OnModelCreating(builder);
    }
}

