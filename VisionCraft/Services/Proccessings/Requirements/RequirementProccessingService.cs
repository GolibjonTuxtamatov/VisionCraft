using VisionCraft.Models.Requirements;
using VisionCraft.Services.Foundations.Requirements;

namespace VisionCraft.Services.Proccessings.Requirements
{
    public class RequirementProccessingService : IRequirementProccessingService
    {
        private readonly IRequirementService requirementService;

        public RequirementProccessingService(IRequirementService requirementService) =>
            this.requirementService = requirementService;

        public async ValueTask<Requirement> ProcAddRequirementAsync(Requirement requirement) =>
            await this.requirementService.AddRequirementAsync(requirement);

        public IQueryable<Requirement> ProcRetrieveAllRequirements() =>
            this.requirementService.RetrieveAllRequirements();

        public async ValueTask<Requirement> ProcRetrieveRequirementByIdAsync(Guid id) =>
            await this.requirementService.RetrieveRequirementByIdAsync(id);

        public async ValueTask<Requirement> ProcModifyRequirementAsync(Requirement requirement) =>
            await this.requirementService.ModifyRequirementAsync(requirement);

        public async ValueTask<Requirement> ProcRemoveRequirementAsync(Guid id) =>
            await this.requirementService.RemoveRequirementAsync(id);
    }
}
