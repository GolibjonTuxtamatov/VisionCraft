namespace VisionCraft.Services.Foundations.OpenAIs
{
    public interface IOpenAIService
    {
        ValueTask<string> EvaluateExtracCVAsync(string[] requests);
    }
}
