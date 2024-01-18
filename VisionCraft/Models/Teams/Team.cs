﻿using Newtonsoft.Json;
using VisionCraft.Models.Vacancies;

namespace VisionCraft.Models.Teams
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
