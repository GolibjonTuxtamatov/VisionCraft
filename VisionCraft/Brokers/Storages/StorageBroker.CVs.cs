using Microsoft.EntityFrameworkCore;
using VisionCraft.Models.CVs;

namespace VisionCraft.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<CV> CVs { get; set; }


        public ValueTask<CV> InsertCVAsync(CV cv) =>
            InsertAsync(cv);

        public IQueryable<CV> SelectAllCVs() =>
            SelectAll<CV>();
    }
}
