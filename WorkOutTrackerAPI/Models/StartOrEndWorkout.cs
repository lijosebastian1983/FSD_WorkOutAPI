using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.DataLayer.Models
{
    public class StartOrEndWorkout
    {
        public int WorkoutId { get; set; }
        public string WorkoutTitle { get; set; }
        public string Comment { get; set; }
        public DateTime DateTimeInfo { get; set; }       
        public string DateInfo { get; set; }
        public string TimeInfo { get; set; }
        public bool Status { get; set; }


    }
}
