using API.Extensions;
using Persistence;
using Application;
using Membership;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppSettingsConfiguration(builder.Configuration);

builder.Services
    .AddPersistence(builder.Configuration)
    .AddApplication()
    .AddMembership()
    .AddAuthenticationWithJwt(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

public partial class Program { }