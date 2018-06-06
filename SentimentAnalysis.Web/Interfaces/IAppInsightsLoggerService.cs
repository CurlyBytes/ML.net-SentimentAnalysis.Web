using System;

namespace SentimentAnalysis.Web.Interfaces
{
    public interface IAppInsightsLoggerService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogException(Exception exception);
    }
}
