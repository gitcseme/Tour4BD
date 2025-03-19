using Application.Features.Auth.Commands;
using Application.Features.Auth.Models;
using SharedKarnel.Contracts;
using Shouldly;
using System.Net.Http.Json;
using System.Text.Json;

namespace Application.IntegrationTests.Tests;

public class AuthTests : IntegrationTestBase
{
    const string Email = "kshuvo96@gmail.com";
    const string Password = "Shuvo$12345";

    public AuthTests(IntegrationTestWebApplicationFactory factory)
        : base(factory)
    {
    }


    [Fact]
    public async Task Register_And_Login_Test()
    {
        // Arrange
        var registerCommand = new RegisterCommand(Email, Password);
        await _requestSender.Send(registerCommand, default);

        var loginData = new LoginCommand(Email, Password, true);
        var content = new StringContent(JsonSerializer.Serialize(loginData), System.Text.Encoding.UTF8, "application/json");

        // Act
        var loginResponse = await _httpClient.PostAsync("http://localhost:8080/api/auth/login", content);
        loginResponse.EnsureSuccessStatusCode();

        var result = await loginResponse.Content.ReadFromJsonAsync<Result<LoginResponse>>();

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
    }

}