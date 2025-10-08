using Dapper;

namespace Kuaniminka.KobFlow.ToolBox
{
    public interface IDataGateway
    {
        T ExecuteReadOneQuery<T>(string query);
        KWriteResult Execute(string query);

        IQueryScannedValues ScanParameters<T>(T parameterObj);
        KWriteResult ExecuteInsert(string query);
        KWriteResult ExecuteInsert(string query,Dictionary<string,object> parameters);
        List<T> ExecuteReadManyResult<T>(string query);

        void ExecuteScalar(string query, Action<string> mapper);

    }

    public interface IQueryScannedValues
    {
        T Get<T>(string queryKey, T _default = default(T));
        void Set<T>(string queryKey, T value);
      
    }

    public class KDapperQueryValues:IQueryScannedValues
    {
        private KSqlMapper map;
        DynamicParameters dynamicParameters;

        public KDapperQueryValues(DynamicParameters dynamicParameters, KSqlMapper kSqlMapper)
        {
            this.map = kSqlMapper;
            this.dynamicParameters = dynamicParameters;
        }

        public T Get<T>(string queryKey, T _default = default(T))
        {
            if (!dynamicParameters.ParameterNames.Contains(queryKey)) return _default;
         T result = dynamicParameters.Get<T>(queryKey);
            return result;
        }

        public void Set<T>(string queryKey, T value)
        {
            if ( value == null) return;
            if (!map.Has(value.GetType())) return; //TODO : need to notify that it cant add these values 

            if(value.GetType() == typeof(string))
            {
                string? val = value==null?"":value.ToString();
                val = val ?? "";
                val = val.Replace("'", "\'\'");
                dynamicParameters.Add(queryKey, val, map.Get(val.GetType()));
                return;
            }


            dynamicParameters.Add(queryKey, value, map.Get(value.GetType()));
        }
    }
}