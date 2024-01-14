using VisionCraft.Models.CVs;

namespace VisionCraft.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<CV> InsertCVAsync(CV cv);
    }
}
