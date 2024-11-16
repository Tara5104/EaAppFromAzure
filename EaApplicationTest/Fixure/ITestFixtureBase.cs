namespace EaApplicationTest.Fixure
{
    public interface ITestFixtureBase
    {
        Task NavigateToURl();
        Task TakeScreenShotAsync(string fileName);
    }
}