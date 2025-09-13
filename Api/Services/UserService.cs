using Api.Domain.Entities;
using Api.Exceptions;
using Api.Repositories;
using Api.RequestViews;

namespace Api.Services;

public sealed class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<int> CreateAsync(CreateUserRequest request)
    {
        var user = User.Create(request);

        await userRepository.CreateAsync(user);
        return user.Id; 
    }

    public async Task<UserView> GetByIdAsync(int id)
    {
        var student = await userRepository.GetByIdAsync(id)
            ?? throw new BadRequestException($"User with id '{id}' not found.");

        return User.View(student);
    }
}