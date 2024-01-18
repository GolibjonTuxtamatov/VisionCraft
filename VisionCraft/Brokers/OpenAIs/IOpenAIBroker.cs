using OpenAI_API.Chat;

namespace VisionCraft.Brokers.OpenAIs
{
    public interface IOpenAIBroker
    {
        ValueTask<ChatResult> EvaluateExtraCVAsync(string[] requests);
    }
}
