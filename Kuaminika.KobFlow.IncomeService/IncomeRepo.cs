using Dapper;
using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeService
{
    public class IncomeRepo : IIncomeRepository
    {
        IKJSONParser kJSONParser;
        IDataGateway dataGateway;
        IKLogTool logTool;
        public IncomeRepo(IncomeRepoArgs args)
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
                                    ,e.source_id as `SourceId`
                                    ,m.name as `SourceName`
                                    ,e.date as `CreatedDate`
                                    ,k.name as `KobHolderName`
                                    ,e.account_id as `KobHolderId`
                                    ,e.category_id as `CategoryId`
                                    ,c.name as `CategoryName`
                                FROM Income e 
                        INNER JOIN IncomeEntity m on e.source_id = m.id
                        INNER JOIN kobHolder k on k.id = e.account_id 
                        INNER JOIN IncomeCategory c on c.id = e.category_id ";

        private IncomeModel validateParameters(IncomeModel testMe)
        {
            IncomeModel model = new IncomeModel();

            DynamicParameters parameters = findDynamicParams(testMe);

            model.Id = parameters.Get<int>("Id");
            model.Description = parameters.Get<string>("Description");
            model.OwnerId = parameters.Get<int>("OwnerId");
            model.SourceId = parameters.Get<int>("SourceId");
            model.SourceName = parameters.Get<string>("SourceName");
            model.Amount = parameters.Get<decimal>("Amount");
            model.CreatedDate = parameters.Get<DateTime>("CreatedDate");
            model.CategoryId = parameters.Get<int>("CategoryId");
            model.CategoryName = parameters.Get<string>("CategoryName");
            model.KobHolderId = parameters.Get<int>("KobHolderId");
            model.KobHolderName = parameters.Get<string>("KobHolderName");
            return model;
        }

        public IncomeModel Add(IncomeModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Add" : method.Name;
            logTool.LogTrace("starting", methodName);

            addMe = validateParameters(addMe);

            string query = $@"INSERT INTO `Income` ( `source_id`, `description`, `category_id`, `amount`, `date`, `account_id`, `user_id`) 
                               VALUES ({addMe.SourceId},'{addMe.Description}',{addMe.CategoryId},{addMe.Amount},'{addMe.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss")}',{addMe.KobHolderId},{addMe.OwnerId})";

            logTool.LogTrace(query, methodName);
            KWriteResult outcome = dataGateway.ExecuteInsert(query);
            IncomeModel recorded = this.FindById(outcome.LastInsertedId);

            logTool.LogTrace("ending", methodName);
            return recorded;


        }

        public IncomeModel FindById(long id)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.FindById" : method.Name;
            logTool.LogTrace("starting", methodName);
            string query = $" {this.selectAllQuery} where e.id = {id}";
            logTool.LogTrace(query, methodName);
            IncomeModel result = dataGateway.ExecuteReadOneQuery<IncomeModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public IncomeModel Delete(IncomeModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Delete" : method.Name;

            if (victim.Id < 0) throw new Exception($" victim {kJSONParser.Serialize(victim)} does not have a valid id");

            string query = $"DELETE from Income where id ={victim.Id}";
            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<IncomeModel>(query);
            logTool.LogTrace("ending", methodName);
            return victim;
        }

        public List<IncomeModel> GetAll()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.GetAlls" : method.Name;
            string query = $"{selectAllQuery}";
            logTool.LogTrace("starting", methodName);
            var result = dataGateway.ExecuteReadManyResult<IncomeModel>(query);
            logTool.LogTrace("ending", methodName);
            return result;
        }

        public IncomeModel Update(IncomeModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? $"{GetType().Name}.Update" : method.Name;
            logTool.LogTrace("starting", methodName);
            victim = validateParameters(victim);
            string query = $@"update Income set source_id = {victim.SourceId}, 
                                                 description = '{victim.Description}',
                                                 category_id = '{victim.CategoryId}',
                                                 amount= {victim.Amount},
                                                 date = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',
                                                 account_id = {victim.KobHolderId},
                                                 user_id = {victim.OwnerId} 
                                           where id = {victim.Id};";
            logTool.LogTrace(query, methodName);
            var outcome = dataGateway.Execute(query);

            IncomeModel recorded = this.FindById(victim.Id);
            logTool.LogTrace("ending", methodName);

            return recorded;


        }
    }


}