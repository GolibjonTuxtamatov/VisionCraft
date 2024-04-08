using Xeptions;

namespace VisionCraft.Models.Teams.Exceptions
{
    public class InvalidTeamException : Xeption
    {
        public InvalidTeamException()
            : base("Team is invalid.")
        { }
    }
}
