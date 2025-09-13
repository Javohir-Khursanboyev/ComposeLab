using Api.Data;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public sealed class UserRepository(DataContext context) : IUserRepository
{
    public async Task CreateAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Id == id);  
    }
}
