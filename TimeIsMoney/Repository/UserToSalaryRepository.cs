using Microsoft.EntityFrameworkCore;
using TimeIsMoney.Extensions;
using TimeIsMoney.Repository.Infrastructure;
using TimeIsMoney.Repository.Models;

namespace TimeIsMoney.Repository;

public class UserToSalaryRepository : IUserToSalaryRepository
{
    private readonly AppDbContext _context;

    public UserToSalaryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateOrUpdateAsync(UserSalaryDbo salaryDbo)
    {
        var existing = await _context.UserSalaries.Where(x => x.UserId == salaryDbo.UserId).FirstOrDefaultAsync();
        if (existing != null)
        {
            existing.MonthSalary = salaryDbo.MonthSalary;
        }
        else
        {
            await _context.UserSalaries.AddAsync(salaryDbo);
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string userId)
    {
        await _context.UserSalaries.Where(x => x.UserId == userId).ExecuteDeleteAsync();
    }

    public async Task<UserSalaryDbo?> FindByUserIdAsync(string userId)
    {
        return await _context.UserSalaries.Where(x => x.UserId == userId).FirstOrDefaultAsync();
    }
}