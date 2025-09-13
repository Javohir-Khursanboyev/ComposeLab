using Api.RequestViews;

namespace Api.Services;

public interface IUserService
{
    Task<int> CreateAsync(CreateUserRequest request);
    Task<UserView> GetByIdAsync(int id);
}