using CancunHotel.Domain.Validations;

namespace CancunHotel.Services.API.Configurations;

public static class ValidationsConfig
{
    public static void AddValidationsConfiguration(this IServiceCollection services)
    {
        services.AddScoped<CustomerValidation>();
        services.AddScoped<RegisterCustomerValidation>();
        services.AddScoped<ReservationValidation>();
        services.AddScoped<BookingValidation>();
    }
}