using Microsoft.Extensions.Caching.Distributed;

namespace EquipmentService.Core.Caching;

public static class CachingProfiles
{
    public static DistributedCacheEntryOptions ShortTTLCacheOption => new DistributedCacheEntryOptions()
        .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
        .SetSlidingExpiration(TimeSpan.FromSeconds(30));

    public static DistributedCacheEntryOptions MediumTTLCacheOption => new DistributedCacheEntryOptions()
        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
        .SetSlidingExpiration(TimeSpan.FromMinutes(3));

    public static DistributedCacheEntryOptions LongTTLCacheOption => new DistributedCacheEntryOptions()
        .SetAbsoluteExpiration(TimeSpan.FromHours(2))
        .SetSlidingExpiration(TimeSpan.FromMinutes(45));

}

