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

        public DbSet<PathfinderTracker.Models.Class> Class { get; set; }
    }
}
