using Api.Domain.Enums;

namespace Api.RequestViews;

public class AssetView
{
    public int Id { get; set; }
    public string Path { get; set; } = null!;
    public FileTye FileTye { get; set; }
}