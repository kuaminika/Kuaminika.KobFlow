using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeSourceService
{
    public class IncomeSourceRepoArgs
    {
        public IDataGateway? DataGateway { get; internal set; }
        public IKLogTool? LogTool { get; internal set; }
        public IKJSONParser? JSONParser { get; internal set; }
    }


}