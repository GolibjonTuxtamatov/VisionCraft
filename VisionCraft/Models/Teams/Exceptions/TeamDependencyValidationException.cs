using Xeptions;

namespace VisionCraft.Models.Teams.Exceptions
{
    public class TeamDependencyValidationException : Xeption
    {
        public TeamDependencyValidationException(Xeption innerException)
            : base("Team dependency validation error occure,try again.",
                 innerException)
        { }
    }
}
