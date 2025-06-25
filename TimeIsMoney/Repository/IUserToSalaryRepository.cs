using TimeIsMoney.Repository.Models;

namespace TimeIsMoney.Repository;

public interface IUserToSalaryRepository
{
    public Task CreateOrUpdateAsync(UserSalaryDbo salaryDbo);
    
    public Task DeleteAsync(string userId);
    
    public Task<UserSalaryDbo?> FindByUserIdAsync(string userId);
}