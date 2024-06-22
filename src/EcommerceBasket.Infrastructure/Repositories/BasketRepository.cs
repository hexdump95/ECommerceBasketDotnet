using System.Text.Json;

using EcommerceBasket.Domain.Entities;
using EcommerceBasket.Domain.Repositories;
using EcommerceBasket.Infrastructure.Configuration;

using StackExchange.Redis;

namespace EcommerceBasket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        private readonly JsonSerializerOptions _jsonOptions;
        const string entityName = "basket";

        public BasketRepository(RedisConfiguration redisConfiguration)
        {
            _database = redisConfiguration.GetDatabase();
            _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }

        public Task<List<Basket>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Basket?> FindById(Guid id)
        {
            var redisValue = await _database.StringGetAsync($"{entityName}:{id}");
            return redisValue.IsNullOrEmpty
                ? null
                : JsonSerializer.Deserialize<Basket>(redisValue!, _jsonOptions);
        }

        public async Task<Basket> Save(Basket basket)
        {
            var jsonString = JsonSerializer.Serialize(basket, _jsonOptions);
            await _database.StringSetAsync($"{entityName}:{basket.Id}", jsonString);
            return basket;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _database.KeyDeleteAsync($"{entityName}:{id}");
        }

        public async Task<bool> ExistsById(Guid id)
        {
            return await _database.KeyExistsAsync($"{entityName}:{id}");
        }
    }
}
