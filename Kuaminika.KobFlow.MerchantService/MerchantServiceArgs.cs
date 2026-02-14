using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.MerchantService
{
    public class MerchantServiceArgs
    {
        public IMerchantRepository Repo {get;set;}

        public IKLogTool LogTool { get; internal set; }
        public KIdentityMap<MerchantModel>? IdentityMap { get; internal set; }
        public ICacheHolder<MerchantModel>? CacheTool { get; internal set; }
    }
}