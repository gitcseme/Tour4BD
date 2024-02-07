using API.MigrationHelpers;

using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string tenantDbConnectionString = builder.Configuration.GetConnectionString("TenantDbConnection")!;
builder.Services.AddPersistence(tenantDbConnectionString);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.MigrateAsync();

app.Run();
