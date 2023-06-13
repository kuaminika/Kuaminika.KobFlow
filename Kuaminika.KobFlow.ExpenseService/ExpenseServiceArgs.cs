using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.ExpenseService
{
    public class ExpenseServiceArgs
    {
        public IExpenseRepository Repo { get; set; }

        public IKLogTool LogTool { get; internal set; }
    }
}