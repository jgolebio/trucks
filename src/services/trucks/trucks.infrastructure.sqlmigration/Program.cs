
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Trucks.Infrastructure.Sql;

namespace Trucks.Infrastructure.SqlMigration;

public class Program
{
    public static readonly string AppName = typeof(Program).Assembly.GetName().Name!;

    public static async Task Main(string[] args)
    {
        try
        {
            var host = CreateHostBuilder(args).Build();

            Log.Information("STARTING HOST ({ApplicationContext})...", AppName);

            var config = host.Services.GetService<IConfiguration>();
            var commandLineArgs = config!["CommandLineArgs"];

            if (args.ToList().Contains("--run-migration") || commandLineArgs.Contains("--run-migration"))
            {
                Log.Information("STARTING DB MIGRATION...", AppName);

                using (var scope = host.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<TrucksDbContext>();
                    db.Database.Migrate();
                }

                Log.Information("MIGRATION SUCCESSFULLY ENDED", AppName);
            }

            await Task.Delay(15000); //end after 15 sec

            Log.Information("HOST ENDED ({ApplicationContext})...", AppName);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "HOST TERMINATED UNEXPECTEDLY ({ApplicationContext})! Error: {ErrorMessage} StackTrace: {StackTrace}", AppName, ex.Message, ex.StackTrace);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog((host, services, loggerConfiguration) =>
            {
                var seqServerUrl = host.Configuration["Serilog:SeqServerUrl"];
                var logstashUrl = host.Configuration["Serilog:LogstashgUrl"];
                loggerConfiguration.ReadFrom.Configuration(host.Configuration)
                    .Enrich.WithProperty("ApplicationContext", AppName)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
                    .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl, null);
            })
            .ConfigureAppConfiguration(x => x.AddConfiguration(GetConfiguration()))
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<TrucksDbContext>(options =>
                {
                    options.UseSqlServer(
                        hostContext.Configuration["TrucksDbConnectionString"],
                        b => b.MigrationsAssembly("Trucks.Infrastructure.Sql"));
                });
            });

    static IConfiguration GetConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
