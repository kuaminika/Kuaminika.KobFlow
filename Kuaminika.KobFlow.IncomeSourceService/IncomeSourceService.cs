
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;

namespace Kuaminika.KobFlow.IncomeSourceService
{
    public class IncomeSourceService : IIncomeSourceService
    {
        private IIncomeSourceRepository repo;
        private IKLogTool logTool;

        public IncomeSourceService(IncomeSourceServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;

        }
        public IncomeSourceModel Add(IncomeSourceModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeSourceModel result = repo.Add(addMe);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public IncomeSourceModel Delete(IncomeSourceModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeSourceModel result = repo.Delete(victim);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public List<IncomeSourceModel> GetAll()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            List<IncomeSourceModel> result = repo.GetAll();
            logTool.LogTrace("ending", methodName);

            return result;

        }

        public IncomeSourceModel Update(IncomeSourceModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeSourceModel result = repo.Update(victim);
            logTool.LogTrace("ending", methodName);

            return result;
        }
    }
}
