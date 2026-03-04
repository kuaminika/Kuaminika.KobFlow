
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;

namespace Kuaminika.KobFlow.ExpenseCategoryService
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private IExpenseCategoryRepository repo;
        private IKLogTool logTool;
        private IKIdentityMap<ExpenseCategoryModel> iKIdentityMap;
        private ICacheHolder<ExpenseCategoryModel> cacheTool;

        public ExpenseCategoryService(ExpenseCategoryServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;
            this.cacheTool = args.CacheTool;
            this.iKIdentityMap= args.IdentityMap;

        }
        public ExpenseCategoryModel Add(ExpenseCategoryModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseCategoryModel result = repo.Add(addMe);
            iKIdentityMap.AddToMap(result.Id, result);
            cacheTool.Add($"{GetType().FullName}-{result.Id.ToString()}", result);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public ExpenseCategoryModel Delete(ExpenseCategoryModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseCategoryModel result = repo.Delete(victim);
            iKIdentityMap.RemoveFromMap(victim.Id);
            cacheTool.Remove($"{GetType().FullName}-{result.Id.ToString()}");
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }

        private void wipeOutCacheGetAll()
        {
            string key = $"{GetType().FullName}-GetAll";
            cacheTool.Remove(key);
        }

        public List<ExpenseCategoryModel> GetAll()
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
            List<ExpenseCategoryModel> result = repo.GetAll();
            logTool.LogTrace("ending", methodName);

            iKIdentityMap.PopulateMap(result);
            cacheTool.PopulateCache(cacheKey, result);


            return result;

        }

        public ExpenseCategoryModel Update(ExpenseCategoryModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseCategoryModel result = repo.Update(victim);
            iKIdentityMap.UpdateInMap(result.Id, result);
            cacheTool.Update($"{GetType().FullName}-{result.Id.ToString()}", result);
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }
    }
}
