using StackExchange.Redis;

namespace EcommerceBasket.Infrastructure.Configuration
{
    public class RedisConfiguration
    {
        private readonly ConnectionMultiplexer _connection;

        public RedisConfiguration(string connectionString)
        {
            _connection = ConnectionMultiplexer.Connect(connectionString);
        }

        public IDatabase GetDatabase()
        {
            return _connection.GetDatabase();
        }
    }
}
