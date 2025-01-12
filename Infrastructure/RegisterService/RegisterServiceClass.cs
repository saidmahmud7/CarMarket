using Infrastructure.Data;
using Infrastructure.Profile;
using Infrastructure.Service.BrandService;
using Infrastructure.Service.CarService;
using Infrastructure.Service.CityService;
using Infrastructure.Service.DealerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RegisterService;

public static class RegisterServiceClass
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICarService,CarService>();
        services.AddScoped<IDealerService, DealerService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddAutoMapper(typeof(InfrastructureProfile));
        services.AddDbContext<DataContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}