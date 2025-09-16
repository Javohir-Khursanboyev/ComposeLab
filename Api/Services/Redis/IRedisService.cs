namespace Api.Services.Redis;

public interface IRedisService
{
    Task SetToRedisAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task<string?> GetFromRedisAsync(string key);
    Task RemoveFromRedisAsync(string key);
}