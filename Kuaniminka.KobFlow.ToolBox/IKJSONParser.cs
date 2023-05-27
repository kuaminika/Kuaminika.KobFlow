namespace Kuaniminka.KobFlow.ToolBox
{
    public interface IKJSONParser
    {
        string Serialize<T>(T obj);
        T Parse<T>(string str);
    }
}