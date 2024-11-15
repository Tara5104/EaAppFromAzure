using EaFramwork.Model;
using Microsoft.Playwright;

namespace EaFramwork.Pages;

public class EmployeePage
{
    private IPage _page;
    public EmployeePage(IPage page) => _page = page;

    private ILocator LnkEmployeeList => _page.Locator("text='Employee List'");
    private ILocator LnkCreateNew => _page.Locator("text='Create New'");
    private ILocator TxtName => _page.Locator("#Name");
    private ILocator TxtSalary => _page.Locator("#Salary");
    private ILocator TxtDurationWorked => _page.Locator("#DurationWorked");
    private ILocator ListGrade => _page.Locator("#Grade");
    private ILocator TxtEmail => _page.Locator("#Email");
    private ILocator BtnCreate => _page.Locator("input[value='Create']");
    public async Task ClickEmployeeList() => await LnkEmployeeList.ClickAsync();
    public async Task ClickCreateNew() => await LnkCreateNew.ClickAsync();
    public async Task CreateNewEmployee(Employee emp)
    {
        await TxtName.FillAsync(emp.Name);
        await TxtSalary.FillAsync(emp.Salary.ToString());
        await TxtDurationWorked.FillAsync(emp.DurationWorked.ToString());
        await ListGrade.SelectOptionAsync(emp.Grade.ToString());
        await TxtEmail.FillAsync(emp.Email);

    }
    public async Task ClickCreate() => await BtnCreate.ClickAsync();
    public ILocator IsEmployeeCreateAsync(string name)
    {
     return  _page.GetByRole(AriaRole.Cell, new() {Name = name, Exact =true });
    }

}


