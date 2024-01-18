using VisionCraft.Models.Vacancies;
using VisionCraft.Services.Foundations.Vacancies;

namespace VisionCraft.Services.Proccessings.Vacancies
{
    public class VacancyProccessingService : IVacancyProccessingService
    {
        private readonly IVacancyService vacancyService;

        public VacancyProccessingService(IVacancyService vacancyService) =>
            this.vacancyService = vacancyService;

        public async ValueTask<Vacancy> ProcAddVacancyAsync(Vacancy vacancy) =>
            await this.vacancyService.AddVacancyAsync(vacancy);

        public IQueryable<Vacancy> ProcRetrieveAllVacancies() =>
            this.vacancyService.RetrieveAllVacancies();

        public async ValueTask<Vacancy> ProcRetrieveVacancyByIdAsync(Guid id) =>
            await this.vacancyService.RetrieveVacancyByIdAsync(id);

        public async ValueTask<Vacancy> ProcModifyVacancyAsync(Vacancy vacancy) =>
            await this.vacancyService.ModifyVacancyAsync(vacancy);

        public async ValueTask<Vacancy> ProcRemoveVacancyAsync(Guid id) =>
            await this.vacancyService.RemoveVacancyAsync(id);
    }
}
