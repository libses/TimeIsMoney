namespace TimeIsMoney.Application;

public interface IRegistrationService
{
    public Task<bool> NeedRegistrationAsync(string userId);
    
    public Task RegisterAsync(string userId, decimal monthSalary);
}