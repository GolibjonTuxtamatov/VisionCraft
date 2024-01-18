namespace VisionCraft.Services.Proccessings.OpenAIs
{
    public interface IOpenAIProccessingService
    {
        ValueTask<string> ProcEvaluateCVAsync(string[] requests);
    }
}
