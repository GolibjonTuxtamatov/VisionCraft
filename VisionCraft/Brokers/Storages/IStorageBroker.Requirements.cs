using VisionCraft.Models.Requirements;

namespace VisionCraft.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Requirement> InsertRequirementAsync(Requirement requirement);
        IQueryable<Requirement> SelectAllRequirements();
        ValueTask<Requirement> SelectRequirementByIdAsync(Guid id);
        ValueTask<Requirement> UpdateRequirementAsync(Requirement requirement);
        ValueTask<Requirement> DeleteRequirementAsync(Requirement requirement);
    }
}
