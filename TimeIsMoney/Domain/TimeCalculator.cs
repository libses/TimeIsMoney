namespace TimeIsMoney.Domain;

public class TimeCalculator : ITimeCalculator
{
    public TimeSpan Calculate(decimal userSalary, decimal price)
    {
        var hourSalary = userSalary / (1972.0M / 12.0M);
        var minuteSalary = hourSalary / 60;
        var secondSalary = minuteSalary / 60;
        var secondsToWork = price / secondSalary;
        return TimeSpan.FromSeconds((double) secondsToWork);
    }
}