namespace TimeIsMoney.Domain;

public interface ITimeCalculator
{
    TimeSpan Calculate(decimal userSalary, decimal price);
}