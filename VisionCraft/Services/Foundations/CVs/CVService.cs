using Microsoft.Data.SqlClient;
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

        public IQueryable<CV> RetrieveAllCVs()
        {
            try
            {
                return this.storageBroker.SelectAllCVs();
            }
            catch (SqlException sqlException)
            {
                var failedStorageCVException =
                    new FailedStorageCVException(sqlException);

                var cVDependencyException =
                    new CVDependencyException(failedStorageCVException);

                this.loggingBroker.LogCritical(cVDependencyException);

                throw cVDependencyException;
            }
        }

        public async ValueTask<CV> RetrieveCvByIdAsync(Guid id) =>
            await this.storageBroker.SelectCVByIdAsync(id);

        public async ValueTask<CV> RemoveCVAsync(Guid id)
        {
            CV foundCV = await this.storageBroker.SelectCVByIdAsync(id);

            DeleteStaticCv(foundCV);

            return await this.storageBroker.DeleteCVAsync(foundCV);
        }

        private static void DeleteStaticCv(CV foundCV)
        {
            var fileInfo = new FileInfo(foundCV.CVPath);

            if (fileInfo.Exists)
                fileInfo.Delete();
        }
    }
}
