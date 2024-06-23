using System.Text.Json;

namespace EcommerceBasket.Infrastructure
{
    public class JsonOptionsProvider
    {
        public JsonSerializerOptions JsonOptions { get; }

        public JsonOptionsProvider()
        {
            JsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }
    }
}
