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

    public async Task CreateAsync(UserSalaryDbo salaryDbo)
    {
        await _context.UserSalaries.AddAsync(salaryDbo);
        await _context.SaveChangesAsync();
    }

    public async Task<UserSalaryDbo?> FindByUserIdAsync(string userId)
    {
        return await _context.UserSalaries.Where(x => x.UserId == userId).FirstOrDefaultAsync();
    }
}