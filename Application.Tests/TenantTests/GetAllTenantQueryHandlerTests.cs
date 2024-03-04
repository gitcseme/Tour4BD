using Application.Features.TenantFeatures.Queries;
using Application.Interfaces;
using Application.Tests.TestHelpers;

using Domain.Entities;

using NSubstitute;

using Shouldly;

namespace Application.Tests.TenantTests;

public class GetAllTenantQueryHandlerTests
{
    private readonly GetAllTenantQueryHandler _handler;
    private readonly ITenantUnitOfWork _tenantUnitOfWorkMock;

    public GetAllTenantQueryHandlerTests()
    {
        _tenantUnitOfWorkMock = Substitute.For<ITenantUnitOfWork>();

        _handler = new GetAllTenantQueryHandler(_tenantUnitOfWorkMock);
    }

    [Fact]
    public async Task Handle_WhenInvoked_ShouldReturnTenants()
    {
        // Arrange
        var tenants = new List<Tenant>
        {
            new() { Id = 1, OrganizationName = "VS-1", ConnectionString = "con-1" },
            new() { Id = 2, OrganizationName = "VS-2", ConnectionString = "con-2" },
        }.AsAsyncQueryable();

        _tenantUnitOfWorkMock.TenantRepository.GetAll().Returns(tenants);

        // Act
        var result = await _handler.Handle(Arg.Any<GetAllTenantQuery>(), default);

        // Assert
        result.Count().ShouldBe(2);
    }

}
