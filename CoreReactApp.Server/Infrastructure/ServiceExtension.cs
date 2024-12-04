using Microsoft.EntityFrameworkCore;
using CoreReactApp.Server.Repositories.Implementation;
using CoreReactApp.Server.Repositories.Interface;
using CoreReactApp.Server.Services.Interface;
using CoreReactApp.Server.Services.Implementation;
using CoreReactApp.Server.Data;
using CoreReactApp.Server.Services;
using CoreReactApp.Server.Repositories;
using CoreReactApp.Server.API;

namespace CoreReactApp.Server.Infrastructure
{
    public static class ServiceExtension
    {
        public static void ConfigureDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void AddDIRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IFlightRepository, FlightRepository>();
        }

        public static void AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IAirportService, AirportService>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .WithOrigins("https://localhost:52477")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("FlightAPI", client => {
                client.BaseAddress = new Uri("https://test.api.amadeus.com");
            });

            services.AddScoped<FlightAPI>();
        }
    }
}
