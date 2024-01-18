using Newtonsoft.Json;
using VisionCraft.Models.Vacancies;

namespace VisionCraft.Models.CVs
{
    public class CV
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Conclusion { get; set; }
        public string CVPath { get; set; }

        public Guid VacancyId { get; set; }
        [JsonIgnore]
        public virtual Vacancy Vacancy { get; set; }
    }
}
