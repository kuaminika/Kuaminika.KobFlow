namespace Kuaniminka.KobFlow.ToolBox
{
    public abstract class AbstractLogTool : IKLogTool
    {
        IKJSONParser _parser;
        public AbstractLogTool(IKJSONParser objParser)
        {
            _parser = objParser;
        }
        public bool LogWithTime { get; set; }
        public bool LogIsOff { get; set; }

        private string time { get { return LogWithTime ? DateTime.Now.ToString() + "-->" : string.Empty; } }
        private string formatLocation(string location)
        {
            return $"[{time}{location}]:";
        }

        protected abstract void _doLog(string message);

        public void Log(string msg, string location = "")
        {
            if (LogIsOff) return;
            _doLog(formatLocation(location) + msg);
        }

        public void LogError(string message, string location = "")
        {
            _doLog(formatLocation(location) + $"ERROR: {message}");
        }

        public void logObject<T>(T list, string location = "")
        {
            if (LogIsOff) return;
            _doLog(formatLocation(location) + _parser.Serialize(list));
        }
    }
}