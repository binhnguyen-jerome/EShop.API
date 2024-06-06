using EShop.CustomerFe.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.CustomerFe.Services.Implements
{
    public class CacheClientService : ICacheClientService
    {
        private readonly IMemoryCache _cache;
        public CacheClientService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public T Get<T>(string key)
        {
            _cache.TryGetValue(key, out T value);
            return value;
        }
        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(expiration);

            _cache.Set(key, value, cacheEntryOptions);
        }
        public void Remove(string key)
        {
            _cache.Remove(key);
        }

    }
}
