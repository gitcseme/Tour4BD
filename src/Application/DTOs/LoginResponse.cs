﻿namespace Application.DTOs;

public class LoginResponse
{
    public UserResponse User { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
