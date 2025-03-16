using Application.Features.Auth.Commands;
using Shouldly;
using System.Net.Http.Json;

namespace Application.IntegrationTests.Tests;

public class AuthTests : IntegrationTestBase
{

    public AuthTests(IntegrationTestWebApplicationFactory factory)
        : base(factory)
    {

    }

    [Fact]
    public async Task Try_Register_A_Valid_User()
    {
        // Arrange
        var registerCommand = new RegisterCommand("kshuvo96@gmail.com", "Shuvo$12345");

        // Act
        var response = await _requestSender.Send(registerCommand, default);

        // Assert
        response.Data.ShouldBeTrue();
    }

}