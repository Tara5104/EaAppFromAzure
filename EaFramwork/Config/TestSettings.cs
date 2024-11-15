namespace EaFramwork.Config;

public class TestSettings
{
  //  public required string[]? Args { get; set; }
    public bool Headless { get; set; }
    public float Timout { get; set; }
    public required string Application_Url { get; set; }
    public float SlowMo { get; set; }  
    public DriverType DriverType
    { get; set; }
}
public enum DriverType
{
    Chromium,
    Firefox,
    Edge,
    Chrome,
    WebKit
}
public class ViewPort
{
    public int Width { get; set; }
    public int Height { get; set; }
    public ViewPort(int width, int height)
    {
        Width = width;
        Height = height;
    }

}
