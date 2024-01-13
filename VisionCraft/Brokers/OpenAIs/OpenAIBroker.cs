using System.Reflection.PortableExecutable;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Completions;
using OpenAI_API.Models;
using VisionCraft.Models.OpenAIs;
using VisionCraft.Models.Requirements;

namespace VisionCraft.Brokers.OpenAIs
{
    public class OpenAIBroker : IOpenAIBroker
    {
        private readonly IConfiguration configuration;
        private readonly OpenAIConfiguration openAIConfiguration;


        public OpenAIBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.openAIConfiguration = new OpenAIConfiguration();
            this.configuration.Bind("OpenAIConfiguration", openAIConfiguration);
        }


        public async ValueTask<ChatResult> EvaluateExtraCVAsync(Requirement requierment)
        {
            var api = new OpenAIAPI(openAIConfiguration.SecretKey);

            ChatResult result = await api.Chat.CreateChatCompletionAsync(requierment.Requirements);

            return result;
        }
    }
}
