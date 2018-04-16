using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.DataLayer.Models
{
    public class WorkoutDetails
    {
        public int WorkoutId { get; set; }
        public string WorkoutTitle { get; set; }
        public bool IsStarted { get; set; }
        public bool IsEnded { get; set; }
    }
}
