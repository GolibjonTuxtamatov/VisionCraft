using OpenAI_API.Chat;
using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.OpenAIs;

namespace VisionCraft.Services.Foundations.OpenAIs
{
    public class OpenAIService : IOpenAIService
    {

        private readonly IOpenAIBroker openAIBroker;
        private readonly ILoggingBroker loggingBroker;

        public OpenAIService(IOpenAIBroker openAIBroker, ILoggingBroker loggingBroker)
        {
            this.openAIBroker = openAIBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<string> EvaluateExtracCVAsync(string[] requests)
        {
            ChatResult result = await this.openAIBroker.EvaluateExtraCVAsync(requests);

            return result.Choices[0].ToString();
        }
    }
}
