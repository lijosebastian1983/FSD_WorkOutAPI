using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.DataLayer.Models
{
    public class WorkoutData
    {
        public int WorkoutId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double TotalTimeInMins { get; set; }
        public double TotalCalory { get; set; }
        public double CaloryInMin { get; set; }

        public int DayID { get; set; }
        public int WeekID { get; set; }
        public int MonthID { get; set; }
        public int YearID { get; set; }

    }
}
