using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace ShipTrack.Shared.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<RedisCacheService> _logger;

    public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        try
        {
            var data = await _cache.GetStringAsync(key);
            if (data is null) return default;

            _logger.LogDebug("[Cache HIT] Key: {Key}", key);
            return JsonSerializer.Deserialize<T>(data);
        }
        catch (Exception ex)
        {
            _logger.LogWarning("[Cache ERROR] Get failed for key {Key}: {Error}", key, ex.Message);
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        try
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(5)
            };

            var data = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, data, options);
            _logger.LogDebug("[Cache SET] Key: {Key}", key);
        }
        catch (Exception ex)
        {
            _logger.LogWarning("[Cache ERROR] Set failed for key {Key}: {Error}", key, ex.Message);
        }
    }

    public async Task RemoveAsync(string key)
    {
        try
        {
            await _cache.RemoveAsync(key);
            _logger.LogDebug("[Cache REMOVE] Key: {Key}", key);
        }
        catch (Exception ex)
        {
            _logger.LogWarning("[Cache ERROR] Remove failed for key {Key}: {Error}", key, ex.Message);
        }
    }

    public async Task RemoveByPrefixAsync(string prefix)
    {
        // Basit implementasyon — ileride Lua script ile genişletilebilir
        await RemoveAsync(prefix);
    }
}
