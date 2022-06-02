using Warehouse.Infrastructure.Persistence.Context;
using Warehouse.Infrastructure.Persistence.Repositories;
using Warehouse.Infrastructure.Persistence.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Warehouse.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WarehouseContext>(options =>
            {
                var connStr = configuration["WarehouseConnectionString"].ToString();

                options.UseSqlite(connStr);
            });
            services.AddScoped<IVehicleRepository, VehicleRepository>();

            return services;
        }
    }
}
