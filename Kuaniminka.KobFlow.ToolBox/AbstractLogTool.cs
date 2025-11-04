using System.Runtime.CompilerServices;

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
        public string ServiceName { get; set; }
        public string Action { get; set; }
        public string Application { get; set; }

        protected virtual string formatLocation(string location)
        {
            return $"[{time}{location}]:";
        }

        protected abstract void _doLogError(string message, string location = "");
        protected abstract void _doLog(string message, string location = "");
        protected abstract void _doTrace(string message, string location = "");
        /// <summary>
        /// will log only if LogIsOff is false
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="location"></param>
        public void Log(string msg, [CallerMemberName]string location = "")
        {
            if (LogIsOff) return;
            _doLog(formatLocation(location) + msg,location);
        }
        /// <summary>
        /// will always log the error even if log is off
        /// </summary>
        /// <param name="message"></param>
        /// <param name="location"></param>
        public void LogError(string message, [CallerMemberName] string location = "")
        {
            _doLogError(formatLocation(location) + $"ERROR: {message}",location);
        }
        /// <summary>
        /// will log the object as a json string if log is not off
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="location"></param>
        public void logObject<T>(T list, [CallerMemberName] string location = "")
        {
            if (LogIsOff) return;
            try
            {
                _doLog(formatLocation(location) + _parser.Serialize(list), location);
            }
            catch (Exception ex)
            {

                LogError(ex.Message, GetType().Name+".logOBJ");
            }
        }
        /// <summary>
        /// will log only if TraceModeOn is true
        /// </summary>
        /// <param name="message"></param>
        /// <param name="location"></param>
        public void LogTrace(string message, [CallerMemberName] string location = "")
        {
            if (!TraceModeOn) return;

            _doTrace(formatLocation(location) + message, location);
        }
    }
}