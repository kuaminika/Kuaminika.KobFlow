using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.ExpenseService
{
    public class ExpenseServiceArgs
    {
        public IExpenseRepository Repo { get; set; }

        public IKLogTool LogTool { get; internal set; }
        public IKIdentityMap<ExpenseModel> IdentityMap { get; internal set; }
        public ICacheHolder<ExpenseModel> CacheTool { get; internal set; }
    }
}