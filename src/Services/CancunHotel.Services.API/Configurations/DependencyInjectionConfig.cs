using CancunHotel.Application.Interfaces;
using CancunHotel.Application.Services;
using CancunHotel.Domain.Interfaces.Repository;
using CancunHotel.Infra.Context;
using CancunHotel.Infra.Repository;

namespace CancunHotel.Services.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddValidationsConfiguration();
            
            // Repositories
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            
            // Services
            services.AddTransient<ICustomerAppService, CustomerAppService>();
            services.AddTransient<IBookingService, BookingService>();
            
            // Context
            services.AddTransient<ApplicationContext>();
        }
    }
}