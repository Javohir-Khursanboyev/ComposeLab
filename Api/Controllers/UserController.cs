using Api.Services;
using Api.RequestViews;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<ApiResponse> CreateAsync([FromBody] CreateUserRequest request)
    {
        int userId = await userService.CreateAsync(request);

        return ApiResponse.Success(userId);
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse> GetByIdAsync([FromRoute] int id)
    {
        var user = await userService.GetByIdAsync(id);

        return ApiResponse.Success(user);
    }
}