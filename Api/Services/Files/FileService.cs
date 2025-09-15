using Api.Storage;

namespace Api.Services.Files;

public sealed class FileService(IBlobService blobService) : IFileService
{
    public async Task<string> SaveFileAsync(IFormFile file, Type entityType)
    {
        var folderName = entityType.Name.ToLower() + "s";
        var fileName = string.Concat(DateTime.UtcNow.Ticks, "_", file.FileName.Trim());

        using var stream = file.OpenReadStream();
        await blobService.UploadAsync(stream, fileName, folderName, file.ContentType);

        var relativePath = Path.Combine(folderName, fileName).Replace("\\", "/");
        return relativePath;
    }

    public Task DeleteFileAsync(string relativePath)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> SaveFileAsync(List<IFormFile> files, Type entityType)
    {
        throw new NotImplementedException();
    }
}