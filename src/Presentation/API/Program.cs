using API.Extensions;
using API.Middlewares;
using Application;
using Membership;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddBasicMvcConfiguration();

builder.Services
    .AddPersistence(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddMembership()
    .AddAuthenticationWithJwt(builder.Configuration)
    .AddSwaggerConfiguration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
    
await app.MigrateAsync();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

namespace API
{
    public partial class Program { }
}