using Api.Domain.Commons;

namespace Api.Domain.Entities;

public sealed class User : BaseModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int AssetId { get; set; }
    public Asset Asset { get; set; } = null!;
}