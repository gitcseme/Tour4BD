﻿using Domain.Entities;

namespace API.Dtos;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public ExtendedIdentityUser User { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}