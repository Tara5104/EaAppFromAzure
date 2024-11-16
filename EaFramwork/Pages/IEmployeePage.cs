using EaFramwork.Model;
using Microsoft.Playwright;

namespace EaFramwork.Pages
{
    public interface IEmployeePage
    {
        Task ClickCreate();
        Task ClickCreateNew();
        Task ClickEmployeeList();
        Task CreateNewEmployee(Employee emp);
        ILocator IsEmployeeCreateAsync(string name);
    }
}