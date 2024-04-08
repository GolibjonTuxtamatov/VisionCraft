using Xeptions;

namespace VisionCraft.Models.Teams.Exceptions
{
    public class FailedTeamStorageException : Xeption
    {
        public FailedTeamStorageException(Exception innerException)
            : base("Failed storage team error occured.", innerException)
        { }
    }
}
