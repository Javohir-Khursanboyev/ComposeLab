using Api.RequestViews;
using Api.Domain.Enums;
using Api.Domain.Commons;

namespace Api.Domain.Entities;

public sealed class Asset : BaseModel
{
    public string Path { get; set; } = null!;
    public FileTye FileType { get; set; } 

    public static AssetView View (Asset asset)
    {
        return new AssetView
        {
            Id = asset.Id,
            Path = asset.Path,
            FileTye = asset.FileType
        };
    }
}