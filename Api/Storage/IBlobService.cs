namespace Api.Storage;

public interface IBlobService
{
    Task UploadAsync(Stream fileStream, string fileName, string containerName, string contentType);
    Task DeleteAsync(string blobName, string containerName);
}