using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Storages;
using VisionCraft.Models.CVs;
using VisionCraft.Models.CVs.Exceptions;

namespace VisionCraft.Services.Foundations.CVs
{
    public class CVService : ICVService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public CVService(IStorageBroker storageBroker,ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;                                                                                                                             
        }

        public async ValueTask<CV> AddCVAsync(CV cv)
        {
            try
            {
                if (cv == null)
                    throw new NullCVException();

                return await this.storageBroker.InsertCVAsync(cv);
            }
            catch (NullCVException nullCVException)
            {
                var cvValidationException = new CVValidationException(nullCVException);

                this.loggingBroker.LogError(cvValidationException);

                throw cvValidationException;
            }
        }

    }
}
