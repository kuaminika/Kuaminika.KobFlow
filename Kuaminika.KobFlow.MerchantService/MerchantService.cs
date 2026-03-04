
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;

namespace Kuaminika.KobFlow.MerchantService
{
    public class MerchantService : IMerchhantService
    {
        private IMerchantRepository repo;
        private IKLogTool logTool;
        private ICacheHolder<MerchantModel> cacheTool;
        private KIdentityMap<MerchantModel> iKIdentityMap;

        public MerchantService(MerchantServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;
                this.cacheTool = args.CacheTool;    
            this.iKIdentityMap = args.IdentityMap;

        }
        private void wipeOutCacheGetAll()
        {
            string key = $"{GetType().FullName}-GetAll";
            cacheTool.Remove(key);
        }
        public MerchantModel AddMerchant(MerchantModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            MerchantModel result = repo.AddMerchant(addMe);
            iKIdentityMap.AddToMap(result.Id, result);
            cacheTool.Add($"{GetType().FullName}-{result.Id.ToString()}", result);
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public MerchantModel DeleteMerchant(MerchantModel victim)
        {
           var baitList =  GetAllAssignedRecords().Where(m => m.Id == victim.Id); 
            if ( baitList!= null && baitList.Count()>0)
            {
                //TODO this error message should not be hardcoded
                var bait = baitList.First();
                throw new Exception($"{bait.Name} cannot be deleted. It is assigned to {bait.ExpenseCount} expenses");
            }

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            MerchantModel result = repo.DeleteMerchant(victim);
            iKIdentityMap.RemoveFromMap(victim.Id);
            cacheTool.Remove($"{GetType().FullName}-{result.Id.ToString()}");
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public List<MerchantModel_Assigned> GetAllAssignedRecords()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            List<MerchantModel_Assigned> result = repo.GetAllAssignedRecords();
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public List<MerchantModel> GetAllMerchants()
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
            List<MerchantModel> result = repo.GetAllMerchants();
            logTool.LogTrace("ending", methodName);

            return result;

        }

        public MerchantModel UpdateMerchant(MerchantModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            MerchantModel result = repo.UpdateMerchant(victim);
            iKIdentityMap.UpdateInMap(result.Id, result);
            cacheTool.Update($"{GetType().FullName}-{result.Id.ToString()}", result);
            wipeOutCacheGetAll();
            logTool.LogTrace("ending", methodName);

            return result;
        }
    }
}
