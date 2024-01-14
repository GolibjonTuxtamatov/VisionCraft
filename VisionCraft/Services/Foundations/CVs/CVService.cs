using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Storages;
using VisionCraft.Models.CVs;

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

        public ValueTask<CV> AddCVAsync(CV cv) =>
            this.storageBroker.InsertCVAsync(cv);

    }
}
