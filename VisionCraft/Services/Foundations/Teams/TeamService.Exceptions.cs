using Microsoft.Data.SqlClient;
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
            catch(SqlException sqlException)
            {
                var failedTeamStorageException =
                    new FailedTeamStorageException(sqlException);

                throw CreateAndLogDependecyException(failedTeamStorageException);
            }
        }

        private TeamValidationException CreateAndLogValidationException(Xeption exception)
        {

            var teamValidationException =
                new TeamValidationException(exception);

            this.loggingBroker.LogError(teamValidationException);

            return teamValidationException;
        }

        private TeamDependencyException CreateAndLogDependecyException(Xeption exception)
        {
            var teamDependencyException = new TeamDependencyException(exception);
            this.loggingBroker.LogCritical(teamDependencyException);

            return teamDependencyException;
        }
    }
}
