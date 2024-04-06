using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RESTFulSense.Controllers;
using VisionCraft.Models.CVs;
using VisionCraft.Services.Foundations.CVs;

namespace VisionCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CVsController : RESTFulController
    {
        private readonly ICVService cVService;

        public CVsController(ICVService cVService) =>
            this.cVService = cVService;

        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<CV>> GetAllCVs() =>
            Ok(this.cVService.RetrieveAllCVs());

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<CV>> SelectCVByIdAsync(Guid id) =>
            Ok(await this.cVService.RetrieveCvByIdAsync(id));

        [HttpDelete]
        public async ValueTask<ActionResult<CV>> DeleteCVAsync(Guid id) =>
            Ok(await this.cVService.RemoveCVAsync(id));
    }
}
