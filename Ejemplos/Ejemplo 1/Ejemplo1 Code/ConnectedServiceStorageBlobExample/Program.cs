using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectedServiceStorageBlobExample
{
    class Program
    {
        static string _connectionString = "<CXString>";
        static string _blobContainerName = "test-blob-container";
        static string _fileName = "file1.txt";

        static void Main(string[] args)
        {

            var storageAccount = CloudStorageAccount.Parse(_connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(_blobContainerName);
            var created = container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container,
            });

            var blob = container.GetBlockBlobReference($"{DateTime.UtcNow.ToString("yyyy/MM/dd/HH/mm/ss")}/{_fileName}");

            using (var fileStream = System.IO.File.OpenRead(_fileName))
            {
                blob.Properties.ContentType = "text/plain; charset=ISO-8859-15";
                blob.UploadFromStream(fileStream);
            }

        }
    }
}
