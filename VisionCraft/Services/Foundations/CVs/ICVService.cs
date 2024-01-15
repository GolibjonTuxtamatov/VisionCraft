using VisionCraft.Models.CVs;

namespace VisionCraft.Services.Foundations.CVs
{
    public interface ICVService
    {
        ValueTask<CV> AddCVAsync(CV cv);
        IQueryable<CV> RetrieveAllCVs();
        ValueTask<CV> RetrieveCvByIdAsync(Guid id);
        ValueTask<CV> RemoveCVAsync(Guid id);
    }
}
