using Dapper;
using MySqlConnector;

namespace Kuaniminka.KobFlow.ToolBox
{
    public interface IDataGateway
    {
        T ExecuteReadOneQuery<T>(string query);
        KWriteResult Execute(string query);

        IQueryScannedValues ScanParameters<T>(T parameterObj);
        KWriteResult ExecuteInsert(string query);
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

    public class DataGateway : IDataGateway
    {
        private string connStr;

        public DataGateway(string connString)
        {
            this.connStr = connString;
        }
        public IQueryScannedValues ScanParameters<T>(T parameterObj)
        {
            IQueryScannedValues result =  findDynamicParams(parameterObj);
            return result;
        }

        private IQueryScannedValues findDynamicParams<T>(T q)
        {
            var map = KSqlMapper.Create;
            

            DynamicParameters reslt = new DynamicParameters();
            IQueryScannedValues result = new KDapperQueryValues(reslt, map);
            foreach (var p in q.GetType().GetProperties())
            {
                result.Set(p.Name, p.GetValue(q));
             /*   if (!map.Has(p.PropertyType)) continue;
                if (p.PropertyType == typeof(string))
                {
                    string value = p.GetValue(q).ToString();
                    value = value ?? "";
                    value = value.Replace("'", "\'\'");
                    reslt.Add(p.Name, value, map.Get(p.PropertyType));
                    continue;
                }
                reslt.Add(p.Name, p.GetValue(q), map.Get(p.PropertyType));*/
            }

            return result;
        }

        public KWriteResult ExecuteInsert(string query)
        {

                KWriteResult result = new KWriteResult();
                MySqlConnection conn = new MySqlConnection(this.connStr);
                conn.Open();


                MySqlCommand cmd = new MySqlCommand(query, conn);
                result.AffectedRowCount = cmd.ExecuteNonQuery();
                result.LastInsertedId = cmd.LastInsertedId;

                conn.Close();


                return result;
           
        }

        public T ExecuteReadOneQuery<T>(string query)
        {
            
                List<T> result = ExecuteReadManyResult<T>(query);
 
                if(result == null) result = new List<T>(); 
                return result.Count < 1 ? default(T) : result[0];        

        }

        public List<T> ExecuteReadManyResult<T>(string query)
        {
           
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    IEnumerable<T> yielded = conn.Query<T>(query);
                    if (yielded == null) return new List<T>();
                    List<T> result = yielded.AsList();
                    return result;
                }
           
        }

        public KWriteResult Execute(string query)
        {

            KWriteResult r = new KWriteResult();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
               
                r.LastInsertedId = conn.Execute(query);

            }
            return r;

        }

        

        public void ExecuteScalar(string query, Action<string> mapper)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {

               var outcome=  conn.ExecuteScalar(query);

                if (outcome == null)
                {
                    mapper("");
                    return;
                }

                mapper(outcome.ToString());
            }
        }
    }
}