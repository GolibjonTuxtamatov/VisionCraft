using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Tokens;
using VisionCraft.Models.Teams;

namespace VisionCraft.Services.Foundations.Tokens
{
    public class TeamSecurityService : ITeamSecurityService
    {
        private readonly ITeamSecurityConfigurations teamSecurityConfigurations;
        private readonly ILoggingBroker loggingBroker;

        public TeamSecurityService(ITeamSecurityConfigurations teamSecurityConfigurations,ILoggingBroker loggingBroker)
        {
            this.teamSecurityConfigurations = teamSecurityConfigurations;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<string> CreateTokenAsync(Team team) =>
            await this.teamSecurityConfigurations.CreateTeamToken(team);
    }
}
