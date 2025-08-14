using Bogus;
using Domain.Entities;
using Persistence;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

var packageFaker = new Faker<Package>()
    .RuleFor(p => p.Name, pf => pf.Commerce.ProductName())
    .RuleFor(p => p.Description, pf => pf.Commerce.ProductDescription())
    .RuleFor(p => p.Price, pf => pf.Random.Double(100, 10000))
    .RuleFor(p => p.IsActive, pf => pf.Random.Bool());

var companyFaker = new Faker<Company>()
    .RuleFor(c => c.Name, cf => cf.Company.CompanyName())
    .RuleFor(c => c.Address, cf => cf.Address.FullAddress())
    .RuleFor(c => c.LisenceLink, cf => cf.Internet.Url())
    .RuleFor(c => c.IsActive, cf => cf.Random.Bool())
    .RuleFor(c => c.Packages, cf => packageFaker.Generate(1000));

var taf = new Faker<TravelAgency>()
    .RuleFor(x => x.Name, f => f.Company.CompanyName())
    .RuleFor(x => x.City, f => f.Address.City())
    .RuleFor(x => x.Country, f => f.Address.Country())
    .RuleFor(x => x.Address, f => f.Address.FullAddress())
    .RuleFor(x => x.Companies, f => companyFaker.Generate(100));

try
{
    for (var i = 1; i <= 10; ++i)
    {
        var travelAgencies = taf.Generate(1);
        dbContext.TravelAgencies.AddRange(travelAgencies);
        dbContext.SaveChanges();
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
}