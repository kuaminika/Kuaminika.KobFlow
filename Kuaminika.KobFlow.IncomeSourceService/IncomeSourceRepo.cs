using Dapper;
using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeSourceService
{
    public class IncomeSourceRepo : IIncomeSourceRepository
    {
        IKJSONParser kJSONParser;
        IDataGateway dataGateway;
        IKLogTool logTool;
        public IncomeSourceRepo(IncomeSourceRepoArgs args)
        {
            dataGateway = args.DataGateway;
            kJSONParser = args.JSONParser;

            logTool = args.LogTool;
        }
        private DynamicParameters findDynamicParams<T>(T q)
        {
            var map = KSqlMapper.Create;
            DynamicParameters reslt = new DynamicParameters();
            foreach (var p in q.GetType().GetProperties())
            {
                if (!map.Has(p.PropertyType)) continue;
                reslt.Add(p.Name, p.GetValue(q), map.Get(p.PropertyType));
            }

            return reslt;
        }


        private IncomeSourceModel validateParameters(IncomeSourceModel testMe)
        {
            IncomeSourceModel model = new IncomeSourceModel();

            DynamicParameters parameters = findDynamicParams(testMe);

            model.Id = parameters.Get<int>("Id");
            model.Name = parameters.Get<string>("Name");
            model.OwnerId = parameters.Get<int>("OwnerId");
            return model;
        }

        public IncomeSourceModel Add(IncomeSourceModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Add" : method.Name;
            logTool.LogTrace("starting", methodName);

            addMe = validateParameters(addMe);

            string query = $"INSERT INTO IncomeEntity(name, user_id) VALUES ('{addMe.Name}',{addMe.OwnerId});";

            logTool.LogTrace(query, methodName);
            KWriteResult outcome = dataGateway.ExecuteInsert(query);
            IncomeSourceModel recorded = this.FindById(outcome.LastInsertedId);

            logTool.LogTrace("ending", methodName);
            return recorded;


        }

        public IncomeSourceModel FindById(long id)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.FindById" : method.Name;
            logTool.LogTrace("starting", methodName);
            string query = $"SELECT id as Id, name as Name , user_id as OwnerId FROM `IncomeEntity` where id = {id}";
            logTool.LogTrace(query, methodName);
            IncomeSourceModel result = dataGateway.ExecuteReadOneQuery<IncomeSourceModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public IncomeSourceModel Delete(IncomeSourceModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Delete" : method.Name;

            if (victim.Id < 0) throw new Exception($" victim {kJSONParser.Serialize(victim)} does not have a valid id");

            string query = $"DELETE from IncomeEntity where id ={victim.Id}";
            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<IncomeSourceModel>(query);
            logTool.LogTrace("ending", methodName);
            return victim;
        }

        public List<IncomeSourceModel> GetAll()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.GetAlls" : method.Name;
            string query = $"SELECT id as Id, name as Name , user_id as OwnerId FROM `IncomeEntity`";
            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<IncomeSourceModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public IncomeSourceModel Update(IncomeSourceModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Update" : method.Name;
            logTool.LogTrace("starting", methodName);
            victim = validateParameters(victim);
            string query = $"update IncomeEntity set name = '{victim.Name}', user_id = {victim.OwnerId} where id = {victim.Id};";
            logTool.LogTrace(query, methodName);
            var outcome = dataGateway.Execute(query);

            IncomeSourceModel recorded = this.FindById(victim.Id);
            logTool.LogTrace("ending", methodName);

            return recorded;


        }
    }


}