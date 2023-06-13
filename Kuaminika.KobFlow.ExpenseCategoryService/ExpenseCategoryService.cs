
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;

namespace Kuaminika.KobFlow.ExpenseCategoryService
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private IExpenseCategoryRepository repo;
        private IKLogTool logTool;

        public ExpenseCategoryService(ExpenseCategoryServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;

        }
        public ExpenseCategoryModel Add(ExpenseCategoryModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseCategoryModel result = repo.Add(addMe);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public ExpenseCategoryModel Delete(ExpenseCategoryModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseCategoryModel result = repo.Delete(victim);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public List<ExpenseCategoryModel> GetAll()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            List<ExpenseCategoryModel> result = repo.GetAll();
            logTool.LogTrace("ending", methodName);

            return result;

        }

        public ExpenseCategoryModel Update(ExpenseCategoryModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseCategoryModel result = repo.Update(victim);
            logTool.LogTrace("ending", methodName);

            return result;
        }
    }
}
