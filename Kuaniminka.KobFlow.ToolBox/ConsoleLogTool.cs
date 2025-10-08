namespace Kuaniminka.KobFlow.ToolBox
{
    public class ConsoleLogTool : AbstractLogTool
    {
        public ConsoleLogTool(IKJSONParser objParser) : base(objParser) { }
         

        protected override void _doLog(string message, string location = "")
        {
            Console.WriteLine(message);
        }

        protected override void _doLogError(string message, string location = "")
        {
            Console.WriteLine(message);
        }

        protected override void _doTrace(string message, string location = "")
        {
            Console.WriteLine(message);
        }
    }
}