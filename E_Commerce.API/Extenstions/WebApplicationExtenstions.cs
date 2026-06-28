using E_Commerce.Domain.Contracts;

namespace E_Commerce.API.Extenstions
{
    public static class WebApplicationExtenstions
    {
        public static async Task<WebApplication> SeedAndMigrateDataAsync(this WebApplication app)
        {
            using var scoped = app.Services.CreateScope();
            var seeder = scoped.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            await seeder.SeedDataAsync();
            return app;
        }
    }
}
