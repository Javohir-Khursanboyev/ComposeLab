using Api.Domain.Entities;

namespace Api.Repositories;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task<User?> GetByIdAsync(int id);
}