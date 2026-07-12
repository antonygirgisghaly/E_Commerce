using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Profiles;
using E_Commerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using static E_Commerce.Application.Profiles.PictureUrlResolver;
namespace E_Commerce.Application
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection service)
        {
            service.AddAutoMapper(c => { },typeof(ApplicationServicesRegisteration).Assembly);
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IBasketService, BasketService>();
            service.AddScoped<ICacheService, CacheService>();
            return service;
        }
    }
}
