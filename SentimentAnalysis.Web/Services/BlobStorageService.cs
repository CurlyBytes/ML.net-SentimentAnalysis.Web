namespace SentimentAnalysis.Web.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using SentimentAnalysis.Web.Interfaces;

    public class BlobStorageService : IBlobStorageService
    {
        CloudStorageAccount storageAccount = new CloudStorageAccount(
        new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
        "sentimentanalysisweb",
        Environment.GetEnvironmentVariable("BlobKey")), true);
        
        private CloudBlobClient blobClient;
        private CloudBlobContainer container;
        private CloudBlockBlob blockBlob;

        private readonly IAppInsightsLoggerService _logger;

        public BlobStorageService(IAppInsightsLoggerService logger)
        {
            _logger = logger;

            this.blobClient = storageAccount.CreateCloudBlobClient();
            this.container = blobClient.GetContainerReference("trained-models");
            this.blockBlob = container.GetBlockBlobReference("SentimentAnalysisModel");
        }

        public async Task<Stream> DownloadModelAsync()
        {
            try
            {
                Stream stream = new MemoryStream();
                await blockBlob.DownloadToStreamAsync(stream);

                return stream;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }
    }
}
