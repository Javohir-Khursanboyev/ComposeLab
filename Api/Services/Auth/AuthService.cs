using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Exceptions;
using Api.Repositories;
using Api.RequestViews;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services.Auth;

public sealed class AuthService(IConfiguration config,IUserRepository userRepository) : IAuthService
{
    public async Task<LoginView> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email)
            ?? throw new BadRequestException("User not found");

        VerifyPassword(request.Password, user.PasswordHash);

        var token = GenerateToken(user.Id, "User");
        return new LoginView
        {
            Token = token
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
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static void VerifyPassword(string requestPassword, string hashedPassword)
    {
        if (!BCrypt.Net.BCrypt.Verify(requestPassword, hashedPassword))
            throw new BadRequestException("Email or password is incorrect!");
    }
}