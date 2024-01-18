using VisionCraft.Models.Teams;

namespace VisionCraft.Services.Foundations.Teams
{
    public interface ITeamService
    {
        ValueTask<Team> AddTeamAsync(Team team);
        IQueryable<Team> RetrieveAllTeams();
        ValueTask<Team> RetrieveTeamByIdAsync(Guid id);
        ValueTask<Team> ModifyTeamAsync(Team team);
        ValueTask<Team> RemoveTeamAsync(Guid id);
    }
}
