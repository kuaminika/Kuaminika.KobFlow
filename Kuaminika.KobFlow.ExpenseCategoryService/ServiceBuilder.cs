﻿using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.ExpenseCategoryService
{
    public class ServiceBuilder
    {
        private IExpenseCategoryRepository merchantRepo;
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
            this.merchantRepo = new ExpenseCategoryRepo(new ExpenseCategoryRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });

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
        public ServiceBuilder BuildRepository(IExpenseCategoryRepository repo = null)
        {
            if (repo != null)
            {
                this.merchantRepo = repo;
                return this;
            }
            return this;
        }


        public IExpenseCategoryService Build()
        {
            this.merchantRepo = new ExpenseCategoryRepo(new ExpenseCategoryRepoArgs() { DataGateway = dbGateway, JSONParser = kJSONParser, LogTool = this.LogTool });
            ExpenseCategoryServiceArgs args = new ExpenseCategoryServiceArgs { LogTool = this.LogTool, Repo = this.merchantRepo };

            IExpenseCategoryService result = new ExpenseCategoryService(args);

            return result;
        }
    }
}