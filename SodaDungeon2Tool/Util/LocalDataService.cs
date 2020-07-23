using Newtonsoft.Json;
using SodaDungeon2Tool.Model;
using System.IO;

namespace SodaDungeon2Tool.Util
{
    public static class LocalDataService
    {
        /// <summary>
        /// Save the current configuration in a json file
        /// </summary>
        public static void SaveConfiguration(Configuration config)
        {
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            string FilePath = Directory.GetCurrentDirectory() + "\\config.json";
            System.Console.WriteLine(FilePath);
            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Load the local config.json file if it exists
        /// </summary>
        public static Configuration LoadConfiguration(){

            string FilePath = Directory.GetCurrentDirectory() + "\\config.json";
            Configuration config;
            if (File.Exists(FilePath))
            {                
                string content = File.ReadAllText(FilePath);
                config = JsonConvert.DeserializeObject<Configuration>(content, new JsonSerializerSettings{MissingMemberHandling = MissingMemberHandling.Error });
            }
            else
                config = new Configuration();
            SaveConfiguration(config);
            return config;
        }
    }
}
