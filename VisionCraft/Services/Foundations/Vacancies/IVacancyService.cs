using VisionCraft.Models.Vacancies;

namespace VisionCraft.Services.Foundations.Vacancies
{
    public interface IVacancyService
    {
        ValueTask<Vacancy> AddVacancyAsync(Vacancy vacancy);
        IQueryable<Vacancy> RetrieveAllVacancies();
        ValueTask<Vacancy> RetrieveVacancyByIdAsync(Guid id);
        ValueTask<Vacancy> ModifyVacancyAsync(Vacancy vacancy);
        ValueTask<Vacancy> RemoveVacancyAsync(Guid id);
    }
}
