using Microsoft.EntityFrameworkCore;

namespace ToyotaGarancneOpravy.Models
{
    public partial class Toyota_DBContext : DbContext
    {
        public Toyota_DBContext()
        {
        }

        public Toyota_DBContext(DbContextOptions<Toyota_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GarancnaOprava> garancneOpravy { get; set; } = null!;
        public virtual DbSet<Priloha> prilohy { get; set; } = null!;
        public virtual DbSet<PrilohaTyp> prilohaTypy { get; set; } = null!;

    }
}
