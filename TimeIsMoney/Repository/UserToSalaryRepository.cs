using TimeIsMoney.Extensions;
using TimeIsMoney.Repository.Models;

namespace TimeIsMoney.Repository;

public class UserToSalaryRepository : IUserToSalaryRepository
{
    private Dictionary<string, UserSalaryDbo> userSalaries = new Dictionary<string, UserSalaryDbo>();
    
    public Task CreateAsync(UserSalaryDbo salaryDbo)
    {
        userSalaries.Add(salaryDbo.UserId, salaryDbo);
        return Task.CompletedTask;
    }

    public Task<UserSalaryDbo?> FindByUserIdAsync(string userId)
    {
        return !userSalaries.TryGetValue(userId, out var salary) ? ((UserSalaryDbo?) null).AsTask() : ((UserSalaryDbo?)salary).AsTask();
    }
}