using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeCategory
{
    public class IncomeCategoryServiceArgs
    {
        public IIncomeCategoryRepository Repo { get; set; }

        public IKLogTool LogTool { get; internal set; }
    }
}