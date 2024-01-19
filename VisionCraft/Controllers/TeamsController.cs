using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RESTFulSense.Controllers;
using VisionCraft.Models.Teams;
using VisionCraft.Services.Foundations.Teams;

namespace VisionCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : RESTFulController
    {
        private readonly ITeamService teamService;

        public TeamsController(ITeamService teamService) =>
            this.teamService = teamService;

        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Team>> GetAllTeams() =>
            Ok(this.teamService.RetrieveAllTeams());

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<Team>> GetTeamByIdAsync(Guid id) =>
            Ok(await this.teamService.RetrieveTeamByIdAsync(id));

        [HttpPut]
        public async ValueTask<ActionResult<Team>> PutTeamAsync(Team team) =>
            Ok(await this.teamService.ModifyTeamAsync(team));

        [HttpDelete]
        public async ValueTask<ActionResult<Team>> DeleteTeamAsync(Guid id) =>
            Ok(await this.teamService.RemoveTeamAsync(id));
    }
}
