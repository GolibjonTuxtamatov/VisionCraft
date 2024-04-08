using Xeptions;

namespace VisionCraft.Models.Teams.Exceptions
{
    public class FailedTeamServiceException : Xeption
    {
        public FailedTeamServiceException(Exception innerException)
            : base("Failed team service error occured,contact support.",
                 innerException)
        { }
    }
}
