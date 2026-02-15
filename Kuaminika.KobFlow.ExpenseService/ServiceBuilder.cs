using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.ExpenseService
{
    public class ServiceBuilder
    {
        private IExpenseRepository merchantRepo;
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
            consoleLog.Log(conenctionString);
            var dbGatewayLog = new DataGateway(conenctionString, consoleLog);
            var logTool = LogToolFactory.New.CreateDbWriter(new LogToolFactoryToolbox
            {
                ObjParser = new KNewtonJSonParser(),
                DbGateway = dbGatewayLog ,
                BackupLogTool = consoleLog,

            });
            logTool.ServiceName = "ExpenseService";
            logTool.Application = "Kuaminika.KobFlow";
            this.LogTool = logTool;
            this.LogTool.TraceModeOn = true;
            this.LogTool.LogWithTime = true;



            this.dbGateway = new DataGateway(conenctionString, consoleLog); ;// new DataGateway(conenctionString, this.LogTool );

           
            this.merchantRepo = new ExpenseRepo(new ExpenseRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });

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
        public ServiceBuilder BuildRepository(IExpenseRepository repo = null)
        {
            if (repo != null)
            {
                this.merchantRepo = repo;
                return this;
            }
            return this;
        }


        public IExpenseService Build()
        {
            this.merchantRepo = new ExpenseRepo(new ExpenseRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });
            ExpenseServiceArgs args = new ExpenseServiceArgs { LogTool = this.LogTool, Repo = this.merchantRepo };
            args.CacheTool = new CacheHolder<ExpenseModel>(CacheRoot.MemoryCache, KConstants.CacheExpirationInSeconds);
            args.IdentityMap = new KIdentityMap<ExpenseModel>();
            IExpenseService result = new ExpenseService(args);

            return result;
        }
    }
}