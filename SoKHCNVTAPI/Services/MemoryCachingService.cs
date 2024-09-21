using Microsoft.Extensions.Caching.Memory;

namespace SoKHCNVTAPI.Services;

public interface IMemoryCachingService
{
    T? Get<T>(string key);

    void Set<T>(string key, T createItem, double expired = 30);

    void Remove(string key);

    T? GetOrCreate<T>(string key, T? createItem, double expired = 30);
}

public class MemoryCachingService : IMemoryCachingService
{
    private readonly IMemoryCache _cache;

    public MemoryCachingService(IMemoryCache cache) { _cache = cache; }

    public T? GetOrCreate<T>(string key, T? createItem, double expired = 30)
    {
        if (!_cache.TryGetValue(key, out T? cacheEntry)) return cacheEntry;
        cacheEntry = createItem;
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(10))
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(expired))
            .SetPriority(CacheItemPriority.Normal)
            .SetSize(1024);
        _cache.Set(key, createItem, cacheEntryOptions);

        return cacheEntry;
    }


    public T? Get<T>(string key) { return _cache.Get<T>(key); }

    public void Set<T>(string key, T createItem, double expired = 30)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(10))
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(expired))
            .SetPriority(CacheItemPriority.Normal)
            .SetSize(1024);
        _cache.Set(key, createItem, cacheEntryOptions);
    }

    public void Remove(string key) { _cache.Remove(key); }
}