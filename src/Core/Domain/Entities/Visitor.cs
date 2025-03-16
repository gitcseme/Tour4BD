using System.Collections.Generic;

namespace Domain.Entities;

public class Visitor : EntityBase<int>
{
    public Visitor(){}

    public Visitor(int systemUserId)
    {
        SystemUserId = systemUserId;
    }

    /// <summary>
    /// It refers to  <see cref="Entities.SystemUser" />
    /// </summary>
    public int SystemUserId { get; set; }
    public SystemUser SystemUser { get; set; }

    public ICollection<Rating> Ratings { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
}