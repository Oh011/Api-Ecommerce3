using Domain.Contracts;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer ConnectionMultiplexer) : IBasketRepository
    {

        private readonly IDatabase _database = ConnectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {

            var value = await _database.StringGetAsync(id);

            // asks Redis to get the value stored under the key id.
            //Returns a RedisValue type from StackExchange.Redis.

            //RedisValues --> struct type


            if (value.IsNullOrEmpty) return null;


            return JsonSerializer.Deserialize<CustomerBasket?>(value);



        }

        public async Task<CustomerBasket?> UpdateBasket(CustomerBasket basket, TimeSpan? TTL = null)
        {

            var JsonBasket = JsonSerializer.Serialize(basket);

            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.id, JsonBasket, TTL ?? TimeSpan.FromDays(20));

            return IsCreatedOrUpdated ? await GetBasketAsync(basket.id) : null;
        }
    }
}


//RedisValue?
//RedisValue is like a smart wrapper around strings and binary data.
//It can be:

//A string
//A number
//JSON
//Even binary data (like byte arrays)


// What is TimeSpan?
//TimeSpan is a.NET structure that represents a duration of time — not a date/time, but how long
//something lasts.
//TimeSpan fiveMinutes = TimeSpan.FromMinutes(5);
//TimeSpan twoHours = TimeSpan.FromHours(2);
//TimeSpan oneDay = TimeSpan.FromDays(1);
//TimeSpan tenSeconds = TimeSpan.FromSeconds(10);

//TimeSpan.Hours, .Minutes Get components
//TimeSpan.TotalSeconds	Get total time in seconds (as double)
//TimeSpan.Zero	Zero duration (like TimeSpan(0))