using EaApplicationTest.Fixure;
using EaFramwork.Model;
using EaFramwork.Pages;
using Microsoft.Playwright;

namespace EaApplicationTest.TestRunner.RegressionTest;

public class EmployeePageTest
{
    LoginPage loginPage;
    EmployeePage employeePage;
    Employee emp;
    private readonly ITestFixtureBase _testFixture;
    private readonly IEmployeePage _employeePage;
    private readonly ILoginPage _loginPage;

    public EmployeePageTest(ITestFixtureBase testFixtureBase, IEmployeePage employeePage, ILoginPage loginPage)
    {
        _testFixture = testFixtureBase;
        _employeePage = employeePage;
        _loginPage = loginPage;
    }
    [Fact]
    public async Task CreatNewEmployeeTest()
    {
        await _testFixture.NavigateToURl();

        await _loginPage.ClickLogin();
        await _loginPage.Login("admin", "password");

        var isExist = await loginPage.IsEmployeeDetailsExists();
        Assert.True(isExist, "Employee details section was not visible.");


        var emp = new Employee
        {
            Name = "radha",
            Salary = 20000,
            DurationWorked = 2,
            Grade = Grade.CLevel,
            Email = "radha@html.com"
        };

        await _employeePage.ClickEmployeeList();
        await _employeePage.ClickCreateNew();
        await _employeePage.CreateNewEmployee(emp);
        await _employeePage.ClickCreate();

        var locator = employeePage.IsEmployeeCreateAsync(emp.Name);
        int empCount = await locator.CountAsync();

        System.Diagnostics.Debug.WriteLine($"Total Employees with name {emp.Name}: {empCount}");

        if (empCount == 1)
        {
            Console.WriteLine($"Total Employee with name{emp.Name}");
            await Assertions.Expect(locator).ToBeVisibleAsync();
        }
        else
        {
            for (int i = 0; i < empCount; i++)
            {
                string employeeName = await locator.Nth(i).TextContentAsync();
                Assert.True(empCount > 1, $"No employees with the name '{emp.Name}' were found.");
                System.Diagnostics.Debug.WriteLine($"Employee {i + 1}: {employeeName}");
            }
        }
    }


}
