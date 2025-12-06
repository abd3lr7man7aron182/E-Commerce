using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities.BasketModule;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        
        private readonly IDatabase database;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            
            database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeTolive = default)
        {
            var JsonBasket=JsonSerializer.Serialize(basket);
           var IsCreatedOrUpdated= await database.StringSetAsync(basket.Id, JsonBasket,
               (timeTolive == default) ? TimeSpan.FromDays(7) : timeTolive);
            if (IsCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteBasketAsync(string basketId)=>  await database.KeyDeleteAsync(basketId);
        

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var Basket=await database.StringGetAsync(basketId);
            if(Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }
    }
}
