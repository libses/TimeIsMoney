namespace TimeIsMoney.Repository.Models;

public class UserSalaryDbo
{
    public Guid Id { get; set; }
    public required string UserId { get; set; }
    public decimal MonthSalary { get; set; }
}