namespace Kuaniminka.KobFlow.ToolBox
{
    public class ConsoleLogTool : AbstractLogTool
    {
        public ConsoleLogTool(IKJSONParser objParser) : base(objParser) { }
         

        protected override void _doLog(string message)
        {
            Console.WriteLine(message);
        }
    }
}