using Microsoft.EntityFrameworkCore;
using VisionCraft.Models.CVs;

namespace VisionCraft.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<CV> CVs { get; set; }
    }
}
