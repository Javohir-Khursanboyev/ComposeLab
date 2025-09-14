using Api.RequestViews;
using Api.Domain.Commons;

namespace Api.Domain.Entities;

public sealed class User : BaseModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int? AssetId { get; set; }
    public Asset Asset { get; set; } = null!;

    public static User Create(CreateUserRequest request)
    {
        return new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
    }

    public static UserView View(User user)
    {
        return new UserView
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
           // AssetView = Asset.View(user.Asset) ?? null!
        };
    }
}