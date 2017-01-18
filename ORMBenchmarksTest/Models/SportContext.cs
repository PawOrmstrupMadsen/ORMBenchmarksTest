using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ORMBenchmarksTest.Models
{
    using System;

    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class SportContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=EFPerfTest;Integrated Security=True;");

        }

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>()
                .ForSqlServerIsMemoryOptimized()
                .HasMany(e => e.Teams)
                .WithOne(e => e.Sport).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Team>()
                 .ForSqlServerIsMemoryOptimized()
                .HasMany(e => e.Players)
                .WithOne(e => e.Team).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .ForSqlServerIsMemoryOptimized()
                ;
        }
    }
}
