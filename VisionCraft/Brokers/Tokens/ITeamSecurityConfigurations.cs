using VisionCraft.Models.Teams;

namespace VisionCraft.Brokers.Tokens
{
    public interface ITeamSecurityConfigurations
    {
        public ValueTask<string> CreateTeamToken(Team team);
    }
}
