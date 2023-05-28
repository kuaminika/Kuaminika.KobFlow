namespace Kuaniminka.KobFlow.ToolBox
{
    public abstract class AbstractLogTool : IKLogTool
    {
        IKJSONParser _parser;
        public AbstractLogTool(IKJSONParser objParser)
        {
            _parser = objParser;
            TraceModeOn = true;
        }
        public bool LogWithTime { get; set; }
        public bool LogIsOff { get; set; }

        private string time { get { return LogWithTime ? DateTime.Now.ToString() + "-->" : string.Empty; } }

        public bool TraceModeOn { get; set; }

        private string formatLocation(string location)
        {
            return $"[{time}{location}]:";
        }

        protected abstract void _doLogError(string message);
        protected abstract void _doLog(string message);
        protected abstract void _doTrace(string message);

        public void Log(string msg, string location = "")
        {
            if (LogIsOff) return;
            _doLog(formatLocation(location) + msg);
        }

        public void LogError(string message, string location = "")
        {
            _doLogError(formatLocation(location) + $"ERROR: {message}");
        }

        public void logObject<T>(T list, string location = "")
        {
            if (LogIsOff) return;
            try
            {
                _doLog(formatLocation(location) + _parser.Serialize(list));
            }
            catch (Exception ex)
            {

                LogError(ex.Message, GetType().Name+".logOBJ");
            }
        }

        public void LogTrace(string message, string location = "")
        {
            if (!TraceModeOn) return;

            _doTrace(formatLocation(location) + message);
        }
    }
}