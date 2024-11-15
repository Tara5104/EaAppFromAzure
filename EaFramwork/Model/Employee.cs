namespace EaFramwork.Model;

public class Employee
{
    public string Name { get; set; }
    public double Salary { get; set; }
    public int DurationWorked { get; set; }
    public Grade Grade { get; set; }
    public string Email {  get; set; }
}
public enum Grade 
{
  Junior,
  Senior,
  Middle,
  CLevel
}
