using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EquipmentService.Core.Caching;

public class CachingHelper : ICachingHelper
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
        byte[]? objInBytes = await _distributedCache.GetAsync(cacheKey);
        if (objInBytes is null)
            return (false, default);

        try
        {

            T? cachedObj = JsonSerializer.Deserialize<T>(objInBytes);
            _logger.LogInformation($"Successfully retrived item from cache: {cacheKey}");
            return (true, cachedObj);
        }
        catch (JsonException ex)
        {
            await _distributedCache.RemoveAsync(cacheKey);
            _logger.LogWarning(ex, $"Found corrupted cache of key: {cacheKey} - removing");
            return (false, default);
        }

    }

    public async Task CacheObject<T>(T obj, string cacheKey, DistributedCacheEntryOptions options)
    {
        byte[] objSerialized;
        try
        {
            objSerialized = JsonSerializer.SerializeToUtf8Bytes(obj);
            await _distributedCache.SetAsync(cacheKey, objSerialized, options);
            _logger.LogInformation($"Successfully saved key: {cacheKey} object to cache");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex, $"Caching for object of key: {cacheKey} failed.");
        }
    }

    public async Task InvalidateCache(string cacheKey)
    {
        await _distributedCache.RemoveAsync(cacheKey);
        _logger.LogInformation($"Invalidated obj in cache with key: {cacheKey}");
    }


    //Static Helper Methods
    public static string GenerateCacheKey(string prefix, Guid id) => $"{prefix}:{id}";

    public static string GenerateCacheKey(string prefix, IEnumerable<Guid> ids)
    {
        var sortedIds = ids.OrderBy(id => id);
        string id = string.Join("-", sortedIds);
        return $"{prefix}:{id}";
    }
}

