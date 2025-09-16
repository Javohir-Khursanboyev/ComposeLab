using System.Text.Json;
using StackExchange.Redis;

namespace Api.Services.Redis;

public sealed class RedisService(IDatabase db) : IRedisService
{
    public async Task SetToRedisAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, json, expiry);
    }

    public async Task<string?> GetFromRedisAsync(string key)
    {
        var value = await db.StringGetAsync(key);
        return value.HasValue ? JsonSerializer.Deserialize<string>(value!) : default;
    }

    public async Task RemoveFromRedisAsync(string key)
    {
        await db.KeyDeleteAsync(key);
    }
}
