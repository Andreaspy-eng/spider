using System;
using System.IO;
using System.Threading.Tasks;
using Autofac.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using YandexRouting;

namespace spider.Web;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
          var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true);
            
        Log.Logger = new LoggerConfiguration()
#if DEBUG
           // .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting web host.");
            
            var builder = WebApplication.CreateBuilder(args);
            builder.Host
                .AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            builder.Configuration.AddConfiguration(configuration.Build());
            builder.Services.AddHttpClient<IAdvantageService, AdvantageService>();
            builder.Services.AddHttpClient<ILocarusService, LocarusService>();
            builder.Services.AddHttpClient<ICounterpartyService, CounterpartyService>();
            builder.Services.AddHttpClient<IYandexRoutingService, YandexRoutingService>();
            builder.Services.AddAuthentication();
            await builder.AddApplicationAsync<spiderWebModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
