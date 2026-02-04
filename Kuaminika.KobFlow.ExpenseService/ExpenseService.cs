
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kuaminika.KobFlow.ExpenseService
{
    public class ExpenseService : IExpenseService
    {
        private IExpenseRepository repo;
        private IKLogTool logTool;
        private IKIdentityMap<ExpenseModel> iKIdentityMap;
        private ICacheHolder<ExpenseModel> cacheTool;
        public ExpenseService(ExpenseServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;
            this.iKIdentityMap = args.IdentityMap;
            this.cacheTool = args.CacheTool;

        }
        public ExpenseModel Add(ExpenseModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseModel result = repo.Add(addMe);
            iKIdentityMap.AddToMap(result.Id, result);
            cacheTool.Add($"{GetType().FullName}-{result.Id.ToString()}", result);
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public ExpenseModel Delete(ExpenseModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseModel result = repo.Delete(victim);
            iKIdentityMap.RemoveFromMap(victim.Id);
            cacheTool.Remove($"{GetType().FullName}-{result.Id.ToString()}");
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public List<ExpenseModel> GetAll()
        {

            string cacheKey = $"{GetType().FullName}-GetAll";

            if (cacheTool.HasList(cacheKey))
            {
                var fromCache = cacheTool.GetListFromCache(cacheKey);
                iKIdentityMap.PopulateMap(fromCache);
                return fromCache;
            }


            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;

            logTool.LogTrace("starting", methodName);
            List<ExpenseModel> result = repo.GetAll();
            logTool.LogTrace("ending", methodName);
            iKIdentityMap.PopulateMap(result);
            cacheTool.PopulateCache($"{GetType().FullName}-{methodName}", result);


            return result;

        }

        public ExpenseModel Update(ExpenseModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseModel result = repo.Update(victim);
            iKIdentityMap.UpdateInMap(result.Id, result);
            cacheTool.Update($"{GetType().FullName}-{result.Id.ToString()}", result);
            wipeOutCacheGetAll();

            logTool.LogTrace("ending", methodName);

            return result;
        }


        private void wipeOutCacheGetAll()
        {
            string key = $"{GetType().FullName}-GetAll";
            cacheTool.Remove(key);
        }
    }
}
