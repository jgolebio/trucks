using TruckHistory.API.Extensions;
using TrucksHistory.API.IoC;
using TrucksHistory.API.Apis;
using TrucksHistory.API.Extensions;
using TrucksHistory.API.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEventBus(builder.Configuration);
builder.Services.AddIntegrationEventHandlers();
builder.Services.AddMediatr();
builder.Services.AddTrucksHistoryDatabase(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureEventBus();
app.Services.RunDatabaseMigrations();
app.UseHttpsRedirection();

app.MapGroup("api/v1/trucks-history")
    .MapTrucksHistoryApi();

app.Run();