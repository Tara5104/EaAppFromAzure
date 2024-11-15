using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EaFramwork.Config;

public static class ConfigReader
{
    public static TestSettings ReadConfig() 
    {
        var configFile = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/AppConfig.json");
      
            var jsonSerializerSettings = new JsonSerializerOptions()
            {
               PropertyNameCaseInsensitive = true,
            };
        jsonSerializerSettings.Converters.Add(new JsonStringEnumConverter());
        return JsonSerializer.Deserialize<TestSettings>(configFile, jsonSerializerSettings);

    }
}
