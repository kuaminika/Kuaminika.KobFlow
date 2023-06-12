
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;

namespace Kuaminika.KobFlow.IncomeCategory
{
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private IIncomeCategoryRepository repo;
        private IKLogTool logTool;

        public IncomeCategoryService(IncomeCategoryServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;

        }
        public IncomeCategoryModel Add(IncomeCategoryModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeCategoryModel result = repo.Add(addMe);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public IncomeCategoryModel Delete(IncomeCategoryModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            IncomeCategoryModel result = repo.Delete(victim);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public List<IncomeCategoryModel> GetAll()
        {
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
            logTool.LogTrace("ending", methodName);

            return result;
        }
    }
}
