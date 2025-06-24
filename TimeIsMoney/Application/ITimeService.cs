namespace TimeIsMoney.Application;

public interface ITimeService
{
    Task<TimeSpan> GetTimeCostAsync(string userId, decimal price);
}