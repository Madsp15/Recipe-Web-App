using Azure.Storage.Blobs;

namespace service;

public class BlobService
{
    private readonly BlobServiceClient _client;

    public BlobService(BlobServiceClient client)
    {
        _client = client;
    }

    public string Save(Stream stream, int id)
    {
        
        // Get object to add with container
        var container = _client.GetBlobContainerClient("foodpictures");
        // Get object to interact with the blob
        var blob = container.GetBlobClient(id.ToString());
        // Couldn't find a replace method, so delete if it exists
        if (blob.Exists().Value) blob.Delete();
        // Now upload the file stream for avatar image
        blob.Upload(stream);
        // Return the GUID part of the blob URI
        return blob.Uri.GetLeftPart(UriPartial.Path);
    }
    
    public string SaveWithSecretURL(Stream stream, String? avatar)
    {
        // Last part of url or a unique string (GUID)
        //var name = url != null ? new Uri(url).LocalPath.Split("/").Last() : Guid.NewGuid().ToString();
        // Get object to add with container
        //var container = _client.GetBlobContainerClient("profilepictures");
        // Get object to interact with the blob
        //var blob = container.GetBlobClient(id.ToString());
        // Couldn't find a replace method, so delete if it exists
        //if (blob.Exists().Value) blob.Delete();
        // Now upload the file stream for avatar image
        //blob.Upload(stream);
        // Return the GUID part of the blob URI
        //return blob.Uri.GetLeftPart(UriPartial.Path);
        return "";
    }
    
    
}