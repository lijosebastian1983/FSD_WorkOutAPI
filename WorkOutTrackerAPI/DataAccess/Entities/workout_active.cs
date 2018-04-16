using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.DataLayer.Entities
{
    public class workout_active
    {
        [Key]    
        public int workout_id { get; set; } 
        public TimeSpan? start_time { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public TimeSpan? end_time { get; set; }
        public string comment { get; set; }
        public bool? status { get; set; }

        [Key, ForeignKey("workout_id")]
        public virtual workout_collection WorkoutCollection { get; set; }

    }
}
