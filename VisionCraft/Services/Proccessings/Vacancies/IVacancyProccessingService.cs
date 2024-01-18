using VisionCraft.Models.Vacancies;

namespace VisionCraft.Services.Proccessings.Vacancies
{
    public interface IVacancyProccessingService
    {
        ValueTask<Vacancy> ProcAddVacancyAsync(Vacancy vacancy);
        IQueryable<Vacancy> ProcRetrieveAllVacancies();
        ValueTask<Vacancy> ProcRetrieveVacancyByIdAsync(Guid id);
        ValueTask<Vacancy> ProcModifyVacancyAsync(Vacancy vacancy);
        ValueTask<Vacancy> ProcRemoveVacancyAsync(Guid id);
    }
}
