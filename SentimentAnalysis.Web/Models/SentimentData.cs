namespace SentimentAnalysis.Web.Models
{
    using Microsoft.ML.Runtime.Api;

    public class SentimentData
    {
        [Column(ordinal: "0")]
        public float id;

        [Column(ordinal: "1", name: "Label")]
        public float Sentiment;

        [Column(ordinal: "2")]
        public string SentimentText;
    }
}
