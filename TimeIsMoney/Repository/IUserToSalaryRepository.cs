using TimeIsMoney.Repository.Models;

namespace TimeIsMoney.Repository;

public interface IUserToSalaryRepository
{
    public Task CreateAsync(UserSalaryDbo salaryDbo);
    
    public Task<UserSalaryDbo?> FindByUserIdAsync(string userId);
}