using Microsoft.EntityFrameworkCore;
using VisionCraft.Models.CVs;

namespace VisionCraft.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<CV> CVs { get; set; }


        public async ValueTask<CV> InsertCVAsync(CV cv) =>
            await InsertAsync(cv);

        public IQueryable<CV> SelectAllCVs() =>
            SelectAll<CV>();

        public async ValueTask<CV> SelectCVByIdAsync(Guid id) =>
            await SelectAsync<CV>(id);

        public async ValueTask<CV> DeleteCVAsync(CV cv) =>
            await DeleteAsync(cv);
    }
}
