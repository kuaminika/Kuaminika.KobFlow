using Microsoft.Extensions.Caching.Memory;

namespace Kuaniminka.KobFlow.ToolBox
{
    public static class CacheRoot
    {
        public static readonly IMemoryCache MemoryCache =
            new MemoryCache(new MemoryCacheOptions());
    }

    public static class KConstants
    {
        public static int CacheExpirationInSeconds = 3600;
    }

}
