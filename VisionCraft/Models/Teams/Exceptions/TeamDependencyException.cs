using Xeptions;

namespace VisionCraft.Models.Teams.Exceptions
{
    public class TeamDependencyException : Xeption
    {
        public TeamDependencyException(Xeption innerException)
            : base("Team dependency error occured, contact support.", innerException)
        { }
    }
}
