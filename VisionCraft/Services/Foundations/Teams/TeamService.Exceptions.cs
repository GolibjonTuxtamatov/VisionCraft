using VisionCraft.Models.Teams;
using VisionCraft.Models.Teams.Exceptions;
using Xeptions;

namespace VisionCraft.Services.Foundations.Teams
{
    public partial class TeamService
    {
        private delegate ValueTask<Team> ReturningTeamFunction();

        private async ValueTask<Team> TryCatch(ReturningTeamFunction returningTeamFunction)
        {
            try
            {
                return await returningTeamFunction();
            }
            catch (NullTeamException nullTeamException)
            {
                throw CreateAndLogValidationException(nullTeamException);
            }
            catch (InvalidTeamException invalidTeamException)
            {
                throw CreateAndLogValidationException(invalidTeamException);
            }
        }

        private TeamValidationException CreateAndLogValidationException(Xeption exception)
        {

            var teamValidationException =
                new TeamValidationException(exception);

            this.loggingBroker.LogError(teamValidationException);

            return teamValidationException;
        }
    }
}
