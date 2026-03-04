using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeSourceService
{
    public class IncomeSourceServiceArgs
    {
        public IIncomeSourceRepository Repo { get; set; }

        public IKLogTool LogTool { get; internal set; }
        public IKIdentityMap<IncomeSourceModel>? IdentityMap { get; internal set; }
        public ICacheHolder<IncomeSourceModel>? CacheTool { get; internal set; }
    }
}