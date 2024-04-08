
using VisionCraft.Brokers.Loggings;
using VisionCraft.Models.Teams;
using VisionCraft.Services.Foundations.Teams;
using VisionCraft.Services.Foundations.Tokens;

namespace VisionCraft.Services.Orchestrations.TeamOrchestrationServices
{
    public class TeamOrchestrstionService : ITeamOrchestrstionService
    {
        private readonly ISecurityService securityService;
        private readonly ITeamService teamService;
        private readonly ILoggingBroker loggingBroker;

        public TeamOrchestrstionService(
            ISecurityService securityService,
            ITeamService teamService,
            ILoggingBroker loggingBroker)
        {
            this.securityService = securityService;
            this.teamService = teamService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Team> AddTeamAsync(Team team) =>
            await this.teamService.AddTeamAsync(team);

        public async ValueTask<string> GetTokenAsync(string email, string password)
        {
            Team maybeTeam = GetTeamByEmailAndPassword(email, password);

            string token = await this.securityService.CreateTokenAsync(maybeTeam);

            return token;
        }

        private Team GetTeamByEmailAndPassword(string email, string password)
        {
            IQueryable<Team> allTeams = this.teamService.RetrieveAllTeams();

            return allTeams.FirstOrDefault(retrivedTeam =>
                retrivedTeam.Email.Equals(email) && retrivedTeam.Password.Equals(password));
        }
    }
}
