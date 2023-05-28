namespace Kuaniminka.KobFlow.ToolBox
{
    public class ConsoleLogTool : AbstractLogTool
    {
        public ConsoleLogTool(IKJSONParser objParser) : base(objParser) { }
         

        protected override void _doLog(string message)
        {
            Console.WriteLine(message);
        }

        protected override void _doLogError(string message)
        {
            Console.WriteLine(message);
        }

        protected override void _doTrace(string message)
        {
            Console.WriteLine(message);
        }
    }
}