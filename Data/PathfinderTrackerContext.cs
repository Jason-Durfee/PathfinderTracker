using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PathfinderTracker.Models
{
    public class PathfinderTrackerContext : DbContext
    {
        public PathfinderTrackerContext (DbContextOptions<PathfinderTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<PathfinderTracker.Models.Alignment> Alignment { get; set; }
    }
}
