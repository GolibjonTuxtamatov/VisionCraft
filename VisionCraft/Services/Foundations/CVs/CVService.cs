using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Storages;
using VisionCraft.Models.CVs;

namespace VisionCraft.Services.Foundations.CVs
{
    public partial class CVService : ICVService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public CVService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<CV> AddCVAsync(CV cv) =>
            TryCatch(async () =>
            {
                ValidateOnAdd(cv);

                return await this.storageBroker.InsertCVAsync(cv);
            });

        public IQueryable<CV> RetrieveAllCVs() =>
            this.storageBroker.SelectAllCVs();

        public async ValueTask<CV> RetrieveCvByIdAsync(Guid id) =>
            await this.storageBroker.SelectCVByIdAsync(id);

        public async ValueTask<CV> RemoveCVAsync(Guid id)
        {
            CV foundCV = await this.storageBroker.SelectCVByIdAsync(id);

            return await this.storageBroker.DeleteCVAsync(foundCV);
        }
    }
}
