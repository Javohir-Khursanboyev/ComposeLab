using Api.Domain.Entities;
using Api.Exceptions;
using Api.Repositories;
using Api.RequestViews;
using Api.Services.Files;

namespace Api.Services.Users;

public sealed class UserService(IUserRepository userRepository, IFileService fileService) : IUserService
{
    public async Task<int> CreateAsync(CreateUserRequest request)
    {
        var user = User.Create(request);

        await userRepository.CreateAsync(user);
        return user.Id;
    }

    public async Task<UserView> GetByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id)
            ?? throw new BadRequestException($"User with id '{id}' not found.");

        return User.View(user);
    }

    public async Task UploadImageAsync(int id, CreateAssetRequest request)
    {
        var user = await userRepository.GetByIdAsync(id)
           ?? throw new BadRequestException($"User with id '{id}' not found.");

        var relativePath = await fileService.SaveFileAsync(request.File, typeof(User));
        var updatedUser = User.UploadImage(user, relativePath);
        await userRepository.UpdateAsync(updatedUser);
    }
}