
using System.Collections;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : BaseEntity<int>
{
    /// <summary>
    /// It refers to ExtendedIdentityUser
    /// </summary>
    public int UserId { get; set; }

    public ICollection<Company> Companies { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Comment> Comments { get; set; }
}