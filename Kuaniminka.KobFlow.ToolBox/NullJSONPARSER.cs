namespace Kuaniminka.KobFlow.ToolBox
{
    public class NullJSONPARSER : IKJSONParser
    {
        public T Parse<T>(string str)
        {
            throw new Exception("JSON parser is not set cannot proceed parse");
        }

        public string Serialize<T>(T obj)
        {
            throw new Exception("JSON parser is not set cannot proceed serialize");
        }
    }


}