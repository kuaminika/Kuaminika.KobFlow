using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Kuaniminka.KobFlow.ToolBox
{
    public abstract class AbstractLogTool : IKLogTool
    {
        IKJSONParser _parser;
        private string _actionName;

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
        public string ApplicationName { get; set; }
        public string Action { get { return _actionName; } set { _actionName = value ?? "unkown action"; } }


        protected virtual string formatLocation(string location)
        {
            return $"[{time}-{ApplicationName}-{ServiceName},{_actionName}][{location}]:";
        }

        protected abstract void _doLogError(string message, string location = "");
        protected abstract void _doLog(string message, string location = "");
        protected abstract void _doTrace(string message, string location = "");

        private string getFullLocation(string location)
        {
            var frame = new StackTrace().GetFrame(2); // 2 = caller of the caller
            var type = frame.GetMethod().DeclaringType;

            return $"[ {type.Name}.{location}]:";

        }

        /// <summary>
        /// will log only if LogIsOff is false
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="location"></param>
        public void Log(string msg, [CallerMemberName]string location = "")
        {
            location = getFullLocation(location);
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
            location = getFullLocation(location);
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
                location = getFullLocation(location);
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

            location = getFullLocation(location);
            _doTrace(formatLocation(location) + message, location);
        }
    }
}