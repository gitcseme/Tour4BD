using Application.DTOs;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Features.Auth.Models;

public record LoginResponse(UserResponse User, string AccessToken, string RefreshToken);

public class UserResponse
{
    public UserResponse() {}

    public UserResponse(SystemUser user)
    {
        Id = user.Id;
        Name = user.UserName;
        Permissions = user.UserPermissions.Select(up => up.Permission.Name).ToList();
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public List<string> Permissions { get; set; }
}