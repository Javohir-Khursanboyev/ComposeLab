using Azure.Storage.Blobs;

namespace Api.Storage;

public sealed class BlobService(BlobServiceClient blobServiceClient) : IBlobService
{
    public async Task<string> UploadAsync(Stream fileStream, string fileName, string containerName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();
        await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, overwrite: true);
        return blobClient.Uri.ToString();
    }

    public Task DeleteAsync(string blobName, string containerName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);
        return blobClient.DeleteIfExistsAsync();
    }
}
