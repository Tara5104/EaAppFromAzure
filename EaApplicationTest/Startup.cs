using EaApplicationTest.Fixure;
using EaFramwork.Config;
using EaFramwork.Driver;
using EaFramwork.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace EaApplicationTest;

public class Startup
{
    public void ConfigureServices(IServiceCollection services) 
    {
        services
            .AddSingleton(ConfigReader.ReadConfig())
            .AddScoped<IPlaywrightDriver, PlaywrightDriver>()
            .AddScoped<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>()
            .AddScoped<IEmployeePage, EmployeePage>()
            .AddScoped<ILoginPage, LoginPage>()            
            .AddScoped<ITestFixtureBase, TestFixtureBase>();
    }
}
