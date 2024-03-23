using VisionCraft.Models.Teams;

namespace VisionCraft.Services.Orchestrations.TeamOrchestrationServices
{
    public interface ITeamOrchestrstionService
    {
        ValueTask<Team> AddTeamAsync(Team team);

        ValueTask<string> GetTokenAsync(string email, string password);
    }
}
