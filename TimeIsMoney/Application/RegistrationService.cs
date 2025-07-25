using TimeIsMoney.Repository;
using TimeIsMoney.Repository.Models;

namespace TimeIsMoney.Application;

public class RegistrationService : IRegistrationService
{
    private readonly IUserToSalaryRepository userToSalaryRepository;

    public RegistrationService(IUserToSalaryRepository userToSalaryRepository)
    {
        this.userToSalaryRepository = userToSalaryRepository;
    }

    public async Task<bool> NeedRegistrationAsync(string userId)
    {
        var salary = await userToSalaryRepository.FindByUserIdAsync(userId);
        return salary == null;
    }

    public async Task RegisterAsync(string userId, decimal monthSalary)
    {
        await userToSalaryRepository.CreateOrUpdateAsync(new UserSalaryDbo { UserId = userId, MonthSalary = monthSalary });
    }

    public async Task UnregisterAsync(string userId)
    {
        await userToSalaryRepository.DeleteAsync(userId);
    }
}