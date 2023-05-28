using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.MerchantService
{
    public class MerchantRepoArgs
    {
        public IDataGateway? DataGateway { get; internal set; }
        public IKLogTool? LogTool { get; internal set; }
        public IKJSONParser? JSONParser { get; internal set; }
    }


}