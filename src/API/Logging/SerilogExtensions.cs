using Serilog;
using Serilog.Events;

namespace UnitConversion.Api.Logging;

/// <summary>
/// Central Serilog bootstrap. Sinks are driven by appsettings "Serilog" section so new
/// destinations can be added without code changes.
/// </summary>
/// <remarks>
/// Azure extension path:
/// 1. Add package, e.g. Serilog.Sinks.ApplicationInsights or Serilog.Sinks.AzureBlobStorage.
/// 2. Append a WriteTo entry under Serilog in appsettings.Production.json.
/// 3. Provide connection string / instrumentation key via environment variables or Key Vault.
/// File logs use Compact JSON (one event per line) for easy ingestion by Azure Monitor or Log Analytics.
/// </remarks>
public static class SerilogExtensions
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "UnitConversion.Api")
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);

            if (context.HostingEnvironment.IsDevelopment())
            {
                loggerConfiguration.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
            }
        });
    }
}
