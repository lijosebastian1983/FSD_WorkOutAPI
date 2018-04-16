using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.DataLayer.Entities
{
    public class workout_category
    {
        [Key]
        public int category_id { get; set; }
        public string category_name { get; set; }
    }
}
