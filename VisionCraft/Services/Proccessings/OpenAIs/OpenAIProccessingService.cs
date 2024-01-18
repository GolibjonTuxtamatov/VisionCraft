using VisionCraft.Services.Foundations.OpenAIs;

namespace VisionCraft.Services.Proccessings.OpenAIs
{
    public class OpenAIProccessingService : IOpenAIProccessingService
    {
        private readonly IOpenAIService openAIService;

        public OpenAIProccessingService(IOpenAIService openAIService) =>
            this.openAIService = openAIService;

        public async ValueTask<string> ProcEvaluateCVAsync(string[] requests) =>
            await this.openAIService.EvaluateExtracCVAsync(requests);
    }
}
