
using E_Commerce.API.Extenstions;
using E_Commerce.Application;
using E_Commerce.Application.Profiles;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastracture;
using Microsoft.Extensions.FileProviders;
namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddApplicationService();
            builder.Services.Configure<UrlSettings>(builder.Configuration.GetSection("UrlSeetings"));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            await app.SeedAndMigrateDataAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Files")),
                RequestPath = "/Files"
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
