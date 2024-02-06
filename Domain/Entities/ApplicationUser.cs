using System.Collections.Generic;

namespace Domain.Entities;

public class ApplicationUser : BaseEntity<int>
{
    /// <summary>
    /// It refers to  <see cref="ExtendedIdentityUser" />
    /// </summary>
    public int UserId { get; set; }

    public ICollection<Company> Companies { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Comment> Comments { get; set; }
}