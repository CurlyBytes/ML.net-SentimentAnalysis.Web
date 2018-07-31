namespace SentimentAnalysis.Web.Services
{
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.ML;
    using SentimentAnalysis.Web.Interfaces;
    using SentimentAnalysis.Web.Models;
    using System;
    using System.Threading.Tasks;

    public class ClassifyService : BaseService, IClassifyService
    {
        private readonly IBlobStorageService _blobService;
        private readonly IAppInsightsLoggerService _logger;

        public ClassifyService(IMemoryCache cache, IAppInsightsLoggerService logger, IBlobStorageService blobStorage) 
            : base(cache, logger)
        {
            _blobService = blobStorage;
            _logger = logger;

            if(_blobService == null)
            {
                throw new ArgumentException("Blob storage service cannot be null.");
            }
        }
        public async Task<string> ClassifySentiment(string usersUtterance)
        {
            try
            {
                var data = new SentimentData()
                {
                    SentimentText = usersUtterance
                };

                var blobResult = _blobService.DownloadModelAsync().Result;

                if (blobResult != null)
                {
                    var model = await PredictionModel.ReadAsync<SentimentData, SentimentPrediction>(blobResult);

                    var classification = model.Predict(data);
                    _logger.LogInfo($"Sentiment returned from model was: {classification.Sentiment}");

                    return $"{(classification.Sentiment ? "Positive" : "Negative")}";
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw ex;
            }
        }
    }
}
