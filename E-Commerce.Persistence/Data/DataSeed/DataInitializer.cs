using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext dbContext;

        public DataInitializer(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task InitializeAsync()
        {
            try
            {
                var hasProducts = await dbContext.Products.AnyAsync();
                var hasBrands = await dbContext.ProductBrands.AnyAsync();
                var hasTypes = await dbContext.ProductTypes.AnyAsync();
                if (hasBrands && hasTypes && hasProducts) return;

                if (!hasBrands)
                    await SeedDataFromJsonAsync<ProductBrand, int>("brands.json", dbContext.ProductBrands);
                if (!hasTypes)
                    await SeedDataFromJsonAsync<ProductType, int>("types.json", dbContext.ProductTypes);
                await dbContext.SaveChangesAsync();

                if (!hasProducts)
                    await SeedDataFromJsonAsync<Product, int>("products.json", dbContext.Products);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data Seeding Failed : {ex}");
            }

        }


        private async Task SeedDataFromJsonAsync<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {

            var filePath = @"..\E-Commerce.Persistence\Data\DataSeed\JSONFiles\" + fileName;
            if (!File.Exists(filePath)) throw new FileNotFoundException($"File {fileName} is not Exists");

            try
            {
                //var Data=File.ReadAllText(filePath); good for small files => it load all file data you need or not 

                using var dataStream = File.OpenRead(filePath);

                var Data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                if (Data is not null)
                {
                    dbset.AddRange(Data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Readind JSON File : {ex}");
                return;
            }
        }
    }
}
