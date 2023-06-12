using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeCategory
{
    public class IncomeCategoryRepoArgs
    {
        public IDataGateway? DataGateway { get; internal set; }
        public IKLogTool? LogTool { get; internal set; }
        public IKJSONParser? JSONParser { get; internal set; }
    }


}