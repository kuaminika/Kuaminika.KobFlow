using Kuaniminka.KobFlow.ToolBox;
using Microsoft.Extensions.Configuration;

namespace Kuaminika.KobFlow.ToolBox.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TEST_consoleLog()
        {
            
         
            var logTool = LogToolFactory.New.Create();
            logTool.LogWithTime = true;
            logTool.Log("logging with tool without location and time turned on");
            logTool.Log("logging with tool with location  and time turned on", $"{GetType().Name}.TestLogTool");
            logTool.LogWithTime = false;
            logTool.Log("logging with tool with location  and time turned off", $"{GetType().Name}.TestLogTool");

            logTool.logObject(new Tuple<string, string>("str1", "this is an object"), $"{GetType().Name}.TestLogTool");
            logTool.LogWithTime = true;
            logTool.logObject(new Tuple<string, string>("str1 with time on", "this is an object"), $"{GetType().Name}.TestLogTool");

            logTool.LogIsOff = true;
            logTool.Log("logging with tool with location  and time turned off", $"{GetType().Name}.TestLogTool");
            logTool.LogError("logging with log being off");
            logTool.LogIsOff = false;
            logTool.Log("log being back on", $"{GetType().Name}.TestLogTool");
            Assert.True(true);

        }


        [Fact]
        public void TEST_MysqlLog()
        {
           
           
            var logTool = LogToolFactory.New.CreateDbWriter( new LogToolFactoryToolbox {  ObjParser = new KNewtonJSonParser(),
                DbGateway = new DataGateway("Server=sql8.freesqldatabase.com;Port=3306;Database=sql8801460;Uid=sql8801460;Pwd=dEPRrEAM6P", LogToolFactory.New.Create()),
                BackupLogTool = LogToolFactory.New.Create()                
            });
            logTool.LogWithTime = true;
            logTool.Log("logging with tool without location and time turned on");


            logTool.Log("logging with tool with location  and time turned on", $"{GetType().Name}.TestLogTool");
            logTool.LogWithTime = false;
            logTool.Log("logging with tool with location  and time turned off", $"{GetType().Name}.TestLogTool");

            logTool.logObject(new Tuple<string, string>("str1", "this is an object"), $"{GetType().Name}.TestLogTool");
            logTool.LogWithTime = true;
            logTool.logObject(new Tuple<string, string>("str1 with time on", "this is an object"), $"{GetType().Name}.TestLogTool");

            logTool.LogIsOff = true;
            logTool.Log("logging with tool with location  and time turned off", $"{GetType().Name}.TestLog13916Tool");
            logTool.LogError("logging with log being off");
            logTool.LogIsOff = false;
            logTool.Log("log being back on", $"{GetType().Name}.TestLogTool");
            Assert.True(true);
        }
    }
}