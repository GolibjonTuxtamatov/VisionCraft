using Xeptions;

namespace VisionCraft.Models.Teams.Exceptions
{
    public class AlreadyExistsTeamException : Xeption
    {
        public AlreadyExistsTeamException(Exception innerException)
            : base("Team already exist.", innerException)
        { }
    }
}
