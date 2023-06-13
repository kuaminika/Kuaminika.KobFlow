﻿using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.IncomeSourceService
{
    public class ServiceBuilder
    {
        private IIncomeSourceRepository merchantRepo;
        private IDataGateway dbGateway;

        public static ServiceBuilder Create(IKonfig config)
        {
            return new ServiceBuilder(config);
        }

        private ServiceBuilder(IKonfig config)
        {
            string conenctionString = config.GetStringValue("connectionString");
            kJSONParser = new KNewtonJSonParser();
            this.LogTool = new ConsoleLogTool(kJSONParser);
            this.LogTool.TraceModeOn = true;
            this.LogTool.LogWithTime = true;
            this.dbGateway = new DataGateway(conenctionString);
            this.merchantRepo = new IncomeSourceRepo(new IncomeSourceRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });

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
        public ServiceBuilder BuildRepository(IIncomeSourceRepository repo = null)
        {
            if (repo != null)
            {
                this.merchantRepo = repo;
                return this;
            }
            return this;
        }


        public IIncomeSourceService Build()
        {
            this.merchantRepo = new IncomeSourceRepo(new IncomeSourceRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });
            IncomeSourceServiceArgs args = new IncomeSourceServiceArgs { LogTool = this.LogTool, Repo = this.merchantRepo };

            IIncomeSourceService result = new IncomeSourceService(args);

            return result;
        }
    }
}