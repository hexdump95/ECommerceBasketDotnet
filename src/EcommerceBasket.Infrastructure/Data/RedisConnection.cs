using StackExchange.Redis;

namespace EcommerceBasket.Infrastructure.Data
{
    public class RedisConnection
    {
        private readonly ConnectionMultiplexer _connection;

        public RedisConnection(string connectionString)
        {
            _connection = ConnectionMultiplexer.Connect(connectionString);
        }

        public IDatabase GetDatabase()
        {
            return _connection.GetDatabase();
        }
    }
}
