using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Api.Services.Storage;

public sealed class BlobService(BlobServiceClient blobServiceClient) : IBlobService
{
    public async Task UploadAsync(Stream fileStream, string fileName, string containerName, string contentType)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(fileName);

        // Ensure stream at beginning
        if (fileStream.CanSeek) fileStream.Position = 0;

        var headers = new BlobHttpHeaders { ContentType = contentType };
        await blobClient.UploadAsync(fileStream, new BlobUploadOptions { HttpHeaders = headers });
    }

    public Task DeleteAsync(string blobName, string containerName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);
        return blobClient.DeleteIfExistsAsync();
    }
}
