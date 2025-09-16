using Api.RequestViews;
using Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ApiResponse> AdminLoginAsync(LoginRequest request)
    {
        var response = await authService.LoginAsync(request);

        return ApiResponse.Success(response);
    }
}
