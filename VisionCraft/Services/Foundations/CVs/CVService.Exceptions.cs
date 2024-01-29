using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using VisionCraft.Models.CVs;
using VisionCraft.Models.CVs.Exceptions;
using Xeptions;

namespace VisionCraft.Services.Foundations.CVs
{
    public partial class CVService
    {
        private delegate ValueTask<CV> ReturningCVFunction();
        private delegate IQueryable<CV> ReturningCVsFunction();

        private async ValueTask<CV> TryCatch(ReturningCVFunction function)
        {
            try
            {
                return await function();
            }
            catch (NullCVException nullCVException)
            {
                throw CreateAndLogValidationException(nullCVException);
            }
            catch (InvalidCVException invalidCVException)
            {
                throw CreateAndLogValidationException(invalidCVException);
            }
            catch (SqlException sqlException)
            {
                var failedStorageCVException =
                    new FailedStorageCVException(sqlException);

                throw CreateAndLogCriticalException(failedStorageCVException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadExistCVException =
                    new AlreadyExistCVException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadExistCVException);
            }
            catch (Exception serviceException)
            {
                var failedServiceException = new FailedServiceException(serviceException);

                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private IQueryable<CV> TryCatch(ReturningCVsFunction returningCVsFunction)
        {
            return returningCVsFunction();
        }

        private CVValidationException CreateAndLogValidationException(Xeption exception)
        {
            var cvValidationException = new CVValidationException(exception);

            this.loggingBroker.LogError(cvValidationException);

            return cvValidationException;
        }

        private CVDependencyException CreateAndLogCriticalException(Xeption exception)
        {
            var cVDependencyException = new CVDependencyException(exception);

            this.loggingBroker.LogCritical(cVDependencyException);

            return cVDependencyException;
        }

        private CVDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var cvDependencyValidationException = new CVDependencyValidationException(exception);

            this.loggingBroker.LogError(cvDependencyValidationException);

            return cvDependencyValidationException;
        }

        private CVServiceException CreateAndLogServiceException(Xeption exception)
        {
            var cvServiceException = new CVServiceException(exception);
            this.loggingBroker.LogError(cvServiceException);

            return cvServiceException;
        }
    }
}
