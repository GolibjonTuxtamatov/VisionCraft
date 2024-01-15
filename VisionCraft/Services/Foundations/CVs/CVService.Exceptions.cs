using VisionCraft.Models.CVs;
using VisionCraft.Models.CVs.Exceptions;
using Xeptions;

namespace VisionCraft.Services.Foundations.CVs
{
    public partial class CVService
    {
        private delegate ValueTask<CV> ReturningCVFunction();

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
        }

        private CVValidationException CreateAndLogValidationException(Xeption exception)
        {
            var cvValidationException = new CVValidationException(exception);

            this.loggingBroker.LogError(cvValidationException);

            return cvValidationException;
        }
    }
}
