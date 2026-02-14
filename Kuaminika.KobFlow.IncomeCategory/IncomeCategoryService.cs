
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;

namespace Kuaminika.KobFlow.IncomeCategory
{
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private IIncomeCategoryRepository repo;
        private IKLogTool logTool;
        private IKIdentityMap<IncomeCategoryModel> iKIdentityMap;
        private ICacheHolder<IncomeCategoryModel> cacheTool;

        public IncomeCategoryService(IncomeCategoryServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;
            this.iKIdentityMap = args.IdentityMap;
            this.cacheTool = args.CacheTool;

        }
        public IncomeCategoryModel Add(IncomeCategoryModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeCategoryModel result = repo.Add(addMe);
            iKIdentityMap.AddToMap(result.Id, result);
            cacheTool.Add($"{GetType().FullName}-{result.Id.ToString()}", result);
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public IncomeCategoryModel Delete(IncomeCategoryModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeCategoryModel result = repo.Delete(victim);
            iKIdentityMap.RemoveFromMap(victim.Id);
            cacheTool.Remove($"{GetType().FullName}-{result.Id.ToString()}");
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public List<IncomeCategoryModel> GetAll()
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
            List<IncomeCategoryModel> result = repo.GetAll();
            logTool.LogTrace("ending", methodName);

            return result;

        }

        public IncomeCategoryModel Update(IncomeCategoryModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeCategoryModel result = repo.Update(victim);
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
