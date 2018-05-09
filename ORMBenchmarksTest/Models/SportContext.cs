

using System.Data.Entity;

namespace ORMBenchmarksTest.Models
{

    public class SportContext : DbContext
    {
        public SportContext() : base("DefaultConnection")
        {
            
        }
        public virtual DbSet<Kid> Kids { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasMany(e => e.Kids)
                .WithRequired(e => e.Player).WillCascadeOnDelete();

            modelBuilder.Entity<Sport>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Sport).WillCascadeOnDelete();

            modelBuilder.Entity<Team>().HasMany(e => e.Players).WithMany(e => e.Teams)
                .Map(x => x.ToTable("TeamPlayers"));
            
        }
    }
}
