using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeService
{
    public class IncomeServiceArgs
    {
        public IIncomeRepository Repo { get; set; }

        public IKLogTool LogTool { get; internal set; }
    }
}