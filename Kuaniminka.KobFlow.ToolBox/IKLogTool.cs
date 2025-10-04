namespace Kuaniminka.KobFlow.ToolBox
{
    public interface IKLogTool
    {
        bool LogIsOff { get; set; }
        bool TraceModeOn { get; set; }
        bool LogWithTime { get; set; }

        void LogError(string message, [System.Runtime.CompilerServices.CallerMemberName] string location = "");
        void Log(string msg, [System.Runtime.CompilerServices.CallerMemberName] string location = "");
        void logObject<T>(T obj, [System.Runtime.CompilerServices.CallerMemberName] string location = "");
        void LogTrace(string message, [System.Runtime.CompilerServices.CallerMemberName] string location = "");
    }
}