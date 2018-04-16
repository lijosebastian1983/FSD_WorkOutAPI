using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.DataLayer.Models
{
    public class WorkoutChart
    {
        public double WorkoutTimeDay { get; set; }
        public double WorkoutTimeWeek { get; set; }
        public double WorkoutTimeMonth { get; set; }
        public double CaloriesBurnedWeek { get; set; }
        public double CaloriesBurnedMonth { get; set; }
        public double CaloriesBurnedYear { get; set; }

        public List<double> WeeklyChart { get; set; }
        public List<double> MonthlyChart { get; set; }
        public List<double> YearlyChart { get; set; }
    }
}
