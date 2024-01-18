using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Storages;
using VisionCraft.Models.Vacancies;

namespace VisionCraft.Services.Foundations.Vacancies
{
    public class VacancyService : IVacancyService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public VacancyService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Vacancy> AddVacancyAsync(Vacancy vacancy) =>
            await this.storageBroker.InsertVacancyAsync(vacancy);

        public IQueryable<Vacancy> RetrieveAllVacancies() =>
            this.storageBroker.SelectAllVacancies();

        public async ValueTask<Vacancy> RetrieveVacancyByIdAsync(Guid id) =>
            await this.storageBroker.SelectVacancyByIdAsync(id);

        public async ValueTask<Vacancy> ModifyVacancyAsync(Vacancy vacancy) =>
            await this.storageBroker.UpdateVacancyAsync(vacancy);

        public async ValueTask<Vacancy> RemoveVacancyAsync(Guid id)
        {
            Vacancy foundVacancy = await this.storageBroker.SelectVacancyByIdAsync(id);

            return await this.storageBroker.DeleteVacancyAsync(foundVacancy);
        }
    }
}
