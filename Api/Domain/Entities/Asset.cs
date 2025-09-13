using Api.Domain.Commons;
using Api.Domain.Enums;

namespace Api.Domain.Entities;

public sealed class Asset : BaseModel
{
    public string Path { get; set; } = null!;
    public FileTye FileType { get; set; } 
}