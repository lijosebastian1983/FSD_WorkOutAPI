using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WorkoutTracker.DataLayer.Entities
{
    public class WorkoutDBContext : DbContext
    {
        public WorkoutDBContext() : base("name = WorkoutConnection") { }


        public virtual DbSet<workout_collection> WorkoutCollection { get; set; }
        public virtual DbSet<workout_category> WorkoutCategory { get; set; }
        public virtual DbSet<workout_active> WorkoutActive { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {           

        }
    }
}
