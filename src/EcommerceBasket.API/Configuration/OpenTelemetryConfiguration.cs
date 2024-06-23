using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace EcommerceBasket.API.Configuration
{
    public static class OpenTelemetryConfiguration
    {
        public static void ConfigureOpenTelemetry(this WebApplicationBuilder builder)
        {
            const string serviceName = "basket-service";

            builder.Logging.AddOpenTelemetry(options =>
            {
                options
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault()
                            .AddService(serviceName))
                    .AddConsoleExporter();
            });
            builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(serviceName))
                .WithTracing(tracing => tracing
                    .AddSource(serviceName)
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter())
                .WithMetrics(metrics => metrics
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter());
            builder.Services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));
        }
    }
}
