using TimeIsMoney.Domain;
using TimeIsMoney.Repository;

namespace TimeIsMoney.Application;

public class TimeService : ITimeService
{
    private readonly ITimeCalculator timeCalculator;
    private readonly IUserToSalaryRepository userToSalaryRepository;

    public TimeService(ITimeCalculator timeCalculator, IUserToSalaryRepository userToSalaryRepository)
    {
        this.timeCalculator = timeCalculator;
        this.userToSalaryRepository = userToSalaryRepository;
    }

    public async Task<TimeSpan> GetTimeCost(string userId, decimal price)
    {
        var userSalary = await userToSalaryRepository.FindByUserIdAsync(userId);
        var time = timeCalculator.Calculate(userSalary.MonthSalary, price);
        return time;
    }
}