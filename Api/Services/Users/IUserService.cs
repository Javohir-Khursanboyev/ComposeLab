using Api.RequestViews;

namespace Api.Services.Users;

public interface IUserService
{
    Task<int> CreateAsync(CreateUserRequest request);
    Task<UserView> GetByIdAsync(int id);
    Task UploadImageAsync(int id, CreateAssetRequest request);
}