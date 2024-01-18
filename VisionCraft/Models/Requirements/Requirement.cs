using Newtonsoft.Json;
using VisionCraft.Models.Vacancies;

namespace VisionCraft.Models.Requirements
{
    public class Requirement
    {
        public Guid Id { get; set; }
        public string Request { get; set; }

        public Guid VacancyId { get; set; }
        [JsonIgnore]
        public virtual Vacancy Vacancy { get; set; }
    }
}
