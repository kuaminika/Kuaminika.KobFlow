using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeSourceService
{
    public class IncomeSourceServiceArgs
    {
        public IIncomeSourceRepository Repo { get; set; }

        public IKLogTool LogTool { get; internal set; }
    }
}