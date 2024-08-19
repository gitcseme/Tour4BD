using System.Collections;
using System.Collections.Generic;

namespace Domain.Entities;

public class Company : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string LisenceLink { get; set; } = string.Empty;

    public int OwnerId { get; set; }
    public ApplicationUser User { get; set; }
    public ICollection<Package> Packages { get; set; } = [];
    public ICollection<Rating> Ratings { get; set; } = [];
}
