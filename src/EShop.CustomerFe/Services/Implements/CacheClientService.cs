using EShop.CustomerFe.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.CustomerFe.Services.Implements
{
    public class CacheClientService(IMemoryCache cache) : ICacheClientService
    {
        public T? Get<T>(string key)
        {
            cache.TryGetValue(key, out T? value);
            return value;
        }
        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(expiration);

            cache.Set(key, value, cacheEntryOptions);
        }
        public void Remove(string key)
        {
            cache.Remove(key);
        }

    }
}
