using Microsoft.EntityFrameworkCore;
using trucks.api.Extensions;
using trucks.api.IoC;
using Trucks.api.Apis;
using Trucks.api.IoC;
using Trucks.Infrastructure.Sql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TrucksDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("TrucksDbConnectionString"),
    sqlServerActions => sqlServerActions.MigrationsAssembly("trucks.infrastructure.sqlmigration")));
builder.Services.ConfigureServices();
builder.Services.ConfigureDatabaseServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.RunDatabaseMigrations();
app.UseHttpsRedirection();

app.MapGroup("api/v1/trucks")
    .MapTrucksApi();

app.Run();
