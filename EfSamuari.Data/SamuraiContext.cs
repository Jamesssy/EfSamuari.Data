using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EfSamurai.Domain;

namespace EfSamurai.Data
{
    public class SamuraiContext :DbContext
    {

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Battle> Battle { get; set; }
        public DbSet<BattleLog> BattleLog { get; set; }
        public DbSet<BattleEvent> BattleEvent { get; set; }
        public DbSet<SamuraiBattle> SamuraiBattle { get; set; }
        public DbSet<SecretIdentity> SecretIdentity { get; set; }
        public DbSet<Quotes> Quotes { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
             "Server = (localdb)\\mssqllocaldb; Database = EfSamurai; Trusted_Connection = True; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<SamuraiBattle>().HasKey(samuraiBattle => new { samuraiBattle.SamuraiId, samuraiBattle.BattleId });
        }

    }
}
