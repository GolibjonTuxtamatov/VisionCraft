using VisionCraft.Models.CVs;

namespace VisionCraft.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<CV> InsertCVAsync(CV cv);
        IQueryable<CV> SelectAllCVs();
        ValueTask<CV> SelectCVByIdAsync(Guid id);
        ValueTask<CV> DeleteCVAsync(CV cv);
    }
}
