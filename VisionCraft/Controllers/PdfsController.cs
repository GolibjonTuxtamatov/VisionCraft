using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using VisionCraft.Services.Orchestrations.CVOrchestrationService;

namespace VisionCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfsController : RESTFulController
    {
        private readonly ICVOrchestrationService cVOrchestrationService;

        public PdfsController(ICVOrchestrationService cVOrchestrationService) =>
            this.cVOrchestrationService = cVOrchestrationService;

        [HttpPost]
        public async ValueTask<IActionResult> UploadCV(IFormFile pdfFile, [FromHeader] Guid vacancyId)
        {
            var pdfStream = new MemoryStream();
            pdfFile.CopyTo(pdfStream);
            pdfStream.Position = 0;
            string fileName = pdfFile.FileName;

            await this.cVOrchestrationService.UploadCv(pdfStream, fileName, vacancyId);

            return Created("CV successfuly uploaded");
        }
    }
}
