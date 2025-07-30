using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Common;

public static class CacheExtensions
{
    public static async Task<T?> GetOrCreateAsync<T>(
        this IDistributedCache cache,
        string key,
        Func<DistributedCacheEntryOptions, Task<T?>> factory) where T : class
    {
        var cachedData = await cache.GetStringAsync(key);
        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        var cacheEntryOptions = new DistributedCacheEntryOptions();
        var data = await factory(cacheEntryOptions);
        if (data is null) return null;

        var serializedData = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(key, serializedData, cacheEntryOptions);
        return data;
    }
}
