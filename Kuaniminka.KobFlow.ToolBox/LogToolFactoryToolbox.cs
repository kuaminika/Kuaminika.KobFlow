namespace Kuaniminka.KobFlow.ToolBox
{
    public class LogToolFactoryToolbox
    {
        public IKJSONParser ObjParser { get;  set; }
        public IDataGateway DbGateway { get;  set; }
        public IKLogTool BackupLogTool { get;  set; }
    }
}