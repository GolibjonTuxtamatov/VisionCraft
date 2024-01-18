using Microsoft.EntityFrameworkCore;
using VisionCraft.Models.Vacancies;

namespace VisionCraft.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Vacancy> Vacancies { get; set; }

        public async ValueTask<Vacancy> InsertVacancyAsync(Vacancy vacancy) =>
            await InsertAsync(vacancy);

        public IQueryable<Vacancy> SelectAllVacancies() =>
            SelectAll<Vacancy>();

        public async ValueTask<Vacancy> SelectVacancyByIdAsync(Guid id)
        {
            Vacancy vacancy = await SelectAll<Vacancy>()
                .Include(vacancy => vacancy.CVs)
                .Include(vacancy => vacancy.Requirements)
                .FirstOrDefaultAsync(vacancy => vacancy.Id == id);

            return vacancy;
        }

        public async ValueTask<Vacancy> UpdateVacancyAsync(Vacancy vacancy) =>
            await UpdateAsync(vacancy);

        public async ValueTask<Vacancy> DeleteVacancyAsync(Vacancy vacancy) =>
            await DeleteAsync(vacancy);
    }
}
