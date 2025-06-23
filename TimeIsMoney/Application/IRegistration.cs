namespace TimeIsMoney.Application;

public interface IRegistration
{
    public Task<bool> NeedRegistrationAsync(string userId);
    
    public Task RegisterAsync(string userId, decimal monthSalary);
}