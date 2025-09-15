namespace Api.Services.Files;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, Type entityType);
    Task<List<string>> SaveFileAsync(List<IFormFile> files, Type entityType);
    Task DeleteFileAsync(string relativePath);
}