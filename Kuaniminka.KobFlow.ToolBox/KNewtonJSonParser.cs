namespace Kuaniminka.KobFlow.ToolBox
{
    public class KNewtonJSonParser : IKJSONParser
    {
        public T Parse<T>(string str)
        {
           T result =  Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
            return result;
        }

        public string Serialize<T>(T obj)
        {

          string result =     Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return result;
        }
    }
}