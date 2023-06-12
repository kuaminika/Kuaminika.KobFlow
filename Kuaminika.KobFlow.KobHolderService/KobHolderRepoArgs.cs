using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.KobHolderService
{
    public class KobHolderRepoArgs
    {
        public IDataGateway? DataGateway { get; internal set; }
        public IKLogTool? LogTool { get; internal set; }
        public IKJSONParser? JSONParser { get; internal set; }
    }


}