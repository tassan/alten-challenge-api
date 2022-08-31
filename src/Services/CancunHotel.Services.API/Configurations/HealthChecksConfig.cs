namespace CancunHotel.Services.API.Configurations;

public static class HealthChecksConfig
{
    public static void AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("DefaultConnection"))
            .AddApplicationInsightsPublisher();

        services.AddHealthChecksUI(opt =>
        {
            opt.SetEvaluationTimeInSeconds(60);
            opt.MaximumHistoryEntriesPerEndpoint(60);
            opt.SetApiMaxActiveRequests(1);
            opt.AddHealthCheckEndpoint("Cancun Hotel API", "/healthz");
        })
            .AddInMemoryStorage();
    }
}