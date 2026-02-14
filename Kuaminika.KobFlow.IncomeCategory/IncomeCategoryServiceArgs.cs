using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeCategory
{
    public class IncomeCategoryServiceArgs
    {
        public IIncomeCategoryRepository Repo { get; set; }

        public IKLogTool LogTool { get; internal set; }
        public IKIdentityMap<IncomeCategoryModel>? IdentityMap { get; internal set; }
        public ICacheHolder<IncomeCategoryModel>? CacheTool { get; internal set; }
    }
}