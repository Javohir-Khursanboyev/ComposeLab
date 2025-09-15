namespace Api.Storage;

public interface IBlobService
{
    Task<string> UploadAsync(Stream fileStream, string fileName, string containerName);
    Task DeleteAsync(string blobName, string containerName);
}