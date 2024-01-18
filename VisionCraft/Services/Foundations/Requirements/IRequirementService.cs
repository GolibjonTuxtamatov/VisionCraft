using VisionCraft.Models.Requirements;

namespace VisionCraft.Services.Foundations.Requirements
{
    public interface IRequirementService
    {
        ValueTask<Requirement> AddRequirementAsync(Requirement requirement);
        IQueryable<Requirement> RetrieveAllRequirements();
        ValueTask<Requirement> RetrieveRequirementByIdAsync(Guid id);
        ValueTask<Requirement> ModifyRequirementAsync(Requirement requirement);
        ValueTask<Requirement> RemoveRequirementAsync(Guid id);
    }
}
