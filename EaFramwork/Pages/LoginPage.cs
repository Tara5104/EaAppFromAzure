using EaFramwork.Driver;
using Microsoft.Playwright;

namespace EaFramwork.Pages;

public class LoginPage : ILoginPage
{
    private IPage _page;
    public LoginPage(IPlaywrightDriver playwrightDriver) => _page = playwrightDriver.Page.Result;

    private ILocator LoginLink => _page.Locator("text=Login");
    private ILocator TxtUserName => _page.Locator("#UserName");
    private ILocator TxtPassword => _page.Locator("#Password");
    private ILocator BtnLogin => _page.Locator("text=Log in");
    private ILocator LnkEmployeeDetails => _page.Locator("text='Employee Details'");
    private ILocator LnkEmployeeList => _page.Locator("text='Employee List'");
    public async Task ClickLogin()
    {
        await LoginLink.ClickAsync();
        await _page.WaitForURLAsync("**/Login", new PageWaitForURLOptions
        {
            Timeout = 5000
        });
    }

    public async Task Login(string username, string password)
    {
        await TxtUserName.FillAsync(username);
        await TxtPassword.FillAsync(password);
        await BtnLogin.ClickAsync();
    }
    public async Task<bool> WaitForElementTobeVisibleAsync(ILocator locator, int timeout = 10000)
    {
        try
        {
            var elementLocator = locator;
            Console.WriteLine($"Waiting for element with locator: {locator}");
            await elementLocator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeout
            });
            Console.WriteLine("Element is visible.");
            return true;
        }
        catch (TimeoutException)
        {
            Console.WriteLine($"Element with locator {locator} did not become visible within the timeout period.");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }

    }
    public async Task<bool> IsEmployeeDetailsExists()
    {
        return await WaitForElementTobeVisibleAsync(LnkEmployeeDetails);
    }
    public async Task ClickEmployeeList()
    {
        await LnkEmployeeList.ClickAsync();
    }


}
