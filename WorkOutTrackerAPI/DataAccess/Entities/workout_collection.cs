using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.DataLayer.Entities
{
    public class workout_collection
    {
        [Key]
        public int workout_id { get; set; }
        public string workout_title { get; set; }
        public string workout_note { get; set; }
        public double? calories_burn_per_min { get; set; }
        public int category_id { get; set; }

        [Key, ForeignKey("category_id")]
        public virtual workout_category WorkoutCategory { get; set; }
    }
}
