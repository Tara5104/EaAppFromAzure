using EaFramwork.Config;
using Microsoft.Playwright;

namespace EaFramwork.Driver;


public class PlaywrightDriver : IDisposable, IPlaywrightDriver
{
    private readonly AsyncTask<IBrowser> _browser;
    private readonly AsyncTask<IBrowserContext> _browserContext;
    private readonly AsyncTask<IPage> _page;
    private readonly TestSettings _testSettings;
    private readonly IPlaywrightDriverInitializer _playwrightDriverInitializer;
    private bool _isDisposed;


    public PlaywrightDriver(TestSettings testSettings, IPlaywrightDriverInitializer playwrightDriverInitializer)
    {
        _testSettings = testSettings;
        _playwrightDriverInitializer = playwrightDriverInitializer;
        /**
            * As this is an invocation of an Dependency of Concrete type within constructor,
            * hence the Type should always be resolved in Constructor,
            * which is not correct way of doing in an library of code like Frameworks  
            PlaywrightDriverIntializer playwrightDriverIntializer = new PlaywrightDriverIntializer();
         */
        _browser = new AsyncTask<IBrowser>(InitialzePlaywrightDriverAsync);
        _browserContext = new AsyncTask<IBrowserContext>(CreateBrowserContext);
        _page = new AsyncTask<IPage>(CreatePageAsync);
    }
    private async Task<IBrowser> InitialzePlaywrightDriverAsync()
    {
        return _testSettings.DriverType switch
        {
            DriverType.Chrome => await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings),
            DriverType.Edge => await _playwrightDriverInitializer.GetEdgeDriverAsync(_testSettings),
            DriverType.Firefox => await _playwrightDriverInitializer.GetFirefoxDriverAsync(_testSettings),
            DriverType.WebKit => await _playwrightDriverInitializer.GetWebKitDriverAsync(_testSettings),
            _ => await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings),

        };
    }
    private BrowserNewContextOptions CreateContextOptions(ViewportSize? viewport = null)
    {
        return new BrowserNewContextOptions
        {
            ViewportSize = viewport ?? new ViewportSize { Width = 1920, Height = 1080 }// Set this to null for maximized or specify a ViewportSize if needed
        };
    }
    private async Task<IBrowserContext> CreateBrowserContext()
    {
        var contextOptions = CreateContextOptions();
        return await (await _browser).NewContextAsync(contextOptions);
    }
    private async Task<IPage> CreatePageAsync()
    {
        return await (await _browserContext).NewPageAsync();

    }
    public Task<IPage> Page => _page.Value;
    public Task<IBrowser> Browser => _browser.Value;
    public Task<IBrowserContext> BrowserContext => _browserContext.Value;

    public void Dispose()
    {
        if (!_isDisposed) return;

        if (_browser.IsValueCreated)

            Task.Run(async () =>
            {
                await (await _browser).CloseAsync();
                await (await _browserContext).CloseAsync();
            });

        _isDisposed = true;
    }
}
