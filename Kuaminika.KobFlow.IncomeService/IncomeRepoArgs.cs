using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeService
{
    public class IncomeRepoArgs
    {
        public IDataGateway? DataGateway { get; internal set; }
        public IKLogTool? LogTool { get; internal set; }
        public IKJSONParser? JSONParser { get; internal set; }
    }


}