using OpenAI_API.Chat;
using VisionCraft.Models.Requirements;

namespace VisionCraft.Brokers.OpenAIs
{
    public interface IOpenAIBroker
    {
        ValueTask<ChatResult> EvaluateExtraCVAsync(Requirement requierment);
    }
}
