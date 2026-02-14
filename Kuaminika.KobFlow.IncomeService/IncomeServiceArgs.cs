using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeService
{
    public class IncomeServiceArgs
    {
        public IIncomeRepository Repo { get; set; }

        public IKLogTool LogTool { get; internal set; }
        public IKIdentityMap<IncomeModel>? IdentityMap { get; internal set; }
        public ICacheHolder<IncomeModel>? CacheTool { get; internal set; }
    }
}