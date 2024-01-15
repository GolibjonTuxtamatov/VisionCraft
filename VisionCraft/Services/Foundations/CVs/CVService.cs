using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Storages;
using VisionCraft.Models.CVs;
using VisionCraft.Models.CVs.Exceptions;

namespace VisionCraft.Services.Foundations.CVs
{
    public partial class CVService : ICVService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public CVService(IStorageBroker storageBroker,ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;                                                                                                                             
        }

        public ValueTask<CV> AddCVAsync(CV cv) =>
            TryCatch(async () =>
            {
                ValidateOnAdd(cv);

                return  await this.storageBroker.InsertCVAsync(cv);
            });

    }
}
