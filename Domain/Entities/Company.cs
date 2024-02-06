using System.Collections;
using System.Collections.Generic;

namespace Domain.Entities;

public class Company : BaseEntity<int>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string LisenceLink { get; set; }

    public int OwnerId { get; set; }
    public ApplicationUser User { get; set; }
    public ICollection<Package> Packages { get; set; }
    public ICollection<Rating> Ratings { get; set; }
}
