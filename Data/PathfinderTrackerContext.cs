using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PathfinderTracker.Models;

namespace PathfinderTracker.Models
{
    public class PathfinderTrackerContext : DbContext
    {
        public PathfinderTrackerContext (DbContextOptions<PathfinderTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<PathfinderTracker.Models.Alignment> Alignment { get; set; }

        public DbSet<PathfinderTracker.Models.Armor> Armor { get; set; }

        public DbSet<PathfinderTracker.Models.ArmorAddon> ArmorAddon { get; set; }

        public DbSet<PathfinderTracker.Models.ArmorType> ArmorType { get; set; }

        public DbSet<PathfinderTracker.Models.Campaign> Campaign { get; set; }

        public DbSet<PathfinderTracker.Models.Character> Character { get; set; }

        public DbSet<PathfinderTracker.Models.CharacterClass> Class { get; set; }

        public DbSet<PathfinderTracker.Models.ClassesToCharacter> ClassesToCharacter { get; set; }

        public DbSet<PathfinderTracker.Models.Deity> Deity { get; set; }

        public DbSet<PathfinderTracker.Models.Feat> Feat { get; set; }

        public DbSet<PathfinderTracker.Models.MagicSchool> MagicSchool { get; set; }

        public DbSet<PathfinderTracker.Models.Material> Material { get; set; }

        public DbSet<PathfinderTracker.Models.Player> Player { get; set; }

        public DbSet<PathfinderTracker.Models.Race> Race { get; set; }

        public DbSet<PathfinderTracker.Models.Spell> Spell { get; set; }

        public DbSet<PathfinderTracker.Models.Bloodline> Bloodline { get; set; }

        public DbSet<PathfinderTracker.Models.Weapon> Weapon { get; set; }

        public DbSet<PathfinderTracker.Models.WeaponCategory> WeaponCategory { get; set; }

        public DbSet<PathfinderTracker.Models.WeaponType> WeaponType { get; set; }

        public DbSet<PathfinderTracker.Models.FeatType> FeatType { get; set; }

        public DbSet<PathfinderTracker.Models.Condition> Condition { get; set; }

        public DbSet<PathfinderTracker.Models.Item> Item { get; set; }

        public DbSet<PathfinderTracker.Models.Slot> Slot { get; set; }

        public DbSet<PathfinderTracker.Models.ArmorAddonType> ArmorAddonType { get; set; }
    }
}
