using Api.RequestViews;

namespace Api.Services.Auth;

public interface IAuthService
{
    Task<LoginView> LoginAsync(LoginRequest request);
}