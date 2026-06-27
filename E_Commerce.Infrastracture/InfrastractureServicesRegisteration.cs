using E_Commerce.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastracture
{
    public static class InfrastractureServicesRegisteration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration configration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}
