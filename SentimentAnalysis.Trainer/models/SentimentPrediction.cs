namespace SentimentAnalysis.Trainer.models
{
    using Microsoft.ML.Runtime.Api;
    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Sentiment;
    }
}
