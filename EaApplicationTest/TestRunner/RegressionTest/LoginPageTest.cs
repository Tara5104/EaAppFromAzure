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
    private PlaywrightDriver _driver;  
    private TestSettings _testSettings;

    public LoginPageTest(IPlaywrightDriverInitializer playwrightDriverInitializer)
    {
        _testSettings = ConfigReader.ReadConfig();        
        _driver = new PlaywrightDriver(_testSettings, playwrightDriverInitializer);
    }

    [Fact]
    public async Task LoginTest()
    {
        var page = await _driver.Page;
        await page.GotoAsync(_Url);

        loginPage = new LoginPage(page);

        await loginPage.ClickLogin();
        await loginPage.Login("admin", "password");

        var isExist = await loginPage.IsEmployeeDetailsExists();
        Assert.True(isExist, "Employee details section was not visible.");  

    }
    //[Fact]
    public async Task CreatNewEmployeeTest()
    {
        var page = await _driver.Page;
        await page.GotoAsync(_Url);

        loginPage = new LoginPage(page);
    
       await page.Context.Tracing.StartAsync(new ()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        await loginPage.ClickLogin();
        await loginPage.Login("admin", "password");

        var isExist = await loginPage.IsEmployeeDetailsExists();
        Assert.True(isExist, "Employee details section was not visible."); 

        employeePage = new EmployeePage(page);
        var emp = new Employee 
        {
            Name = "Hina",
            Salary= 20000,
            DurationWorked =2,
            Grade = Grade.CLevel,
            Email ="hina@html.com"
        };

        await employeePage.ClickEmployeeList();
        await employeePage.ClickCreateNew();
        await employeePage.CreateNewEmployee(emp);
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
