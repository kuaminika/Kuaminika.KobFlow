
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;

namespace Kuaminika.KobFlow.IncomeSourceService
{
    public class IncomeSourceService : IIncomeSourceService
    {
        private IIncomeSourceRepository repo;
        private IKLogTool logTool;
        private IKIdentityMap<IncomeSourceModel> iKIdentityMap;
        private ICacheHolder<IncomeSourceModel> cacheTool;

        public IncomeSourceService(IncomeSourceServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;
            this.iKIdentityMap = args.IdentityMap;
            this.cacheTool = args.CacheTool;

        }
        public IncomeSourceModel Add(IncomeSourceModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeSourceModel result = repo.Add(addMe);
            iKIdentityMap.AddToMap(result.Id, result);
            cacheTool.Add($"{GetType().FullName}-{result.Id.ToString()}", result);
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public IncomeSourceModel Delete(IncomeSourceModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeSourceModel result = repo.Delete(victim);
            iKIdentityMap.RemoveFromMap(victim.Id);
            cacheTool.Remove($"{GetType().FullName}-{result.Id.ToString()}");
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public List<IncomeSourceModel> GetAll()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);

            string cacheKey = $"{GetType().FullName}-GetAll";

            if (cacheTool.HasList(cacheKey))
            {
                var fromCache = cacheTool.GetListFromCache(cacheKey);
                iKIdentityMap.PopulateMap(fromCache);
                return fromCache;
            }
            logTool.LogTrace($"cache miss for {cacheKey}", cacheKey);
            List<IncomeSourceModel> result = repo.GetAll();


            cacheTool.PopulateCache(cacheKey, result);
            iKIdentityMap.PopulateMap(result);
            logTool.LogTrace("ending", methodName);

            return result;

        }

        public IncomeSourceModel Update(IncomeSourceModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeSourceModel result = repo.Update(victim);
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
