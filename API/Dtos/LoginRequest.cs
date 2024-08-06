using Domain.Entities;

namespace API.Dtos;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public UserResponse User { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

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