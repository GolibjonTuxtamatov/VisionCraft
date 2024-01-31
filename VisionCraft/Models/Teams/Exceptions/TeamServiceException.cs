using Xeptions;

namespace VisionCraft.Models.Teams.Exceptions
{
    public class TeamServiceException : Xeption
    {
        public TeamServiceException(Xeption innerException)
            :base("Team service error occured, contact support.",
                 innerException)
        { }
    }
}
