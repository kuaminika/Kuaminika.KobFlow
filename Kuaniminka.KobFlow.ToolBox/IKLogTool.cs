namespace Kuaniminka.KobFlow.ToolBox
{
    public interface IKLogTool
    {
        bool LogIsOff { get; set; }
        bool LogWithTime { get; set; }

        void LogError(string message, string location = "");

        void Log(string msg, string location = "");
        void logObject<T>(T list, string location = "");
    }
}