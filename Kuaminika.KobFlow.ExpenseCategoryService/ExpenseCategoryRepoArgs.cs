using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.ExpenseCategoryService
{
    public class ExpenseCategoryRepoArgs
    {
        public IDataGateway? DataGateway { get; internal set; }
        public IKLogTool? LogTool { get; internal set; }
        public IKJSONParser? JSONParser { get; internal set; }
    }


}