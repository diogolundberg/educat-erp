using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Upload.ViewModel;

namespace Upload.Controllers
{
    [Route("api/[controller]")]
    public class PresignController : Controller
    {
        private readonly IConfiguration _configuration;

        public PresignController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public string GenerateLink([FromBody]Presign presign)
        {
            string accountName = _configuration["BLOB_AZURE_ACCOUNT_NAME"];
            string accessKey = _configuration["BLOB_AZURE_ACCESS_KEY"];
            string containerName = _configuration["BLOB_AZURE_CONTAINER"];

            StorageCredentials auth = new StorageCredentials(accountName, accessKey);
            CloudStorageAccount account = new CloudStorageAccount(auth, true);
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(presign.FileName);

            SharedAccessBlobPolicy adHocSAS = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["BLOB_AZURE_SHARED_ACCESS_EXPIRY_TIME"])),
                Permissions = SharedAccessBlobPermissions.Write
            };

            string sasBlobToken = blob.GetSharedAccessSignature(adHocSAS);
            
            string uri = blob.Uri + sasBlobToken;

            return uri;
        }
    }
}