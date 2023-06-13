using Dapper;
using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.ExpenseService
{
    public class ExpenseRepo : IExpenseRepository
    {
        IKJSONParser kJSONParser;
        IDataGateway dataGateway;
        IKLogTool logTool;
        public ExpenseRepo(ExpenseRepoArgs args)
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

        string selectAllQuery = @"SELECT 
                                     e.id as `Id`
                                    ,e.description as `Description`
                                    ,e.user_id as `OwnerId`
                                    ,e.amount as `Amount`
                                    ,e.store_id as `MerchantId`
                                    ,m.name as `MerchantName`
                                    ,e.date as `CreatedDate`
                                    ,k.name as `KobHolderName`
                                    ,e.account_id as `KobHolderId`
                                    ,e.category_id as `CategoryId`
                                    ,c.name as `CategoryName`
                                FROM Expense e 
                        INNER JOIN Entity m on e.store_id = m.id
                        INNER JOIN kobHolder k on k.id = e.account_id 
                        INNER JOIN Category c on c.id = e.category_id ";

        private ExpenseModel validateParameters(ExpenseModel testMe)
        {
            ExpenseModel model = new ExpenseModel();

            DynamicParameters parameters = findDynamicParams(testMe);

            model.Id = parameters.Get<int>("Id");
            model.Description = parameters.Get<string>("Description");
            model.OwnerId = parameters.Get<int>("OwnerId");
            model.MerchantId = parameters.Get<int>("MerchantId");
            model.MerchantName = parameters.Get<string>("MerchantName");
            model.Amount = parameters.Get<int>("Amount");
            model.CreatedDate = parameters.Get<DateTime>("CreatedDate");
            model.CategoryId = parameters.Get<int>("CategoryId");
            model.CategoryName = parameters.Get<string>("CategoryName");
            model.KobHolderId = parameters.Get<int>("KobHolderId");
            model.KobHolderName = parameters.Get<string>("KobHolderName");
            return model;
        }

        public ExpenseModel Add(ExpenseModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Add" : method.Name;
            logTool.LogTrace("starting", methodName);

            addMe = validateParameters(addMe);

            string query = $@"INSERT INTO `Expense` ( `store_id`, `description`, `category_id`, `amount`, `date`, `account_id`, `user_id`) 
                               VALUES ({addMe.MerchantId},'{addMe.Description}',{addMe.CategoryId},{addMe.Amount},'{addMe.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss")}',{addMe.KobHolderId},{addMe.OwnerId})";

            logTool.LogTrace(query, methodName);
            KWriteResult outcome = dataGateway.ExecuteInsert(query);
            ExpenseModel recorded = this.FindById(outcome.LastInsertedId);

            logTool.LogTrace("ending", methodName);
            return recorded;


        }

        public ExpenseModel FindById(long id)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.FindById" : method.Name;
            logTool.LogTrace("starting", methodName);
            string query = $"SELECT {this.selectAllQuery} where id = {id}";
            logTool.LogTrace(query, methodName);
            ExpenseModel result = dataGateway.ExecuteReadOneQuery<ExpenseModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public ExpenseModel Delete(ExpenseModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Delete" : method.Name;

            if (victim.Id < 0) throw new Exception($" victim {kJSONParser.Serialize(victim)} does not have a valid id");

            string query = $"DELETE from Expense where id ={victim.Id}";
            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<ExpenseModel>(query);
            logTool.LogTrace("ending", methodName);
            return victim;
        }

        public List<ExpenseModel> GetAll()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.GetAlls" : method.Name;
            string query = $"{selectAllQuery}";
            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<ExpenseModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public ExpenseModel Update(ExpenseModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Update" : method.Name;
            logTool.LogTrace("starting", methodName);
            victim = validateParameters(victim);
            string query = $@"update Expense set store_id = {victim.MerchantId}, 
                                                 description = '{victim.Description}',
                                                 category_id = '{victim.CategoryId}',
                                                 amount= {victim.Amount},
                                                 date = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',
                                                 account_id = {victim.KobHolderId},
                                                 user_id = {victim.OwnerId} 
                                           where id = {victim.Id};";
            logTool.LogTrace(query, methodName);
            var outcome = dataGateway.Execute(query);

            ExpenseModel recorded = this.FindById(victim.Id);
            logTool.LogTrace("ending", methodName);

            return recorded;


        }
    }


}