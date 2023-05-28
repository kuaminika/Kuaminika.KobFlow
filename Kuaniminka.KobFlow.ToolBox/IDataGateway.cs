﻿using Dapper;
using MySqlConnector;

namespace Kuaniminka.KobFlow.ToolBox
{
    public interface IDataGateway
    {
        T ExecuteReadOneQuery<T>(string query);
        KWriteResult Execute(string query);
        KWriteResult ExecuteInsert(string query);
        List<T> ExecuteReadManyResult<T>(string query);

        void ExecuteScalar(string query, Action<string> mapper);

    }

    public class DataGateway : IDataGateway
    {
        private string connStr;
     //   private KMysql_KDBAbstraction dbAbstraction;

        public DataGateway(string connString)
        {
            this.connStr = connString;
       //    this.dbAbstraction = new KMysql_KDBAbstraction(connString);
        }


        public KWriteResult ExecuteInsert(string query)
        {
            
                KWriteResult r = new KWriteResult();
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    r.LastInsertedId =  conn.Execute(query);
                   
                }
                return r;           
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