
namespace VisionCraft.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> loggingBroker)
        {
            this.logger = loggingBroker;
        }

        public void LogError(Exception exception) =>
            this.logger.LogError(exception,exception.Message);

        public void LogCritical(Exception exception) =>
            this.logger.LogCritical(exception,exception.Message);
    }
}
