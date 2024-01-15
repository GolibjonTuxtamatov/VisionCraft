using VisionCraft.Models.CVs;
using VisionCraft.Services.Foundations.CVs;

namespace VisionCraft.Services.Proccessings.CVs
{
    public class ProccessingCVService : IProccessingCVService
    {
        private readonly ICVService cVService;

        public ProccessingCVService(ICVService cVService)
        {
            this.cVService = cVService;
        }

        public async ValueTask<CV> ProcAddCVAsync(CV cv) =>
            await this.cVService.AddCVAsync(cv);

        public IQueryable<CV> ProcRetrieveAllCVs() =>
            this.cVService.RetrieveAllCVs();

        public async ValueTask<CV> ProcRetrieveCvByIdAsync(Guid id) =>
            await this.cVService.RetrieveCvByIdAsync(id);

        public async ValueTask<CV> ProcRemoveCVAsync(Guid id) =>
            await this.cVService.RemoveCVAsync(id);
    }
}
