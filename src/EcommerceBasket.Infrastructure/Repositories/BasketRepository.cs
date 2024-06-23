using System.Text.Json;

using AutoMapper;

using EcommerceBasket.Domain.Entities;
using EcommerceBasket.Domain.Repositories;
using EcommerceBasket.Infrastructure.Configuration;
using EcommerceBasket.Infrastructure.Persistence;

using StackExchange.Redis;

namespace EcommerceBasket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IMapper _mapper;
        const string EntityName = "basket";
        const string EntityPluralName = "baskets";

        public BasketRepository(RedisConfiguration redisConfiguration, IMapper mapper)
        {
            _database = redisConfiguration.GetDatabase();
            _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            _mapper = mapper;
        }

        public Task<List<Basket>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Basket?> FindById(Guid id)
        {
            var redisValue = await _database.StringGetAsync($"{EntityName}:{id}");
            if (redisValue.IsNullOrEmpty)
                return null;
            var basketEntity = JsonSerializer.Deserialize<BasketEntity>(redisValue!, _jsonOptions);
            var basket = _mapper.Map<BasketEntity, Basket>(basketEntity!);
            basket.Id = id.ToString();
            return basket;
        }

        public async Task<Basket> Save(Basket basket)
        {
            var basketEntity = _mapper.Map<Basket, BasketEntity>(basket);
            var jsonString = JsonSerializer.Serialize(basketEntity, _jsonOptions);
            await _database.SetAddAsync($"{EntityPluralName}", basket.Id);
            await _database.StringSetAsync($"{EntityName}:{basket.Id}", jsonString);
            return basket;
        }

        public async Task<bool> Delete(Guid id)
        {
            var isSetDeleted = await _database.SetRemoveAsync(EntityPluralName, id.ToString());
            var isStringDeleted = await _database.KeyDeleteAsync($"{EntityName}:{id}");
            return isSetDeleted && isStringDeleted;
        }

        public async Task<bool> ExistsById(Guid id)
        {
            return await _database.SetContainsAsync(EntityPluralName, id.ToString());
        }
    }
}
