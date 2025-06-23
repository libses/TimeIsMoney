namespace TimeIsMoney.Repository.Models;

public class UserSalaryDbo
{
    public required string UserId { get; set; }
    public decimal MonthSalary { get; set; }
}