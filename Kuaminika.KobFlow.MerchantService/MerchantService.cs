
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;

namespace Kuaminika.KobFlow.MerchantService
{
    public class MerchantService : IMerchhantService
    {
        private IMerchantRepository repo;
        private IKLogTool logTool;

        public MerchantService(MerchantServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;
           
        }
        public MerchantModel AddMerchant(MerchantModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            MerchantModel result = repo.AddMerchant(addMe);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public MerchantModel DeleteMerchant(MerchantModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            MerchantModel result = repo.DeleteMerchant(victim);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public List<MerchantModel> GetAllMerchants()
        {
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
            logTool.LogTrace("ending", methodName);

            return result;
        }
    }
}
