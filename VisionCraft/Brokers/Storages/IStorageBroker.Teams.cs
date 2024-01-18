using VisionCraft.Models.Teams;

namespace VisionCraft.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Team> InsertTeamAsync(Team team);
        IQueryable<Team> SelectAllTeams();
        ValueTask<Team> SelectTeamByIdAsync(Guid id);
        ValueTask<Team> UpdateTeamAsync(Team team);
        ValueTask<Team> DeleteTeamAsync(Team team);
    }
}
