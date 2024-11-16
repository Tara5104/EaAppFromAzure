using EaFramwork.Config;
using EaFramwork.Driver;
using Microsoft.Playwright;

namespace EaApplicationTest.Fixure;

public class TestFixtureBase : ITestFixtureBase
{
    private readonly IPlaywrightDriver _driver;
    private readonly TestSettings _testsettings;
    private Task<IPage> _page;
    public TestFixtureBase(IPlaywrightDriver playwrightDriver, TestSettings testSettings)
    {
        _driver = playwrightDriver;
        _testsettings = testSettings;
        _page = _driver.Page;
    }
    public async Task NavigateToURl()
    {
        await (await _page).GotoAsync(_testsettings.Application_Url);
    }
    public async Task TakeScreenShotAsync(string fileName)
    {
        await (await _page).ScreenshotAsync(new PageScreenshotOptions() { Path = fileName, FullPage = true });
    }

}
