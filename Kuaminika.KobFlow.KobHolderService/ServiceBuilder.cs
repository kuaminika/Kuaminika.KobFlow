using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.KobHolderService
{
    public class ServiceBuilder
    {
        private IKobHolderRepository merchantRepo;
        private IDataGateway dbGateway;

        public static ServiceBuilder Create(IKonfig config)
        {
            return new ServiceBuilder(config);
        }

        private ServiceBuilder(IKonfig config)
        {
            string conenctionString = config.GetStringValue("connectionString");
            kJSONParser = new KNewtonJSonParser();
            var consoleLog = new ConsoleLogTool(kJSONParser);
            var dbGatewayLog = new DataGateway(conenctionString, consoleLog);
            this.LogTool = LogToolFactory.New.CreateDbWriter(new LogToolFactoryToolbox
            {
                ObjParser = new KNewtonJSonParser(),
                DbGateway = dbGatewayLog,
                BackupLogTool = consoleLog,

            });
            this.LogTool.TraceModeOn = true;
            this.LogTool.LogWithTime = true;
            this.LogTool.Application = "Kuaminika.KobFlow";
            this.LogTool.ServiceName = "KobHolderService";
            this.dbGateway = new DataGateway(conenctionString, this.LogTool);
            this.merchantRepo = new KobHolderRepo(new KobHolderRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });

        }
        public IKLogTool LogTool { get; private set; }
        public IKJSONParser kJSONParser { get; private set; }

        public ServiceBuilder BuilJSONParser(IKJSONParser parser = null)
        {
            if (parser != null)
            {
                kJSONParser = parser;
                return this;
            }
            return this;
        }

        public ServiceBuilder BuildLog(IKLogTool logTool = null)
        {

            if (logTool != null)
            {
                this.LogTool = logTool;
                return this;
            }
            kJSONParser = kJSONParser ?? new NullJSONPARSER();

            return this;
        }


        public ServiceBuilder BuildRepository(IDataGateway gateway = null)
        {
            if (gateway != null)
            {
                this.dbGateway = gateway;

                return this;
            }
            return this;
        }
        public ServiceBuilder BuildRepository(IKobHolderRepository repo = null)
        {
            if (repo != null)
            {
                this.merchantRepo = repo;
                return this;
            }
            return this;
        }


        public IKobHolderService Build()
        {
            this.merchantRepo = new KobHolderRepo(new KobHolderRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });
            KobHolderServiceArgs args = new KobHolderServiceArgs { LogTool = this.LogTool, Repo = this.merchantRepo, CacheTool= new CacheHolder<KobHolderModel>(CacheRoot.MemoryCache, KConstants.CacheExpirationInSeconds) , IdentityMap = new KIdentityMap<KobHolderModel>()};

            IKobHolderService result = new KobHolderService(args);

            return result;
        }
    }
}