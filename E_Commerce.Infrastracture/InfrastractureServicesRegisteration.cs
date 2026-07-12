using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastracture.Data;
using E_Commerce.Infrastracture.DataSeeding;
using E_Commerce.Infrastracture.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastracture
{
    public static class InfrastractureServicesRegisteration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configration.GetConnectionString("DefaultConnection"));
            });
            services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IConnectionMultiplexer>(Config =>
            {
                
                return ConnectionMultiplexer.Connect(configration.GetConnectionString("RedisConnection")!);
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddSingleton<ICacheRepository,CacheRepository>();
            return services;
        }
    }
}
