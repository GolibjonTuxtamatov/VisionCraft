using Xeptions;

namespace VisionCraft.Models.Teams.Exceptions
{
    public class TeamOrchestrationValidationException : Xeption
    {
        public TeamOrchestrationValidationException(Xeption innerException)
            :base("Team orchestration validation error occured, fix the error and try again",innerException)
        { }
    }
}
