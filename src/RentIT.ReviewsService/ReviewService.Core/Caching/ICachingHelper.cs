using Microsoft.Extensions.Caching.Distributed;

namespace ReviewServices.Core.Caching;

public interface ICachingHelper
{
    Task<(bool Found, T? Value)> GetCachedObject<T>(string cacheKey, CancellationToken cancellationToken = default);
    Task CacheObject<T>(T obj, string cacheKey, DistributedCacheEntryOptions options, CancellationToken cancellationToken = default);
    Task InvalidateCache(string cacheKey, CancellationToken cancellationToken = default);
}
