using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using VisionCraft.Models.Teams;
using VisionCraft.Services.Foundations.Teams;

namespace VisionCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : RESTFulController
    {
        private readonly ITeamService teamService;

        public AccountsController(ITeamService teamService) =>
            this.teamService = teamService;

        [HttpPost("register")]
        public async ValueTask<ActionResult<Team>> RegisterTeamAsync(Team team) =>
            Created(await this.teamService.AddTeamAsync(team));

        [HttpPost("login")]
        public async ValueTask<IActionResult> LogIn() =>
            throw new NotImplementedException();
    }
}
