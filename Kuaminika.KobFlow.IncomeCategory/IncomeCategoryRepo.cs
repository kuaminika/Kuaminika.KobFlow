using Dapper;
using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeCategory
{
    public class IncomeCategoryRepo : IIncomeCategoryRepository
    {
        IKJSONParser kJSONParser;
        IDataGateway dataGateway;
        IKLogTool logTool;
        public IncomeCategoryRepo(IncomeCategoryRepoArgs args)
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


        private IncomeCategoryModel validateParameters(IncomeCategoryModel testMe)
        {
            IncomeCategoryModel model = new IncomeCategoryModel();

            var parameters = dataGateway.ScanParameters(testMe); // findDynamicParams(testMe);

            model.Id = parameters.Get<int>("Id");
            model.Name = parameters.Get<string>("Name");
            model.OwnerId = parameters.Get<int>("OwnerId");
            return model;
        }

        public IncomeCategoryModel Add(IncomeCategoryModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Add" : method.Name;
            logTool.LogTrace("starting", methodName);

            addMe = validateParameters(addMe);

            string query = $"INSERT INTO IncomeCategory(name, user_id) VALUES ('{addMe.Name}',{addMe.OwnerId});";

            logTool.LogTrace(query, methodName);
            KWriteResult outcome = dataGateway.ExecuteInsert(query);
            IncomeCategoryModel recorded = this.FindById(outcome.LastInsertedId);

            logTool.LogTrace("ending", methodName);
            return recorded;


        }

        public IncomeCategoryModel FindById(long id)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.FindById" : method.Name;
            logTool.LogTrace("starting", methodName);
            string query = $"SELECT id as Id, name as Name , user_id as OwnerId FROM `IncomeCategory` where id = {id}";
            logTool.LogTrace(query, methodName);
            IncomeCategoryModel result = dataGateway.ExecuteReadOneQuery<IncomeCategoryModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public IncomeCategoryModel Delete(IncomeCategoryModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Delete" : method.Name;

            if (victim.Id < 0) throw new Exception($" victim {kJSONParser.Serialize(victim)} does not have a valid id");

            string query = $"DELETE from IncomeCategory where id ={victim.Id}";
            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<IncomeCategoryModel>(query);
            logTool.LogTrace("ending", methodName);
            return victim;
        }

        public List<IncomeCategoryModel> GetAll()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.GetAlls" : method.Name;
            string query = $"SELECT id as Id, name as Name , user_id as OwnerId FROM `IncomeCategory`";
            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<IncomeCategoryModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public IncomeCategoryModel Update(IncomeCategoryModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Update" : method.Name;
            logTool.LogTrace("starting", methodName);
            victim = validateParameters(victim);
            string query = $"update IncomeCategory set name = '{victim.Name}', user_id = {victim.OwnerId} where id = {victim.Id};";
            logTool.LogTrace(query, methodName);
            var outcome = dataGateway.Execute(query);

            IncomeCategoryModel recorded = this.FindById(victim.Id);
            logTool.LogTrace("ending", methodName);

            return recorded;


        }
    }


}