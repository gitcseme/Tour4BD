using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests;

public abstract class IntegrationTestBase 
    : IClassFixture<IntegrationTestWebApplicationFactory>, IDisposable
{
    protected readonly IServiceScope _serviceScope;
    protected readonly ISender _requestSender;
    protected readonly HttpClient _httpClient;

    public IntegrationTestBase(IntegrationTestWebApplicationFactory factory)
    {
        _serviceScope = factory.Services.CreateScope();
        _requestSender = _serviceScope.ServiceProvider.GetRequiredService<ISender>();
        _httpClient = factory.CreateClient();
    }

    public void Dispose()
    {
        _serviceScope?.Dispose();
    }
}
