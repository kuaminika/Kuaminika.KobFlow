namespace Kuaniminka.KobFlow.ToolBox
{
    public class KIdentityMap<T> : IKIdentityMap<T>
    {
        private readonly Dictionary<int, T> _map = new Dictionary<int, T>();
        public void AddToMap(int id, T result)
        {
            _map[id] = result;
        }
        public T ? Get(int id)
        {
            _map.TryGetValue(id, out T? result);
            return result;
        }
        public List<T> GetAllFromMap()
        {
            return _map.Values.ToList();
        }
        public bool IsPopulated()
        {
            return _map.Count > 0;
        }
        public void PopulateMap(List<T> result)
        {
            foreach (var item in result)
            {
                var idProperty = item.GetType().GetProperty("Id");
                if (idProperty != null)
                {
                    int id = (int)idProperty.GetValue(item);
                    _map[id] = item;
                }
            }
        }
        public void RemoveFromMap(int id)
        {
            _map.Remove(id);
        }
        public void UpdateInMap(int id, T result)
        {
            _map[id] = result;
        }
      
    }

}
