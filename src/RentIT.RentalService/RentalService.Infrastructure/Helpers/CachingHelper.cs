using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Text.Json;

namespace RentalService.Infrastructure.Helpers;

public class CachingHelper
{
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger<CachingHelper> _logger;
    public CachingHelper(IDistributedCache distributedCache, ILogger<CachingHelper> logger)
    {
        _distributedCache = distributedCache;
        _logger = logger;
    }
    public async Task<(bool Found, T? Value)> GetCachedObject<T>(string cacheKey)
    {
        var stringObj = await _distributedCache.GetStringAsync(cacheKey);

        if (stringObj is null)
            return (false, default);

        try
        {

            T? cachedObj = JsonSerializer.Deserialize<T>(stringObj);
            return (true, cachedObj);
        }
        catch (JsonException)
        {
            await _distributedCache.RemoveAsync(cacheKey);
            _logger.LogWarning($"Found corrupted cache of key: {cacheKey} - removing");
            return (false, default);
        }

    }

    public async Task CacheObject<T>(T obj, string cacheKey, DistributedCacheEntryOptions options)
    {
        string objSerialized;
        try
        {
            objSerialized = JsonSerializer.Serialize<T>(obj);
            await _distributedCache.SetStringAsync(cacheKey, objSerialized, options);
            _logger.LogInformation($"Successfully saved key: {cacheKey} object to cache");
        }
        catch
        {
            _logger.LogWarning($"Caching for object of key: {cacheKey} failed");
        }
    }

    public static string GenerateCacheKey(string prefix, Guid id) => $"{prefix}:{id}";

    public static string GenerateCacheKey(string prefix, IEnumerable<Guid> ids)
    {
        var sortedIds = ids.OrderBy(id => id);
        string id = string.Join("-", sortedIds);
        return $"{prefix}:{id}";
    }
}

