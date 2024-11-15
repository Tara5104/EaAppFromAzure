using EaFramwork.Config;
using Microsoft.Playwright;

namespace EaFramwork.Driver;


public class PlaywrightDriverInitializer : IPlaywrightDriverInitializer
{
    public const float DEFAULT_TIMEOUT = 10000;
    public async Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings)
    {
        var options = GetParameters(testSettings.Timout, testSettings.Headless);
        options.Channel = "chrome";
        return await GetBrowserAsync(DriverType.Chromium, options);
    }
    public async Task<IBrowser> GetEdgeDriverAsync(TestSettings testSettings)
    {
        var options = GetParameters( testSettings.Timout, testSettings.Headless);
        options.Channel = "msedge";
        return await GetBrowserAsync(DriverType.Chromium, options);

    }
    public async Task<IBrowser> GetWebKitDriverAsync(TestSettings testSettings)
    {
        var options = GetParameters( testSettings.Timout, testSettings.Headless);
        options.Channel = " ";
        return await GetBrowserAsync(DriverType.WebKit, options);

    }
    public async Task<IBrowser> GetFirefoxDriverAsync(TestSettings testSettings)
    {
        var options = GetParameters( testSettings.Timout, testSettings.Headless);
        options.Channel = "firefox";
        return await GetBrowserAsync(DriverType.Firefox, options);

    }
    public async Task<IBrowser> GetChromiumDriverAsync(TestSettings testSettings)
    {
        var options = GetParameters( testSettings.Timout, testSettings.Headless);
        options.Channel = "chromium";
        return await GetBrowserAsync(DriverType.Chromium, options);
    }

    private async Task<IBrowser> GetBrowserAsync(DriverType driverType, BrowserTypeLaunchOptions options)
    {
        var playwright = await Playwright.CreateAsync();

        return await playwright[driverType.ToString().ToLower()].LaunchAsync(options);

    }
    private BrowserTypeLaunchOptions GetParameters( float? timeout = DEFAULT_TIMEOUT, bool? headless = false, float? slowmo = null)
    {
        return new BrowserTypeLaunchOptions
        {
            //Args = args,
            Timeout = ToMilliseconds(timeout),
            Headless = headless,
            SlowMo = slowmo
        };
    }
   
    private static float? ToMilliseconds(float? seconds)
    {
        return seconds * 1000;
    }  
       
}
