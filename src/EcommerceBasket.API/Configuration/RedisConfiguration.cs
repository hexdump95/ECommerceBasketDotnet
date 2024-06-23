using EcommerceBasket.Infrastructure.Data;

namespace EcommerceBasket.API.Configuration
{
    public static class RedisConfiguration
    {
        public static void ConfigureRedis(this WebApplicationBuilder builder)
        {
            var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection")!;
            builder.Services.AddSingleton(new RedisConnection(redisConnectionString));
        }
    }
}
