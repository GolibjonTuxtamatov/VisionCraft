using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RESTFulSense.Controllers;
using VisionCraft.Models.Vacancies;
using VisionCraft.Services.Foundations.Vacancies;

namespace VisionCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VacanciesController : RESTFulController
    {
        private readonly IVacancyService vacancyService;

        public VacanciesController(IVacancyService vacancyService) =>
            this.vacancyService = vacancyService;

        [HttpPost]
        public async ValueTask<ActionResult<Vacancy>> PostVacancyAsync(Vacancy vacancy) =>
            await this.vacancyService.AddVacancyAsync(vacancy);

        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Vacancy>> GetAllVacancies() =>
            Ok(this.vacancyService.RetrieveAllVacancies());

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<Vacancy>> GetVacancyByIdAsync(Guid id) =>
            await this.vacancyService.RetrieveVacancyByIdAsync(id);

        [HttpPut]
        public async ValueTask<ActionResult<Vacancy>> PutVacancyAsync(Vacancy vacancy) =>
            await this.vacancyService.ModifyVacancyAsync(vacancy);

        [HttpDelete]
        public async ValueTask<ActionResult<Vacancy>> DeleteVacancyAsync(Guid id) =>
            await this.vacancyService.RemoveVacancyAsync(id);
    }
}
