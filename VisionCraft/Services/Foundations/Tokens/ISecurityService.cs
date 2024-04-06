using VisionCraft.Models.Teams;

namespace VisionCraft.Services.Foundations.Tokens
{
    public interface ISecurityService
    {
        ValueTask<string> CreateTokenAsync(Team team);
    }
}
