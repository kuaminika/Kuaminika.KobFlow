using Dapper;
using MySqlConnector;

namespace Kuaniminka.KobFlow.ToolBox
{
    public class DataGateway : IDataGateway
    {
        private IKLogTool logTool;
        private string connStr; 
        public DataGateway(string connString,IKLogTool logTool)
        {
            this.logTool = logTool;
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


        public KWriteResult ExecuteInsert(string query, Dictionary<string, object> parameters)
        {
            logTool.LogTrace($"Executing insert: {query} with parameters: {string.Join(", ", parameters.Select(kv => kv.Key + "=" + kv.Value))}", "DataGateway.ExecuteInsert");
            var result = new KWriteResult();

            try
            {
                using (var conn = new MySqlConnection(this.connStr))
                {
                    logTool.LogTrace("Opening connection.", "DataGateway.ExecuteInsert");
                    logTool.LogTrace("connectionTimeout:"+ conn.ConnectionTimeout, "DataGateway.ExecuteInsert");
                    conn.Open();
                    logTool.LogTrace("Connection opened successfully.", "DataGateway.ExecuteInsert");
                    
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        logTool.LogTrace("Preparing command with parameters.", "DataGateway.ExecuteInsert");
                        foreach (var p in parameters)
                            cmd.Parameters.AddWithValue(p.Key, p.Value);

                        result.AffectedRowCount = cmd.ExecuteNonQuery();
                        result.LastInsertedId = cmd.LastInsertedId;
                    }

                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                logTool.logObject(ex, "DataGateway.ExecuteInsert MySqlException");
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