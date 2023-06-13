using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.ExpenseService
{
    public class ExpenseRepoArgs
    {
        public IDataGateway? DataGateway { get; internal set; }
        public IKLogTool? LogTool { get; internal set; }
        public IKJSONParser? JSONParser { get; internal set; }
    }


}