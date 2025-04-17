
using Bogus;
using Domain.Entities;
using Persistence;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

var taf = new Faker<TravelAgency>()
    .RuleFor(x => x.Name, f => f.Company.CompanyName())
    .RuleFor(x => x.City, f => f.Address.City())
    .RuleFor(x => x.Country, f => f.Address.Country())
    .RuleFor(x => x.Address, f => f.Address.FullAddress());

var travelAgencies = taf.Generate(100);

await dbContext.TravelAgencies.AddRangeAsync(travelAgencies);
await dbContext.SaveChangesAsync();

