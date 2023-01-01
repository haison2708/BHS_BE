using BHS.Domain.SeedWork;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BHS.API.Services;

public interface IFileService : ISingletonService
{
    string GetBlob(string name, string containerName);
    Task<IEnumerable<string>> AllBlobs(string containerName);
    Task<bool> UploadBlob(string name, IFormFile file, string containerName);
    Task<bool> DeleteBlob(string name, string containerName);
}

public class FileService : IFileService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public FileService(BlobServiceClient blobServiceClient, IConfiguration configuration)
    {
        _blobServiceClient = blobServiceClient;
        _containerName = configuration["BlobContainerName"];
    }

    public string GetBlob(string name, string containerName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        // this will allow us access to the file inside the container via the file name
        var blobClient = containerClient.GetBlobClient(name);

        return blobClient.Uri.AbsoluteUri;
    }

    public Task<IEnumerable<string>> AllBlobs(string containerName)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UploadBlob(string name, IFormFile file, string containerName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        var blobClient = containerClient.GetBlobClient(name);

        var httpHeaders = new BlobHttpHeaders
        {
            ContentType = file.ContentType
        };

        var res = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);
        return res != null;
    }

    public Task<bool> DeleteBlob(string name, string containerName)
    {
        throw new NotImplementedException();
    }
}