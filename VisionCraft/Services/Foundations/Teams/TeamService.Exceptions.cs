using VisionCraft.Models.Teams;
using VisionCraft.Models.Teams.Exceptions;
using Xeptions;

namespace VisionCraft.Services.Foundations.Teams
{
    public partial class TeamService
    {
        private delegate ValueTask<Team> ReturningTeamFunction();

        private ValueTask<Team> TryCatch(ReturningTeamFunction returningTeamFunction)
        {
            try
            {
                return returningTeamFunction();
            }
            catch (NullTeamException nullTeamException)
            {
                throw CreateAndLogValidationException(nullTeamException);
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
