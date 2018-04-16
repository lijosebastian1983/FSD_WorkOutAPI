using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.DataLayer.Models
{
    public class WorkoutCollectionModel
    {
        public int workout_id { get; set; }
        public string workout_title { get; set; }
        public string workout_note { get; set; }
        public double? calories_burn_per_min { get; set; }
        public int category_id { get; set; }
    }
}
