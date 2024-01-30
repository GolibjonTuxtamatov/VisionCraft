using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Storages;
using VisionCraft.Models.Teams;
using VisionCraft.Models.Teams.Exceptions;

namespace VisionCraft.Services.Foundations.Teams
{
    public class TeamService : ITeamService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public TeamService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Team> AddTeamAsync(Team team)
        {
            try
            {

                if (team == null)
                    throw new NullTeamException();

                return await this.storageBroker.InsertTeamAsync(team);

            }
            catch (NullTeamException nullTeamException)
            {
                var teamValidationException =
                    new TeamValidationException(nullTeamException);

                this.loggingBroker.LogError(teamValidationException);

                throw teamValidationException;
            }

        }

        public IQueryable<Team> RetrieveAllTeams() =>
            this.storageBroker.SelectAllTeams();

        public async ValueTask<Team> RetrieveTeamByIdAsync(Guid id) =>
            await this.storageBroker.SelectTeamByIdAsync(id);

        public async ValueTask<Team> ModifyTeamAsync(Team team) =>
            await this.storageBroker.UpdateTeamAsync(team);

        public async ValueTask<Team> RemoveTeamAsync(Guid id)
        {
            Team foundTeam = await this.storageBroker.SelectTeamByIdAsync(id);

            return await this.storageBroker.DeleteTeamAsync(foundTeam);
        }
    }
}
