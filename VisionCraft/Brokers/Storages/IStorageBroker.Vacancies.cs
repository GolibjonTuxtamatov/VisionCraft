using VisionCraft.Models.Vacancies;

namespace VisionCraft.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Vacancy> InsertVacancyAsync(Vacancy vacancy);
        IQueryable<Vacancy> SelectAllVacancies();
        ValueTask<Vacancy> SelectVacancyByIdAsync(Guid id);
        ValueTask<Vacancy> UpdateVacancyAsync(Vacancy vacancy);
        ValueTask<Vacancy> DeleteVacancyAsync(Vacancy vacancy);
    }
}
