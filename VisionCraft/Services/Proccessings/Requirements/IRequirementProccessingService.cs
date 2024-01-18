using VisionCraft.Models.Requirements;

namespace VisionCraft.Services.Proccessings.Requirements
{
    public interface IRequirementProccessingService
    {
        ValueTask<Requirement> ProcAddRequirementAsync(Requirement requirement);
        IQueryable<Requirement> ProcRetrieveAllRequirements();
        ValueTask<Requirement> ProcRetrieveRequirementByIdAsync(Guid id);
        ValueTask<Requirement> ProcModifyRequirementAsync(Requirement requirement);
        ValueTask<Requirement> ProcRemoveRequirementAsync(Guid id);
    }
}
