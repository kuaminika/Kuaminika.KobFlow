
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;

namespace Kuaminika.KobFlow.IncomeService
{
    public class IncomeService : IIncomeService
    {
        private IIncomeRepository repo;
        private IKLogTool logTool;
        private IKIdentityMap<IncomeModel> iKIdentityMap;
        private ICacheHolder<IncomeModel> cacheTool;

        public IncomeService(IncomeServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;
            this.iKIdentityMap = args.IdentityMap;
            this.cacheTool = args.CacheTool;

        }
        public IncomeModel Add(IncomeModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);

            IncomeModel result = repo.Add(addMe);
            iKIdentityMap.AddToMap(result.Id, result);
            cacheTool.Add($"{GetType().FullName}-{result.Id.ToString()}", result);
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public IncomeModel Delete(IncomeModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeModel result = repo.Delete(victim);
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

        public List<IncomeModel> GetAll()
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
            List<IncomeModel> result = repo.GetAll();
            logTool.LogTrace("ending", methodName);

            return result;

        }

        public IncomeModel Update(IncomeModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeModel result = repo.Update(victim);
            iKIdentityMap.UpdateInMap(result.Id, result);
            cacheTool.Update($"{GetType().FullName}-{result.Id.ToString()}", result);
            wipeOutCacheGetAll();

            logTool.LogTrace("ending", methodName);

            return result;
        }
    }
}
