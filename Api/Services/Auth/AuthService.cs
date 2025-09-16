using System.Text;
using Api.Exceptions;
using Api.Repositories;
using Api.RequestViews;
using Api.Services.Redis;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Services.Auth;

public sealed class AuthService(
    IConfiguration config,
    IUserRepository userRepository,
    IRedisService redisService) : IAuthService
{
    public async Task<LoginView> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email)
            ?? throw new BadRequestException("User not found");

        VerifyPassword(request.Password, user.PasswordHash);

        var token = GenerateToken(user.Id, "User");
        var refreshToken = GenerateRefreshToken();

        var redisKey = $"refresh{user.Id}";
        await redisService.SetToRedisAsync(redisKey, refreshToken, TimeSpan.FromMinutes(15));

        return new LoginView
        {
            Token = token,
            RefreshToken = refreshToken
        };
    }

    public async Task<LoginView> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var user = await userRepository.GetByIdAsync(request.UserId)
            ?? throw new BadRequestException("User not found");

        var redisKey = $"refresh{user.Id}";
        var stored = redisService.GetFromRedisAsync(redisKey);

        if(stored == null || stored.Result != request.RefreshToken)
            throw new BadRequestException("Invalid refresh token");

        var token = GenerateToken(user.Id, "User");
        var refreshToken = GenerateRefreshToken();

        await redisService.SetToRedisAsync(redisKey, refreshToken, TimeSpan.FromMinutes(15));

        return new LoginView()
        {
            Token = token,
            RefreshToken = refreshToken
        };
    }

    private string GenerateToken(int id, string role)
    {
        var securityKey = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim("Role", role)
        };
        var token = new JwtSecurityToken(
            issuer: "test",
            audience: "test",
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static void VerifyPassword(string requestPassword, string hashedPassword)
    {
        if (!BCrypt.Net.BCrypt.Verify(requestPassword, hashedPassword))
            throw new BadRequestException("Email or password is incorrect!");
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}