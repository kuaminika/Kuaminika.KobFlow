using Microsoft.Extensions.Caching.Memory;

namespace Kuaniminka.KobFlow.ToolBox
{

    public class CacheHolder<T> : ICacheHolder<T>
    {
        private readonly IMemoryCache _cache; 
        private readonly TimeSpan _defaultCacheDuration;

        public CacheHolder(IMemoryCache cache , int TTL = 30)
        {
            _cache = cache; 
            _defaultCacheDuration = TimeSpan.FromSeconds(TTL);
        }

        public void Add(string key, T result)
        {
           _cache.Set(key, result, _defaultCacheDuration);
        }
        
        public List<T> GetListFromCache(string cacheKey)
        {
          var result =  _cache.Get<List<T>>(cacheKey);
            return result;
        }

        public bool HasList(string cacheKey)
        {

            return _cache.TryGetValue(cacheKey, out List<T> _);
        }

        public void PopulateCache(string methodName, List<T> result)
        {
           _cache.Set(methodName, result, _defaultCacheDuration);
        }

        public void Remove(string v)
        {
            _cache.Remove(v);
        }

        public void Update(string key, T result)
        {
            _cache.Set(key, result, _defaultCacheDuration);
        }
    }

}
