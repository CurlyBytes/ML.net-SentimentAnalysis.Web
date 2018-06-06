namespace SentimentAnalysis.Web.Services
{
    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.DataContracts;
    using SentimentAnalysis.Web.Interfaces;
    using System;
    using System.Diagnostics;

    public class AppInsightsLoggerService : IAppInsightsLoggerService
    {
        private TelemetryClient _logger;
        public AppInsightsLoggerService()
        {
            this._logger = new TelemetryClient();
            _logger.InstrumentationKey = Environment.GetEnvironmentVariable("AppInsightsKey");
            _logger.Context.Cloud.RoleName = "SentimentAnalysis.Web";
        }

        public void LogInfo(string message)
        {
            try
            {
                _logger.TrackTrace(message, SeverityLevel.Information);
            }
            catch (Exception)
            {
                Trace.Write("Log info app insights call failed.");
            }
        }

        public void LogWarning(string message)
        {
            try
            {
                _logger.TrackTrace(message, SeverityLevel.Warning);
            }
            catch (Exception)
            {
                Trace.Write("Log warning app insights call failed.");
            }
        }

        public void LogError(string message)
        {
            try
            {
                _logger.TrackTrace(message, SeverityLevel.Error);
            }
            catch (Exception)
            {
                Trace.Write("Log error app insights call failed.");
            }
        }

        public void LogException(Exception exception)
        {
            try
            {
                _logger.TrackException(exception);
            }
            catch (Exception)
            {
                Trace.Write("Log Exception app insights call failed.");
            }
        }
    }
}
