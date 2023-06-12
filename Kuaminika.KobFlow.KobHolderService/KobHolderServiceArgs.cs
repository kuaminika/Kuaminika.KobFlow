using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.KobHolderService
{
    public class KobHolderServiceArgs
    {
        public IKobHolderRepository Repo { get; set; }

        public IKLogTool LogTool { get; internal set; }
    }
}