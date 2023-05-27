using Microsoft.Extensions.Configuration;


namespace Kuaniminka.KobFlow.ToolBox
{
    public class DefaultConfigTool : IKonfig
    {
        private IConfiguration config;
        private string ERROR_FETICHING = "Error fetching key '{0}'";
        private string ERROR_NOTFOUND = "Key '{0}' not found";

        public DefaultConfigTool(IConfiguration config)
        {
            this.config = config; 
        }

        public bool GetBoolBalue(string key)
        {
            try 
            {

                if(config[key]==null) throw new Exception(string.Format(ERROR_NOTFOUND, key));
                bool result= false;

                if(! bool.TryParse(config[key], out result)) throw new Exception(string.Format(ERROR_FETICHING, key));

                return result;            
            }
            catch(Exception ex)
            {
                throw new Exception(string.Format(ERROR_FETICHING,key), ex);
            }
        }

        public int GetIntValue(string key)
        {
            try
            {

                if (config[key] == null) throw new Exception(string.Format(ERROR_NOTFOUND, key));
                int result = 0;

                if (!int.TryParse(config[key], out result)) throw new Exception(string.Format(ERROR_FETICHING, key));

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(ERROR_FETICHING, key), ex);
            }
        }

        public T GetObj<T>(string key)
        {
            try
            {

                if (config[key] == null) throw new Exception(string.Format(ERROR_NOTFOUND, key));

                T result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(key);
               
                if(result == null) throw new Exception(string.Format(ERROR_NOTFOUND, key));
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(ERROR_FETICHING, key), ex);
            }
        }

        public string GetStringValue(string key)
        {
            try
            {

                if (config[key] == null) throw new Exception(string.Format(ERROR_NOTFOUND, key));
                string result = config[key];
                if (result == null) throw new Exception(string.Format(ERROR_NOTFOUND, key));
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(ERROR_FETICHING, key), ex);
            }
        }
    }

}