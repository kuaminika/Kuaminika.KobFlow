
using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.MerchantService
{
    public class MerchantRepo : IMerchantRepository
    {
        IKJSONParser kJSONParser;
        IDataGateway dataGateway;
        IKLogTool logTool;
        public MerchantRepo(MerchantRepoArgs args)
        {
            dataGateway = args.DataGateway;
            kJSONParser = args.JSONParser;

            logTool = args.LogTool;
        }
    

        private MerchantModel validateParameters(MerchantModel testMe)
        {
            MerchantModel model = new MerchantModel();
            var parameters = dataGateway.ScanParameters(testMe);

            model.Id = parameters.Get<int>("Id");
            model.Name = parameters.Get<string>("Name");
            model.OwnerId = parameters.Get<int>("OwnerId");
            return model;
        }

        public MerchantModel AddMerchant(MerchantModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.AddMerchant" : method.Name;
            logTool.LogTrace("starting", methodName);

            addMe = validateParameters(addMe);

            string query = $"INSERT INTO Entity(name, user_id) VALUES ('{addMe.Name}',{addMe.OwnerId});";

            logTool.LogTrace(query, methodName);
            KWriteResult outcome = dataGateway.ExecuteInsert(query);
            MerchantModel recorded = this.FindById( outcome.LastInsertedId);

            logTool.LogTrace("ending", methodName);
            return recorded;


        }

        public  MerchantModel FindById(long id)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.FindById" : method.Name;
            logTool.LogTrace("starting", methodName);
            string query = $"SELECT id as Id, name as Name , user_id as OwnerId FROM `Entity` where id = {id}";
            logTool.LogTrace(query, methodName);
            MerchantModel result =  dataGateway.ExecuteReadOneQuery<MerchantModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public MerchantModel DeleteMerchant(MerchantModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.DeleteMerchant" : method.Name;

            if (victim.Id < 0) throw new Exception($" victim {kJSONParser.Serialize(victim)} does not have a valid id");

            string query = $"DELETE from Entity where id ={victim.Id}";
            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<MerchantModel>(query);
            logTool.LogTrace("ending", methodName);
            return victim;
        }

        public List<MerchantModel> GetAllMerchants()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.GetAllMerchants" : method.Name;
            string query = $"SELECT id as Id, name as Name , user_id as OwnerId FROM `Entity`";
            logTool.LogTrace("starting", methodName);
            var result =  dataGateway.ExecuteReadManyResult<MerchantModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public MerchantModel UpdateMerchant(MerchantModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.UpdateMerchant" : method.Name;
            logTool.LogTrace("starting", methodName);
            victim = validateParameters(victim);
            string query = $"update Entity set name = '{victim.Name}', user_id = {victim.OwnerId} where id = {victim.Id};";
            logTool.LogTrace(query, methodName);
            var outcome =  dataGateway.Execute(query);

            MerchantModel recorded = this.FindById(victim.Id);
            logTool.LogTrace("ending", methodName);

            return recorded;


        }

        public List<MerchantModel_Assigned> GetAllAssignedRecords()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.GetAllAssignedRecords" : method.Name;
            string query = $@"SELECT s.id as Id,
                                s.name as Name , 
                                s.user_id as OwnerId , 
                                count(e.id) as ExpenseCount 
                            FROM `Entity` s 
                            inner join `Expense` e on s.id = e.store_id
                            group by s.id, s.name,s.user_id";


            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<MerchantModel_Assigned>(query);
            logTool.LogTrace("ending", methodName);
            return result;

        }
    }


}