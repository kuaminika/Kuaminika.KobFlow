using Kuaniminka.KobFlow.ToolBox;

namespace Kuaminika.KobFlow.Expense.Test
{
    public class ToolBoxTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDefaultLogTool()
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
            Assert.Pass();
        }
    }
}