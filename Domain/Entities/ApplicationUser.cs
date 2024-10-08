﻿using System.Collections.Generic;

namespace Domain.Entities;

public class ApplicationUser : BaseEntity<int>
{
    public ApplicationUser(){}

    public ApplicationUser(int membershipId)
    {
        MembershipId = membershipId;
    }

    /// <summary>
    /// It refers to  <see cref="ExtendedIdentityTenantUser" />
    /// </summary>
    public int MembershipId { get; set; }

    public ICollection<Company> Companies { get; set; } = [];
    public ICollection<Rating> Ratings { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
}