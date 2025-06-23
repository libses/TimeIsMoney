namespace TimeIsMoney.Domain;

public class TimeCalculator : ITimeCalculator
{
    public TimeSpan Calculate(decimal userSalary, decimal price)
    {
        var daySalary = userSalary / 30;
        var hourSalary = daySalary / 24;
        var minuteSalary = hourSalary / 60;
        var secondSalary = minuteSalary / 60;
        var secondsToWork = price / secondSalary;
        return TimeSpan.FromSeconds((double) secondSalary);
    }
}