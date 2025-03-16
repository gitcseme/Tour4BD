using Application.DTOs;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Features.Auth.Models;

public record LoginResponse(UserResponse User, string AccessToken, string RefreshToken);

public class UserResponse(SystemUser user)
{
    public int Id { get; set; } = user.Id;
    public string? Name { get; set; } = user.UserName;
    public List<string> Permissions { get; set; } = user.UserPermissions.Select(up => up.Permission.Name).ToList();
}