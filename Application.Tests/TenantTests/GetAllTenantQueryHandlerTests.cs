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
    private readonly IUnitOfWork _uow;

    public GetAllTenantQueryHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();

        _handler = new GetAllTenantQueryHandler(_uow);
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

        _uow.Repository<Tenant, int>().Query(asNoTracking: true).Returns(tenants);

        // Act
        var result = await _handler.Handle(new GetAllTenantQuery(), default);

        // Assert
        result.Count().ShouldBe(2);
    }

}
