using MySqlConnector.Logging;

namespace Kuaniminka.KobFlow.ToolBox
{
    public class MYSQLLogTool : AbstractLogTool
    {
        private IDataGateway dbGateway;
        private IKLogTool backupLogTool;
        private enum KLogLevel
        {
            Trace = 0,
            Info = 1,
            Warning = 2,
            Error = 3,
            Critical = 4
        }

        public MYSQLLogTool( LogToolFactoryToolbox logToolFactoryToolbox) : base(logToolFactoryToolbox.ObjParser)
        {
              dbGateway = logToolFactoryToolbox.DbGateway;
            backupLogTool = logToolFactoryToolbox.BackupLogTool;
        }

        public MYSQLLogTool(IKJSONParser objParser, IDataGateway dbGateway, IKLogTool backupLogTool) : base(objParser) { 
            this.dbGateway = dbGateway;
            this.backupLogTool = backupLogTool;

        }

        protected override string formatLocation(string location)
        {
            return $"";
        }

        private async  Task writeLogToDbAsync(string message, string location = "",KLogLevel logLevel =KLogLevel.Info)
        {
            backupLogTool.Log($"will save this log to DB:{message}");
            
             
            await Task.Run(() =>
            {
                try
                {
                    backupLogTool.Log("trying to log to db"+ Task.CurrentId);
                    KWriteResult re =   dbGateway.ExecuteInsert($@"
                        INSERT INTO db_log (log_level, location, message, log_time)
                        VALUES (@level, @location, @message)", new Dictionary<string, object>
                            {
                            {"@level",logLevel},
                            {"@location",location},
                            {"@message",message},
                              {"@log_time",DateTime.Now   }
                        });

                    backupLogTool.logObject(Task.CurrentId+ "--> done running test");
                    backupLogTool.logObject(re, "db log result");
                }
                catch (Exception ex)
                {

                    string err = flattenExceptionMessages(ex);
                    backupLogTool.LogError($"cannot log to db, error:{err}");
                }
            }); 
        }
        private string flattenExceptionMessages(Exception ex)
        {
            string err = ex.Message;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                err += $" | {ex.Message}";
            }
            return err;
        }
        protected override void _doLog(string message, string location = "unknown")
        {
            this._doLog(message,location);
            _ = writeLogToDbAsync(message, location,KLogLevel.Info);
        }

        protected override void _doLogError(string message, string location = "unknown")
        {

            this._doLogError(message, location);
            _ = writeLogToDbAsync(message,location,KLogLevel.Error);
        }

        protected override void _doTrace(string message, string location = "unknown")
        {
            this._doTrace(message, location);
            _ = writeLogToDbAsync(message, location,KLogLevel.Trace);
        }
    }
}