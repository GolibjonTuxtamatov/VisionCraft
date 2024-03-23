using VisionCraft.Models.Teams;

namespace VisionCraft.Services.Foundations.Tokens
{
    public interface ITeamSecurityService
    {
        ValueTask<string> CreateTokenAsync(Team team);
    }
}
