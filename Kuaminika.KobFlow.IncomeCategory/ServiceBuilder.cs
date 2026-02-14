using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeCategory
{
    public class ServiceBuilder
    {
        private IIncomeCategoryRepository merchantRepo;
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
            this.LogTool.ServiceName = "IncomeCategoryService";
            this.LogTool.Application = "Kuaminika.KobFlow";
            this.LogTool.LogWithTime = true;
            this.dbGateway = new DataGateway(conenctionString, this.LogTool);
            this.merchantRepo = new IncomeCategoryRepo(new IncomeCategoryRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });

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
        public ServiceBuilder BuildRepository(IIncomeCategoryRepository repo = null)
        {
            if (repo != null)
            {
                this.merchantRepo = repo;
                return this;
            }
            return this;
        }


        public IIncomeCategoryService Build()
        {
            this.merchantRepo = new IncomeCategoryRepo(new IncomeCategoryRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });
            IncomeCategoryServiceArgs args = new IncomeCategoryServiceArgs { LogTool = this.LogTool, Repo = this.merchantRepo };
            args.CacheTool = new CacheHolder<IncomeCategoryModel>(CacheRoot.MemoryCache, 60);
            args.IdentityMap = new KIdentityMap<IncomeCategoryModel>();
            IIncomeCategoryService result = new IncomeCategoryService(args);

            return result;
        }
    }
}