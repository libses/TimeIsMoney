namespace TimeIsMoney.Application;

public interface ITimeService
{
    Task<TimeSpan> GetTimeCost(string userId, decimal price);
}