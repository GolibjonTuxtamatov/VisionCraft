using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VisionCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async ValueTask<ActionResult<string>> Get() =>
            Ok("Okkey");
    }
}
