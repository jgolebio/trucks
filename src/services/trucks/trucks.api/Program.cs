using Microsoft.EntityFrameworkCore;
using Trucks.API.Extensions;
using Trucks.API.IoC;
using Trucks.API.Apis;
using Trucks.API.IoC;
using Trucks.Infrastructure.Sql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TrucksDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("TrucksDbConnectionString"),
        sqlServerActions => sqlServerActions.MigrationsAssembly("trucks.infrastructure.sqlmigration"));
    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.ConfigureServices();
builder.Services.ConfigureDatabaseServices();
builder.Services.AddEventBus(builder.Configuration);

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
