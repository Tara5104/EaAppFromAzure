using Microsoft.Playwright;

namespace EaFramwork.Pages
{
    public interface ILoginPage
    {
        Task ClickEmployeeList();
        Task ClickLogin();
        Task<bool> IsEmployeeDetailsExists();
        Task Login(string username, string password);
        Task<bool> WaitForElementTobeVisibleAsync(ILocator locator, int timeout = 10000);
    }
}