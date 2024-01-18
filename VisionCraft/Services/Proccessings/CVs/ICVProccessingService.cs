using VisionCraft.Models.CVs;

namespace VisionCraft.Services.Proccessings.CVs
{
    public interface ICVProccessingService
    {
        ValueTask<CV> ProcAddCVAsync(CV cv);
        IQueryable<CV> ProcRetrieveAllCVs();
        ValueTask<CV> ProcRetrieveCvByIdAsync(Guid id);
        ValueTask<CV> ProcRemoveCVAsync(Guid id);
    }
}
