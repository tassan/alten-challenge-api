using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;

namespace CancunHotel.Services.API.Configurations;

public static class ResponseCompressionConfig
{
    public static void AddResponseCompressionConfiguration(this IServiceCollection services)
    {
        services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
        });
    }
}