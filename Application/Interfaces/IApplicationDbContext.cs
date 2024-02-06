using System.Threading.Tasks;

using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; set; }
    DbSet<Package> Packages { get; set; }
    DbSet<Spot> Spots { get; set; }
    DbSet<Rating> Ratings { get; set; }
    DbSet<Discount> Discounts { get; set; }
    DbSet<Comment> Comments { get; set; }

    Task<int> SaveAsync();
}
