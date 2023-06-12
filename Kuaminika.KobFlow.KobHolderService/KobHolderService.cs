
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;

namespace Kuaminika.KobFlow.KobHolderService
{
    public class KobHolderService : IKobHolderService
    {
        private IKobHolderRepository repo;
        private IKLogTool logTool;

        public KobHolderService(KobHolderServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;

        }
        public KobHolderModel Add(KobHolderModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            KobHolderModel result = repo.Add(addMe);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public KobHolderModel Delete(KobHolderModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            KobHolderModel result = repo.Delete(victim);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public List<KobHolderModel> GetAll()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            List<KobHolderModel> result = repo.GetAll();
            logTool.LogTrace("ending", methodName);

            return result;

        }

        public KobHolderModel Update(KobHolderModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            KobHolderModel result = repo.Update(victim);
            logTool.LogTrace("ending", methodName);

            return result;
        }
    }
}
