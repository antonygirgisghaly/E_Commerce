using E_Commerce.Domain.Comman;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Infrastracture.DataSeeding
{
    internal class CatalogDataSeeder(StoreDbContext dbContext,ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigrations.Any())
                    await dbContext.Database.MigrateAsync(ct);
                    var defaultPath = Path.Combine(AppContext.BaseDirectory, "DataSeed");

                    await SeedIfEmpty<ProductBrand, int>(defaultPath, "brands.json", ct);
                    await SeedIfEmpty<ProductType, int>(defaultPath, "types.json", ct);
                    await SeedIfEmpty<Product, int>(defaultPath, "products.json", ct);

                    int result =  await dbContext.SaveChangesAsync(ct);
                    if(result > 0)
                    {
                        logger.LogInformation($"{result} Rows Added");
                        return;
                    }
                    else
                    {
                        logger.LogInformation($"Database already seeded");
                    }
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during seeding");
                throw;
            }
        }

        private async Task SeedIfEmpty<T,Tkey>(string rootPath,string folderName ,CancellationToken ct) where T : BaseEntity<Tkey>
        {
            if(await dbContext.Set<T>().AnyAsync())
            {
                logger.LogInformation("Table already has data");
                return;
            }
            var filePath = Path.Combine(rootPath,folderName);
            if (!File.Exists(filePath))
            {
                logger.LogWarning($"File {filePath} is not exists");
                return;
            }
            using var fileStream = File.OpenRead(filePath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream,options,ct);
            if (items?.Any() ?? false)
            {
                await dbContext.Set<T>().AddRangeAsync(items, ct);
            }
        }
    }
}
