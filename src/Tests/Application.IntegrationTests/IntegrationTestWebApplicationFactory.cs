using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Testcontainers.MsSql;


namespace Application.IntegrationTests
{
    public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithPassword("Strong_password_123!")
            //.WithWaitStrategy(
            //    Wait.ForUnixContainer()
            //        //.UntilPortIsAvailable(1433)
            //        .UntilCommandIsCompleted("/opt/mssql-tools18/bin/sqlcmd", "-C", "-Q", "SELECT 1;")
            //)
            .WithCleanUp(true)
            .Build();

        public async Task InitializeAsync()
        {
            await _msSqlContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    string containerConnectionString = _msSqlContainer.GetConnectionString();
                    options.UseSqlServer(containerConnectionString, optBuilder =>
                    {
                        optBuilder.CommandTimeout(30);
                        optBuilder.EnableRetryOnFailure(3);
                    })
                        .EnableDetailedErrors()
                        .EnableSensitiveDataLogging();
                });
            });
        }

        public async new Task DisposeAsync()
        {
            await _msSqlContainer.StopAsync();
        }
    }
}