using CancunHotel.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CancunHotel.Services.API.Factory;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql(
            "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=admin@post;");

        return new ApplicationContext(optionsBuilder.Options);
    }
}