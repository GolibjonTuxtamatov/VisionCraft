using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Storages;
using VisionCraft.Models.Requirements;

namespace VisionCraft.Services.Foundations.Requirements
{
    public class RequirementService : IRequirementService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public RequirementService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Requirement> AddRequirementAsync(Requirement requirement) =>
            await this.storageBroker.InsertRequirementAsync(requirement);

        public IQueryable<Requirement> RetrieveAllRequirements() =>
            this.storageBroker.SelectAllRequirements();

        public async ValueTask<Requirement> RetrieveRequirementByIdAsync(Guid id) =>
            await this.storageBroker.SelectRequirementByIdAsync(id);

        public async ValueTask<Requirement> ModifyRequirementAsync(Requirement requirement) =>
            await this.storageBroker.UpdateRequirementAsync(requirement);

        public async ValueTask<Requirement> RemoveRequirementAsync(Guid id)
        {
            Requirement foundRequirement =
                await this.storageBroker.SelectRequirementByIdAsync(id);

            return await this.storageBroker.DeleteRequirementAsync(foundRequirement);
        }
    }
}
