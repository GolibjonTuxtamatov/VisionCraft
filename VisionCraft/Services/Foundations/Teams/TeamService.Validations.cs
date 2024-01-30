using VisionCraft.Models.Teams;
using VisionCraft.Models.Teams.Exceptions;

namespace VisionCraft.Services.Foundations.Teams
{
    public partial class TeamService
    {
        private static void ValidateNotNull(Team team)
        {
            if (team == null)
                throw new NullTeamException();
        }
    }
}
