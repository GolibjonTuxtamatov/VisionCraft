namespace VisionCraft.Services.Orchestrations.TeamOrchestrationServices
{
    public interface ITeamOrchestrstionService
    {
        ValueTask<string> GetTokenAsync(string email, string password);
    }
}
