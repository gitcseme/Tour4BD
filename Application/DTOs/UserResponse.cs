using Domain.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Application.DTOs;

public class UserResponse
{
    public UserResponse(ExtendedIdentityUser user)
    {
        Id = user.Id;
        Name = user.UserName;
        Permissions = user.Permissions.Select(p => p.Name).ToList();
    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<string> Permissions { get; set; }
}