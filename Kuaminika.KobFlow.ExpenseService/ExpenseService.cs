
using Kuaniminka.KobFlow.ToolBox;
using System;
using System.Collections.Generic;

namespace Kuaminika.KobFlow.ExpenseService
{
    public class ExpenseService : IExpenseService
    {
        private IExpenseRepository repo;
        private IKLogTool logTool;

        public ExpenseService(ExpenseServiceArgs args)
        {
            this.repo = args.Repo;
            this.logTool = args.LogTool;

        }
        public ExpenseModel Add(ExpenseModel addMe)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseModel result = repo.Add(addMe);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public ExpenseModel Delete(ExpenseModel victim)
        {

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseModel result = repo.Delete(victim);
            logTool.LogTrace("ending", methodName);

            return result;
        }

        public List<ExpenseModel> GetAll()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            List<ExpenseModel> result = repo.GetAll();
            logTool.LogTrace("ending", methodName);

            return result;

        }

        public ExpenseModel Update(ExpenseModel victim)
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            string methodName = method == null ? "" : method.Name;
            logTool.LogTrace("starting", methodName);
            ExpenseModel result = repo.Update(victim);
            logTool.LogTrace("ending", methodName);

            return result;
        }
    }
}
