namespace Kuaniminka.KobFlow.ToolBox
{
    public interface ICacheHolder<T>
    {
        void Add(string key, T result);
        List<T> GetListFromCache(string cacheKey);
        bool HasList(string cacheKey);
        void PopulateCache(string methodName, List<T> result);
        void Remove(string v);
        void Update(string v, T result);
    }
}