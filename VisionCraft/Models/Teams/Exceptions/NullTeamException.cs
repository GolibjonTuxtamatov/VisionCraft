using Xeptions;

namespace VisionCraft.Models.Teams.Exceptions
{
    public class NullTeamException : Xeption
    {
        public NullTeamException()
            : base("Team is null.")
        { }
    }
}