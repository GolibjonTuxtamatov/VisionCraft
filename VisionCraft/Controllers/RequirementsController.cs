using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RESTFulSense.Controllers;
using VisionCraft.Models.Requirements;
using VisionCraft.Services.Foundations.Requirements;

namespace VisionCraft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RequirementsController : RESTFulController
    {
        private readonly IRequirementService requirementService;

        public RequirementsController(IRequirementService requirementService) =>
            this.requirementService = requirementService;

        [HttpPost]
        public async ValueTask<ActionResult<Requirement>> PostRequirementAsync(Requirement requirement) =>
            Created(await this.requirementService.AddRequirementAsync(requirement));

        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Requirement>> GetAllRequirements() =>
            Ok(this.requirementService.RetrieveAllRequirements());

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<Requirement>> GetRequirementByIdAsync(Guid id) =>
            Ok(await this.requirementService.RetrieveRequirementByIdAsync(id));

        [HttpPut]
        public async ValueTask<ActionResult<Requirement>> PutRequirementAsync(Requirement requirement) =>
            Ok(await this.requirementService.ModifyRequirementAsync(requirement));

        [HttpDelete]
        public async ValueTask<ActionResult<Requirement>> DeleteRequirementAsync(Guid id) =>
            Ok(await this.requirementService.RemoveRequirementAsync(id));
    }
}
