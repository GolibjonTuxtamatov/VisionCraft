using VisionCraft.Models.CVs;

namespace VisionCraft.Services.Foundations.CVs
{
    public interface ICVService
    {
        ValueTask<CV> AddCVAsync(CV cv);
    }
}
