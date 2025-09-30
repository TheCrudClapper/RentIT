using Microsoft.Extensions.Caching.Distributed;

namespace RentalService.Core.Caching;

public interface ICachingHelper
{
    Task<(bool Found, T? Value)> GetCachedObject<T>(string cacheKey);
    Task CacheObject<T>(T obj, string cacheKey, DistributedCacheEntryOptions options);
    Task InvalidateCache(string cacheKey);
}
