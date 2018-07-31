namespace SentimentAnalysis.Web.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using SentimentAnalysis.Web.Interfaces;

    public class BlobStorageService : BaseService, IBlobStorageService
    {
        CloudStorageAccount storageAccount = new CloudStorageAccount(
        new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
        "sentimentanalysisweb",
        Environment.GetEnvironmentVariable("BlobKey")), true);
        
        private CloudBlobClient blobClient;
        private CloudBlobContainer container;
        private CloudBlockBlob blockBlob;

        private readonly IMemoryCache _cache;
        private readonly IAppInsightsLoggerService _logger;

        public BlobStorageService(IMemoryCache cache, IAppInsightsLoggerService logger) : base(cache, logger)
        {
            _logger = logger;
            _cache = cache;

            this.blobClient = storageAccount.CreateCloudBlobClient();
            this.container = blobClient.GetContainerReference("trained-models");
            this.blockBlob = container.GetBlockBlobReference("SentimentAnalysisModel");
        }

        public async Task<Stream> DownloadModelAsync()
        {
            Stream stream;

            try
            {
                _cache.TryGetValue("trainedModel", out stream);

                if (stream != null)
                {
                    return stream;
                }

                else
                {
                    await blockBlob.DownloadToStreamAsync(stream);
                    _cache.Set<Stream>("trainedModel", stream);
                }

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
