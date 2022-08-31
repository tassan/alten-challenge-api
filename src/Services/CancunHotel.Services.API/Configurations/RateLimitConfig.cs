using AspNetCoreRateLimit;

namespace CancunHotel.Services.API.Configurations;

public static class RateLimitConfig
{
    public static void AddRateLimitConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ClientRateLimitOptions>(configuration.GetSection("ClientRateLimiting"));
        services.Configure<ClientRateLimitPolicies>(configuration.GetSection("ClientRateLimitPolicies"));
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }
}