using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Tokens;
using VisionCraft.Models.Teams;

namespace VisionCraft.Services.Foundations.Tokens
{
    public class SecurityService : ISecurityService
    {
        private readonly ISecurityConfigurations teamSecurityConfigurations;
        private readonly ILoggingBroker loggingBroker;

        public SecurityService(ISecurityConfigurations teamSecurityConfigurations,ILoggingBroker loggingBroker)
        {
            this.teamSecurityConfigurations = teamSecurityConfigurations;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<string> CreateTokenAsync(Team team) =>
            await this.teamSecurityConfigurations.CreateToken(team);
    }
}
