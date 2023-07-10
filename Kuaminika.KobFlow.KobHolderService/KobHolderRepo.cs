using Dapper;
using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.KobHolderService
{
    public class KobHolderRepo : IKobHolderRepository
    {
        IKJSONParser kJSONParser;
        IDataGateway dataGateway;
        IKLogTool logTool;
        public KobHolderRepo(KobHolderRepoArgs args)
        {
            dataGateway = args.DataGateway;
            kJSONParser = args.JSONParser;

            logTool = args.LogTool;
        }
    


        private KobHolderModel validateParameters(KobHolderModel testMe)
        {
            KobHolderModel model = new KobHolderModel();

            var parameters = dataGateway.ScanParameters(testMe);

            model.Id = parameters.Get<int>("Id");
            model.Name = parameters.Get<string>("Name");
            model.OwnerId = parameters.Get<int>("OwnerId");
            return model;
        }

        public KobHolderModel Add(KobHolderModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Add" : method.Name;
            logTool.LogTrace("starting", methodName);

            addMe = validateParameters(addMe);

            string query = $"INSERT INTO kobHolder(name, user_id) VALUES ('{addMe.Name}',{addMe.OwnerId});";

            logTool.LogTrace(query, methodName);
            KWriteResult outcome = dataGateway.ExecuteInsert(query);
            KobHolderModel recorded = this.FindById(outcome.LastInsertedId);

            logTool.LogTrace("ending", methodName);
            return recorded;


        }

        public KobHolderModel FindById(long id)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.FindById" : method.Name;
            logTool.LogTrace("starting", methodName);
            string query = $"SELECT id as Id, name as Name , user_id as OwnerId FROM `kobHolder` where id = {id}";
            logTool.LogTrace(query, methodName);
            KobHolderModel result = dataGateway.ExecuteReadOneQuery<KobHolderModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public KobHolderModel Delete(KobHolderModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Delete" : method.Name;

            if (victim.Id < 0) throw new Exception($" victim {kJSONParser.Serialize(victim)} does not have a valid id");

            string query = $"DELETE from kobHolder where id ={victim.Id}";
            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<KobHolderModel>(query);
            logTool.LogTrace("ending", methodName);
            return victim;
        }

        public List<KobHolderModel> GetAll()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.GetAlls" : method.Name;
            string query = $"SELECT id as Id, name as Name , user_id as OwnerId FROM `kobHolder`";
            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<KobHolderModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public KobHolderModel Update(KobHolderModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Update" : method.Name;
            logTool.LogTrace("starting", methodName);
            victim = validateParameters(victim);
            string query = $"update kobHolder set name = '{victim.Name}', user_id = {victim.OwnerId} where id = {victim.Id};";
            logTool.LogTrace(query, methodName);
            var outcome = dataGateway.Execute(query);

            KobHolderModel recorded = this.FindById(victim.Id);
            logTool.LogTrace("ending", methodName);

            return recorded;


        }

        public KobHolderModel_Count UsedHolderLikeThis(KobHolderModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.GetAlls" : method.Name;
            string query = $@"SELECT k.id as Id, 
                                    k.name as Name ,
                                    k.user_id as OwnerId, 
                                    COUNT(e.id) as ExpenseCount,
                                    COUNT(i.id) as IncomeCount
                                 FROM `kobHolder` k
                            left JOIN `Expense` e on e.account_id = k.id
                            left JOIN `Income` i on i.account_id = k.id
                                 where k.id = {victim.Id } and k.name ='{victim.Name}' and k.user_id = {victim.OwnerId}
                                 GROUP by k.id, k.name, k.user_id; ";
            logTool.LogTrace("starting", methodName);
            logTool.LogTrace(query);
            var result = dataGateway.ExecuteReadManyResult<KobHolderModel_Count>(query);
            if (result == null) return null;
            logTool.LogTrace("ending", methodName);
            return result[0];
        }
    }


}