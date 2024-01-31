using EFxceptions.Models.Exceptions;
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
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsTeamException =
                    new AlreadyExistsTeamException(duplicateKeyException);

                throw CreateAndLogDependecyValidationException(alreadyExistsTeamException);
            }
            catch (Exception exception)
            {
                var failedTeamServiceException = new FailedTeamServiceException(exception);

                throw CreateAndLogServiceException(failedTeamServiceException);
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

        private TeamDependencyValidationException CreateAndLogDependecyValidationException(Xeption exception)
        {
            var teamDependencyValidationException = new TeamDependencyValidationException(exception);
            this.loggingBroker.LogError(teamDependencyValidationException);
            
            return teamDependencyValidationException;
        }

        private TeamServiceException CreateAndLogServiceException(Xeption exception)
        {
            var teamServiceException = new TeamServiceException(exception);
            this.loggingBroker.LogError(teamServiceException);

            return teamServiceException;
        }
    }
}
