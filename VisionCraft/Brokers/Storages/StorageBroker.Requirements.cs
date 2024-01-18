using Microsoft.EntityFrameworkCore;
using VisionCraft.Models.Requirements;

namespace VisionCraft.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Requirement> Requirements { get; set; }

        public async ValueTask<Requirement> InsertRequirementAsync(Requirement requirement) =>
            await InsertAsync(requirement);

        public IQueryable<Requirement> SelectAllRequirements() =>
            SelectAll<Requirement>();

        public async ValueTask<Requirement> SelectRequirementByIdAsync(Guid id) =>
            await SelectAsync<Requirement>(id);

        public async ValueTask<Requirement> UpdateRequirementAsync(Requirement requirement) =>
            await UpdateAsync(requirement);

        public async ValueTask<Requirement> DeleteRequirementAsync(Requirement requirement) =>
            await DeleteAsync(requirement);
    }
}
