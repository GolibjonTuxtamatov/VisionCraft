using VisionCraft.Models.Teams.Exceptions;
using Xeptions;

namespace VisionCraft.Services.Orchestrations.TeamOrchestrationServices
{
    public partial class TeamOrchestrstionService
    {
        private delegate ValueTask<string> ReturningTokenFunction();

        private async ValueTask<string> TryCatch(ReturningTokenFunction function)
        {
            try
            {
                return await function();
            }
            catch (InvalidTeamException invalidTeamException)
            {
                throw CreateAndLogValidationException(invalidTeamException);
            }
            catch (NullTeamException nullTeamException)
            {
                throw CreateAndLogValidationException(nullTeamException);
            }
        }

        private Xeption CreateAndLogValidationException(Xeption exception)
        {
            var teamOrchestrationValidationException =
                new TeamOrchestrationValidationException(exception);

            this.loggingBroker.LogError(teamOrchestrationValidationException);

            return teamOrchestrationValidationException;
        }
    }
}
