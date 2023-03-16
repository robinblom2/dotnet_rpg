using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "Fireball", Damage = 20 },
                new Skill { Id = 2, Name = "Whirlwind", Damage = 40 },
                new Skill { Id = 3, Name = "Meteor", Damage = 60 },
                new Skill { Id = 4, Name = "Backstab", Damage = 30 },
                new Skill { Id = 5, Name = "Smite", Damage = 30 }
            );
        }

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
        public DbSet<Skill> Skills => Set<Skill>();



    }
}