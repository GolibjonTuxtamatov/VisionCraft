using Xeptions;

namespace VisionCraft.Models.Teams.Exceptions
{
    public class TeamValidationException : Xeption
    {
        public TeamValidationException(Xeption innerException)
            : base("Team validation error occured, fix the error and try again.",
                 innerException)
        { }
    }
}
