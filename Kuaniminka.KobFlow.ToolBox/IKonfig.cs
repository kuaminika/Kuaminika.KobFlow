namespace Kuaniminka.KobFlow.ToolBox
{
    public interface IKonfig
    {
       string GetStringValue(string key);
        bool GetBoolBalue(string key);

        T GetObj<T>(string key);

        int GetIntValue(string v);
    }
}