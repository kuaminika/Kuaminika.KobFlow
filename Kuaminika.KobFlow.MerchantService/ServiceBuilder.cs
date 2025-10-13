using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.MerchantService
{
    public class ServiceBuilder
    {
        private IMerchantRepository merchantRepo;
        private IDataGateway dbGateway;

        public static ServiceBuilder Create(IKonfig config) { 
            return new ServiceBuilder(config);
        }

        private ServiceBuilder(IKonfig config)
        {
            string conenctionString = config.GetStringValue("connectionString");
            kJSONParser = new KNewtonJSonParser();
            var consoleLog = new ConsoleLogTool(kJSONParser);
            this.LogTool = LogToolFactory.New.CreateDbWriter(new LogToolFactoryToolbox
            {
                ObjParser = new KNewtonJSonParser(),
                DbGateway = new DataGateway(conenctionString, consoleLog),
                BackupLogTool = consoleLog
            });
            //TODO : make log.traceModeOn configurable 
            this.LogTool.TraceModeOn = true;
            this.LogTool.LogWithTime = true;
            this.LogTool.ServiceName = "MerchantService";
            this.LogTool.ApplicationName = "KobFlow";            
            this.dbGateway = new DataGateway(conenctionString, this.LogTool);
            this.merchantRepo = new MerchantRepo(new MerchantRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });

        }
        public IKLogTool LogTool { get; private set; }
        public IKJSONParser kJSONParser { get; private set; }

        public ServiceBuilder BuilJSONParser(IKJSONParser parser = null)
        {
            if(parser !=null)
            {
                kJSONParser = parser;
                return this;
            }
            return this;
        }

        public ServiceBuilder BuildLog(IKLogTool logTool= null)
        {
            
            if(logTool != null )
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
        public ServiceBuilder BuildRepository(IMerchantRepository repo= null)
        {
            if (repo != null)
            {
                this.merchantRepo = repo;
                return this;
            }
            return this;
        }


        public IMerchhantService Build()
        {
            this.merchantRepo = new MerchantRepo(new MerchantRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });
            MerchantServiceArgs args = new MerchantServiceArgs { LogTool = this.LogTool , Repo = this.merchantRepo};

            IMerchhantService result = new MerchantService(args);

            return result;
        }
    }
}