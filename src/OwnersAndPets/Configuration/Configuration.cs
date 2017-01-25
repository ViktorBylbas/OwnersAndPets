using System.IO;
using Newtonsoft.Json;

namespace OwnersAndPets.Configuration
{
    public class Configuration
    {
        public string ConnectionString { get; set; }
        public string PathToDb { get; set; }
    }

    public class ConfigurationProvider
    {
        public string PathToConfigFile { get; set; }

        public ConfigurationProvider(string pathToConfigFile)
        {
            PathToConfigFile = pathToConfigFile;
        }

        public Configuration LoadConfig()
        {
            string json = File.ReadAllText(PathToConfigFile);      
            return JsonConvert.DeserializeObject<Configuration>(json);
        }
    }
}
