using KironBackendProject.Services.Shared.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace KironBackendProject.Services.Shared
{

    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            return _memoryCache.TryGetValue(key, out T value) ? value : default;
        }

        public void Set<T>(string key, T value, int durationInMinutes)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(durationInMinutes));
        }

        public bool Exists(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }

}
