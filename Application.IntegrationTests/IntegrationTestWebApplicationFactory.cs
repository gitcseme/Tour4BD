using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

using Testcontainers.MsSql;

namespace Application.IntegrationTests
{
    public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithPassword("Strong_password_123!")
            .WithWaitStrategy(
                Wait.ForUnixContainer()
                    //.UntilPortIsAvailable(1433)
                    .UntilCommandIsCompleted("/opt/mssql-tools18/bin/sqlcmd", "-C", "-Q", "SELECT 1;")
            )
            .Build();

        public async Task InitializeAsync()
        {
            await msSqlContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                //var tenantServiceDescriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<TenantDbContext>));

                //if (tenantServiceDescriptor is not null)
                //{
                //    services.Remove(tenantServiceDescriptor);
                //}

                //var tenantInterfaceServiceDescriptor = ServiceDescriptor.Scoped<ITenantDbContext, TenantDbContext>();

                //if (tenantInterfaceServiceDescriptor is not null)
                //{
                //    services.Remove(tenantInterfaceServiceDescriptor);
                //}


                //var dockerInstanceConnectionString = msSqlContainer.GetConnectionString();
                //services.AddDbContext<TenantDbContext>(options =>
                //{
                //    options.UseSqlServer(dockerInstanceConnectionString);
                //});

                //services.AddScoped<ITenantDbContext, TenantDbContext>();

            });
        }

        public async new Task DisposeAsync()
        {
            await msSqlContainer.StopAsync();
        }
    }
}