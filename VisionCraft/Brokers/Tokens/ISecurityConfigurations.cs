using VisionCraft.Models.Teams;

namespace VisionCraft.Brokers.Tokens
{
    public interface ISecurityConfigurations
    {
        public ValueTask<string> CreateToken(Team team);
    }
}
