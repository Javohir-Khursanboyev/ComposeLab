using Api.RequestViews;
using Api.Services.Users;
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

    [HttpPost("{id:int}/image")]
    public async Task<ApiResponse> UploadImageAsync([FromRoute] int id, [FromForm] CreateAssetRequest request)
    {
        await userService.UploadImageAsync(id, request);
        return ApiResponse.Ok();
    }
}