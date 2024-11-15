using EaFramwork.Config;
using EaFramwork.Driver;
using Microsoft.Extensions.DependencyInjection;

namespace EaApplicationTest;

public class Startup
{
    public void ConfigureServices(IServiceCollection services) 
    {
        services.AddSingleton(ConfigReader.ReadConfig);
        services.AddScoped<IPlaywrightDriver, PlaywrightDriver>();
        services.AddScoped<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>();
        
    
    }
}
