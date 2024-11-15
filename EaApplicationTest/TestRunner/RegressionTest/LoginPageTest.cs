using EaFramwork.Config;
using EaFramwork.Driver;
using EaFramwork.Model;
using EaFramwork.Pages;
using Microsoft.Playwright;

namespace EaApplicationTest.TestRunner.RegressionTest;

public class LoginPageTest
{
    private readonly string _Url = "http://www.eaapp.somee.com";
    LoginPage loginPage;
    EmployeePage employeePage;
    Employee emp;
    private readonly IPlaywrightDriver _playwrightDriver;
    private readonly IEmployeePage _employeePage;
    private readonly ILoginPage _loginPage;
    private readonly TestSettings _testSettings;

    public LoginPageTest(IPlaywrightDriver playwrightDriver,TestSettings testSettings,IEmployeePage employeePage,ILoginPage loginPage)
    {
        _playwrightDriver = playwrightDriver;   
        _testSettings = testSettings;
        _employeePage = employeePage;
        _loginPage = loginPage;

     }

    [Fact]
    public async Task LoginTest()
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync(_testSettings.Application_Url);
       
        await _loginPage.ClickLogin();
        await _loginPage.Login("admin", "password");

        var isExist = await _loginPage.IsEmployeeDetailsExists();
        Assert.True(isExist, "Employee details section was not visible.");  

    }
    [Fact]
    public async Task CreatNewEmployeeTest()
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync(_Url);       
    
       await page.Context.Tracing.StartAsync(new ()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        await _loginPage.ClickLogin();
        await _loginPage.Login("admin", "password");

        var isExist = await loginPage.IsEmployeeDetailsExists();
        Assert.True(isExist, "Employee details section was not visible."); 

        
        var emp = new Employee 
        {
            Name = "Maya",
            Salary= 20000,
            DurationWorked =2,
            Grade = Grade.CLevel,
            Email ="Maya@html.com"
        };

        await _employeePage.ClickEmployeeList();
        await _employeePage.ClickCreateNew();
        await _employeePage.CreateNewEmployee(emp);
        await employeePage.ClickCreate();

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
                Assert.True(empCount > 1,$"No employees with the name '{emp.Name}' were found.");
                System.Diagnostics.Debug.WriteLine($"Employee {i + 1}: {employeeName}");
            }          
        }
        // Your test code here

        await page.Context.Tracing.StopAsync(new()
        {
            Path = @"C:\Users\pares\source\repos\EaApplicationTest\EaApplicationTest\bin\Debug\net8.0\trace.zip" // This generates a trace file you can open in Playwright Trace Viewer
        });
        // await Assertions.Expect(locator).ToBeVisibleAsync();
    }

}
