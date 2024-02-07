using System.Collections.Generic;

namespace Domain.Entities;

public class Package : BaseEntity<int>
{
    public string Name { get; set; }

    public double Price { get; set; }
    public bool IsActive { get; set; }

    public int CompanyId { get; set; }
    public Company Comapny { get; set; }

    public ICollection<Spot> Spots { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Discount> Discounts { get; set; }
}
