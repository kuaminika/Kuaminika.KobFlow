using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.ExpenseCategoryService
{
    public class ExpenseCategoryServiceArgs
    {
        public IExpenseCategoryRepository Repo { get; set; }

        public IKLogTool LogTool { get; internal set; }
        public ICacheHolder<ExpenseCategoryModel>? CacheTool { get; internal set; }
        public IKIdentityMap<ExpenseCategoryModel>? IdentityMap { get; internal set; }
    }
}