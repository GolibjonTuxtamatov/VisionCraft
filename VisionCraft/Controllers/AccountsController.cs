using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using VisionCraft.Models.Teams;
using VisionCraft.Services.Foundations.Teams;
using VisionCraft.Services.Orchestrations.TeamOrchestrationServices;

namespace VisionCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : RESTFulController
    {
        private readonly ITeamOrchestrstionService teamOrchestrstionService;

        public AccountsController(ITeamOrchestrstionService teamOrchestrstionService) =>
            this.teamOrchestrstionService = teamOrchestrstionService;

        [HttpPost("register")]
        public async ValueTask<ActionResult<Team>> RegisterTeamAsync(Team team) =>
            Created(await this.teamOrchestrstionService.AddTeamAsync(team));

        [HttpPost("login")]
        public async ValueTask<ActionResult<object>> LogIn(string email,string password)
        {

            string token = await this.teamOrchestrstionService.GetTokenAsync(email, password);

            return Ok(new {token});
        }
    }
}
