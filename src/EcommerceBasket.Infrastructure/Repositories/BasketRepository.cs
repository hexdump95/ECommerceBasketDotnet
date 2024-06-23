using System.Text.Json;

using AutoMapper;

using EcommerceBasket.Domain.Models;
using EcommerceBasket.Domain.Repositories;
using EcommerceBasket.Infrastructure.Data;
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

        public BasketRepository(RedisConnection redisConnection, JsonOptionsProvider jsonOptionsProvider,
            IMapper mapper)
        {
            _database = redisConnection.GetDatabase();
            _jsonOptions = jsonOptionsProvider.JsonOptions;
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
            basket.Id = id;
            return basket;
        }

        public async Task<Basket> Save(Basket basket)
        {
            var basketEntity = _mapper.Map<Basket, BasketEntity>(basket);
            var jsonString = JsonSerializer.Serialize(basketEntity, _jsonOptions);
            await _database.SetAddAsync($"{EntityPluralName}", basket.Id.ToString());
            await _database.SortedSetAddAsync($"{EntityPluralName}:updated_at", basket.Id.ToString(),
                basket.UpdatedAt.Ticks);
            await _database.StringSetAsync($"{EntityName}:{basket.Id}", jsonString);
            return basket;
        }

        public async Task<bool> Delete(Guid id)
        {
            var isSetDeleted = await _database.SetRemoveAsync(EntityPluralName, id.ToString());
            var isSortedSetDeleted =
                await _database.SortedSetRemoveAsync($"{EntityPluralName}:updated_at", id.ToString());
            var isStringDeleted = await _database.KeyDeleteAsync($"{EntityName}:{id}");
            return isSetDeleted && isSortedSetDeleted && isStringDeleted;
        }

        public async Task<bool> ExistsById(Guid id)
        {
            return await _database.SetContainsAsync(EntityPluralName, id.ToString());
        }

        public async Task<int> DeleteBasketRecordsToUpdatedAt(DateTime toDate)
        {
            var redisValues =
                await _database.SortedSetRangeByScoreAsync($"{EntityPluralName}:updated_at", 0, toDate.Ticks);

            int deletedRecords = 0;
            foreach (var redisValue in redisValues)
            {
                await Delete(Guid.Parse(redisValue!));
                deletedRecords++;
            }

            return deletedRecords;
        }
    }
}
