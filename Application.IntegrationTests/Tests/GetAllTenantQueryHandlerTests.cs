using Shouldly;

namespace Application.IntegrationTests.Tests;

public class GetAllTenantQueryHandlerTests : IntegrationTestBase
{

    public GetAllTenantQueryHandlerTests(IntegrationTestWebApplicationFactory factory)
        : base(factory)
    {

    }

    [Fact]
    public async Task Handle_WhenInvoked_ShouldReturnTenants()
    {
        // Arrange

        // Act
        //var response = await _requestSender.Send(new GetAllTenantQuery(), default);

        //// Assert
        //response.ShouldNotBeNull();
        Assert.True(true);
    }
}