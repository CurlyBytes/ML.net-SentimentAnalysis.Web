namespace SentimentAnalysis.Trainer
{
    using Microsoft.ML;
    using Microsoft.ML.Data;
    using Microsoft.ML.Models;
    using Microsoft.ML.Runtime;
    using Microsoft.ML.Trainers;
    using Microsoft.ML.Transforms;
    using SentimentAnalysis.Trainer.models;
    using System;
    using System.Collections.Generic;

    class Program
    {
        // Credit - Bag of words meets bag of popcorn - Kaggle dataset
        const string dataPath = "data/train.tsv";
        //TODO: implement test set of data
        const string testData = "data/test.tsv";
        const string trainedModelPath = @"data/SentimentAnalysisModel";

        static void Main(string[] args)
        {
            var pipeline = new LearningPipeline();

            var loader = new TextLoader(dataPath).CreateFrom<SentimentData>(useHeader: true, '\t');
            pipeline.Add(loader);

            pipeline.Add(new TextFeaturizer("Features", "SentimentText") {

                StopWordsRemover = new PredefinedStopWordsRemover(),
                KeepPunctuations = false,
                TextCase = TextNormalizerTransformCaseNormalizationMode.Lower,
                VectorNormalizer = TextTransformTextNormKind.L2
            });

            pipeline.Add(new StochasticDualCoordinateAscentBinaryClassifier() { NumThreads = 8, Shuffle = true, NormalizeFeatures = NormalizeOption.Yes });

            PredictionModel<SentimentData, SentimentPrediction> model = pipeline.Train<SentimentData, SentimentPrediction>();

            IEnumerable<SentimentData> sentiments = new[]
            {
                new SentimentData
                {
                    SentimentText = "I hated the movie."
                },
                new SentimentData
                {
                    SentimentText = "The movie was entertaining the whole time, i really enjoyed it."
                }
            };

            IEnumerable<SentimentPrediction> predictions = model.Predict(sentiments);

            foreach(var item in predictions)
            {
                Console.WriteLine($"Prediction: {(item.Sentiment ? "Positive" : "Negative")}");
            }

            var evulatorTrained = new BinaryClassificationEvaluator();
            BinaryClassificationMetrics metricsTrained = evulatorTrained.Evaluate(model, loader);

            Console.WriteLine("ACCURACY OF MODEL ON TRAINED DATA: " + metricsTrained.Accuracy);

            model.WriteAsync(trainedModelPath);

            Console.Read();
        }
    }
}
