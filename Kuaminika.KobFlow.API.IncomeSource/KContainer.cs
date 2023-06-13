using Kuaminika.KobFlow.IncomeSourceService;
using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.API.IncomeSource
{
    public class KContainer
    {
        private IKonfig configs;
        private ServiceBuilder sb;

        public KContainer(IConfiguration configs)
        {
            this.configs = new DefaultConfigTool(configs);

            this.sb = ServiceBuilder.Create(this.configs);

            this.LogTool = sb.LogTool;
            this.LogTool.LogWithTime = true;
        }

        public IIncomeSourceService Service { get { return sb.Build(); } }
        public IKLogTool LogTool { get; internal set; }
    }
}