using VisionCraft.Models.CVs;
using VisionCraft.Models.CVs.Exceptions;

namespace VisionCraft.Services.Foundations.CVs
{
    public partial class CVService
    {
        private static void ValidateCVNotNull(CV cv)
        {
            if (cv == null)
                throw new NullCVException();
        }
    }
}
