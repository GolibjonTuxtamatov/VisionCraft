using VisionCraft.Brokers.Storages;
using VisionCraft.Models.CVs;

namespace VisionCraft.Services.Foundations.CVs
{
    public class CVService : ICVService
    {
        private readonly IStorageBroker storageBroker;

        public CVService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public ValueTask<CV> AddCVAsync(CV cv) =>
            this.storageBroker.InsertCVAsync(cv);

    }
}
