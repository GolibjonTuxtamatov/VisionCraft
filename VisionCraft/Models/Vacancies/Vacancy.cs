using Newtonsoft.Json;
using VisionCraft.Models.CVs;
using VisionCraft.Models.Requirements;
using VisionCraft.Models.Teams;

namespace VisionCraft.Models.Vacancies
{
    public class Vacancy
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public LevelType Level { get; set; }

        public Guid TeamId { get; set; }
        [JsonIgnore]
        public virtual Team Team { get; set; }
        [JsonIgnore]
        public virtual ICollection<Requirement> Requirements { get; set; }
        [JsonIgnore]
        public virtual ICollection<CV> CVs { get; set; }
    }
}
