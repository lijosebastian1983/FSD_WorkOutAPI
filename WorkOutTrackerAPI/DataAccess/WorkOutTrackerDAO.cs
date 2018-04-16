using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using WorkoutTracker.DataLayer.Entities;
using WorkoutTracker.DataLayer.Models;

namespace WorkOutTrackerAPI.DataAccess
{
    public class WorkOutTrackerDAO
    {
        //CRUD - workout_collection
        public string CreateWorkoutCollection(WorkoutCollectionModel wCollectionModel)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var wCollection = new workout_collection()
                {
                    calories_burn_per_min = wCollectionModel.calories_burn_per_min,
                    category_id = wCollectionModel.category_id,
                    workout_id = wCollectionModel.workout_id,
                    workout_title = wCollectionModel.workout_title,
                    workout_note = wCollectionModel.workout_note

                };
                dbContext.WorkoutCollection.Add(wCollection);
                dbContext.SaveChanges();
                return "Workout Successfully    Inserted";
            }
        }

        public List<WorkoutDetails> GetWorkoutCollectionDetails(string name)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var lst = dbContext.WorkoutCollection.Where(i => (i.workout_title.StartsWith(name) || String.IsNullOrEmpty(name))).ToList();
                var wrkDetails = new List<WorkoutDetails>();


                foreach (var it in lst)
                {
                    var wrk = new WorkoutDetails();
                    wrk.WorkoutId = it.workout_id;
                    wrk.WorkoutTitle = it.workout_title;
                    var act = dbContext.WorkoutActive.Where(i => i.workout_id == it.workout_id).FirstOrDefault();
                    if (act != null)
                    {
                        wrk.IsStarted = act.start_date == null ? false : true;
                        wrk.IsEnded = act.end_date == null ? false : true;
                    }
                    else
                    {
                        wrk.IsStarted = false;
                        wrk.IsEnded = true;
                    }
                    if (wrk.IsEnded == true)
                        wrk.IsStarted = false;
                    wrkDetails.Add(wrk);
                }

                return wrkDetails;
            }
        }

        public DateTime GetWorkoutStartDateTime(int workoutId)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var wrk = dbContext.WorkoutCollection.Where(i => (i.workout_id == workoutId)).FirstOrDefault();
                var act = dbContext.WorkoutActive.Where(i => i.workout_id == wrk.workout_id).FirstOrDefault();
                return act.start_date.Value;
            }
        }
        public WorkoutCollectionModel GetWorkoutCollectionById(int id)
        {
            var coll = new WorkoutCollectionModel();
            using (var dbContext = new WorkoutDBContext())
            {
                coll = dbContext.WorkoutCollection.Where(i => (i.workout_id == id))
                    .Select(x => new WorkoutCollectionModel()
                    {
                        calories_burn_per_min = x.calories_burn_per_min,
                        category_id = x.category_id,
                        workout_id = x.workout_id,
                        workout_note = x.workout_note,
                        workout_title = x.workout_title
                    })
                    .FirstOrDefault();
            }
            return coll;
        }

        public WorkoutCollectionModel GetWorkoutCollectionByTitle(string title)
        {
            var coll = new WorkoutCollectionModel();
            using (var dbContext = new WorkoutDBContext())
            {
                coll = dbContext.WorkoutCollection.Where(i => (i.workout_title == title))
                    .Select(x => new WorkoutCollectionModel()
                    {
                        calories_burn_per_min = x.calories_burn_per_min,
                        category_id = x.category_id,
                        workout_id = x.workout_id,
                        workout_note = x.workout_note,
                        workout_title = x.workout_title
                    })
                    .FirstOrDefault();
            }
            return coll;
        }

        public string UpdateWorkoutCollection(WorkoutCollectionModel wrk)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var cat = dbContext.WorkoutCollection.Where(a => a.workout_id.Equals(wrk.workout_id)).SingleOrDefault();
                cat.workout_title = wrk.workout_title;
                cat.workout_note = wrk.workout_note;
                cat.category_id = wrk.category_id;
                dbContext.SaveChanges();
                return "Workout collection Successfully Updated";
            }
        }

        public string DeleteWorkoutCollection(int wrk)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var workout = dbContext.WorkoutCollection.Where(a => a.workout_id.Equals(wrk)).SingleOrDefault();
                var active = dbContext.WorkoutActive.Where(i => i.workout_id == workout.workout_id).ToList();
                foreach (var ac in active)
                    dbContext.WorkoutActive.Remove(ac);
                dbContext.WorkoutCollection.Remove(workout);
                dbContext.SaveChanges();
                return "Workout collection Successfully deleted";
            }
        }

        public string StartWorkOut(StartOrEndWorkout wrk)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var exist = dbContext.WorkoutActive.Where(a => a.workout_id.Equals(wrk.WorkoutId)).SingleOrDefault();
                if (exist == null)
                {
                    var aWork = new workout_active()
                    {
                        workout_id = wrk.WorkoutId,
                        comment = wrk.Comment,
                        status = true,
                        start_date = wrk.DateTimeInfo,
                        start_time = wrk.DateTimeInfo.TimeOfDay,
                        end_date = null,
                        end_time = null
                    };
                    dbContext.WorkoutActive.Add(aWork);
                    dbContext.SaveChanges();
                }
                else
                {
                    exist.start_time = wrk.DateTimeInfo.TimeOfDay;
                    exist.start_date = wrk.DateTimeInfo;
                    exist.comment = wrk.Comment;
                    exist.status = true;
                    exist.end_time = null;
                    exist.end_date = null;
                    dbContext.SaveChanges();
                }
                return "Workout Successfully started";
            }
        }

        public string EndWorkOut(StartOrEndWorkout wrk)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var exist = dbContext.WorkoutActive.Where(a => a.workout_id.Equals(wrk.WorkoutId)).SingleOrDefault();
                exist.end_time = wrk.DateTimeInfo.TimeOfDay;
                exist.end_date = wrk.DateTimeInfo;
                exist.comment = wrk.Comment;
                exist.status = false;
                dbContext.SaveChanges();
                return "Workout Successfully Ended";
            }
        }

        public StartOrEndWorkout GetStartWorkoutDetails(int workoutId)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var aWorkout = new StartOrEndWorkout();
                var coll = dbContext.WorkoutCollection.Where(i => (i.workout_id == workoutId)).FirstOrDefault();
                var exist = dbContext.WorkoutActive.Where(a => a.workout_id.Equals(workoutId)).SingleOrDefault();
                if (exist == null)
                {
                    aWorkout.WorkoutTitle = coll.workout_title;
                    aWorkout.WorkoutId = workoutId;
                    aWorkout.Comment = "";
                    aWorkout.DateTimeInfo = DateTime.Now;
                    aWorkout.Status = true;
                    aWorkout.DateInfo = "";
                    aWorkout.TimeInfo = "";

                }
                else
                {
                    aWorkout.WorkoutTitle = coll.workout_title;
                    aWorkout.WorkoutId = workoutId;
                    aWorkout.Comment = exist.comment;
                    aWorkout.DateTimeInfo = DateTime.Now;
                    aWorkout.Status = true;
                    aWorkout.DateInfo = "";
                    aWorkout.TimeInfo = "";
                }
                return aWorkout;
            }
        }

        public StartOrEndWorkout GetEndWorkoutDetails(int workoutId)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var aWorkout = new StartOrEndWorkout();
                var coll = dbContext.WorkoutCollection.Where(i => (i.workout_id == workoutId)).FirstOrDefault();
                var exist = dbContext.WorkoutActive.Where(a => a.workout_id.Equals(workoutId)).SingleOrDefault();
                aWorkout.WorkoutTitle = coll.workout_title;
                aWorkout.WorkoutId = workoutId;
                aWorkout.Comment = exist.comment;
                aWorkout.DateTimeInfo = exist.end_date ?? DateTime.Now;
                aWorkout.Status = true;
                aWorkout.DateInfo = "";
                aWorkout.TimeInfo = "";

                return aWorkout;
            }
        }

        public WorkoutChart GetWorkoutChartDetails()
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var dateNow = DateTime.Now; ;
                var abc = dbContext.WorkoutActive.Where(i => i.start_date != null && i.end_date != null);
                var wrkDatasOrg = new List<WorkoutData>();
                var wrkDatas = new List<WorkoutData>();
                wrkDatasOrg = abc
                    .Join(dbContext.WorkoutCollection, k => k.workout_id, i => i.workout_id, (k, i) =>
                            new WorkoutData()
                            {
                                WorkoutId = k.workout_id,
                                StartTime = k.start_date.Value,
                                EndTime = k.end_date.Value,
                                CaloryInMin = i.calories_burn_per_min ?? 0,
                            }).ToList();

                //Check for start and end date
                foreach (var itm in wrkDatasOrg)
                {
                    if (itm.StartTime.Date != itm.EndTime.Date)
                    {
                        TimeSpan difference = itm.StartTime.Date - itm.EndTime.Date;
                        var days = Math.Abs(difference.TotalDays);
                        var stDay = itm.StartTime;
                        for (int i = 0; i <= days; i++)
                        {
                            var wk = new WorkoutData();
                            wk.WorkoutId = itm.WorkoutId;
                            wk.StartTime = stDay;
                            wk.EndTime = new DateTime(stDay.Year, stDay.Month, stDay.Day, 23, 59, 59);
                            if (i == days)
                                wk.EndTime = itm.EndTime;
                            wk.CaloryInMin = itm.CaloryInMin;
                            wrkDatas.Add(wk);
                            stDay = new DateTime(stDay.Year, stDay.Month, stDay.Day, 0, 0, 1).AddDays(1);
                        }
                    }
                    else
                    {
                        wrkDatas.Add(itm);
                    }
                }

                foreach (var itm in wrkDatas)
                {
                    itm.MonthID = itm.StartTime.Month;
                    itm.YearID = itm.StartTime.Year;
                    itm.DayID = GetDayIds(itm.StartTime);
                    itm.WeekID = GetWeekOfMonth(itm.StartTime);
                    itm.TotalTimeInMins = Math.Abs(itm.EndTime.Subtract(itm.StartTime).TotalMinutes);
                    itm.TotalCalory = itm.TotalTimeInMins * itm.CaloryInMin;
                }


                var wChart = new WorkoutChart();
                wChart.WeeklyChart = new List<double>();
                wChart.MonthlyChart = new List<double>();
                wChart.YearlyChart = new List<double>();

                //Get total workout time details

                wChart.WorkoutTimeDay = Math.Round(wrkDatas.Where(x => x.DayID == GetDayIds(dateNow) && x.WeekID == GetWeekOfMonth(dateNow) && x.YearID == dateNow.Year)
                   .Sum(x => (x.TotalTimeInMins)), 0);
                wChart.WorkoutTimeWeek = Math.Round(wrkDatas.Where(x => x.WeekID == GetWeekOfMonth(dateNow) && x.YearID == dateNow.Year)
                   .Sum(x => (x.TotalTimeInMins)), 0);
                wChart.WorkoutTimeMonth = Math.Round(wrkDatas.Where(x => x.MonthID == dateNow.Month && x.YearID == dateNow.Year)
                   .Sum(x => (x.TotalTimeInMins)), 0);

                //Get total burned out details               
                wChart.CaloriesBurnedWeek = Math.Round(wrkDatas.Where(x => x.WeekID == GetWeekOfMonth(dateNow) && x.YearID == dateNow.Year)
                     .Sum(x => x.TotalCalory), 2);

                wChart.CaloriesBurnedMonth = Math.Round(wrkDatas.Where(x => x.MonthID == dateNow.Month && x.YearID == dateNow.Year)
                   .Sum(x => x.TotalCalory), 2);

                wChart.CaloriesBurnedYear = Math.Round(wrkDatas.Where(x => x.YearID == dateNow.Year)
                    .Sum(x => x.TotalCalory), 2);

                //Weekly Chart               
                for (int j = 1; j <= 7; j++)
                {
                    wChart.WeeklyChart.Add(Math.Round(wrkDatas.Where(x => x.WeekID == GetWeekOfMonth(dateNow) && x.YearID == dateNow.Year)
                        .Where(m => m.DayID == j)
                        .Sum(x => x.TotalCalory), 2));

                }

                ////Month Chart               
                int currWeek = GetWeekOfMonth(dateNow);
                var weeks = Enumerable.Range(0, 4).Select(n => dateNow.AddDays(n * 7 - (int)dateNow.DayOfWeek + 1)).TakeWhile(monday => monday.Month == dateNow.Month);

                foreach (var w in weeks)
                {
                    wChart.MonthlyChart.Add(Math.Round(wrkDatas.Where(x => x.StartTime.Month == dateNow.Month && x.YearID == dateNow.Year)
                        .Where(m => GetWeekOfMonth(m.StartTime) == GetWeekOfMonth(w.Date))
                        .Sum(x => x.TotalCalory), 2));

                }
                if (wChart.MonthlyChart.Count < 5)
                    wChart.MonthlyChart.Add(0);

                ////Year Chart               
                for (int j = 1; j <= 12; j++)
                {
                    wChart.YearlyChart.Add(Math.Round(wrkDatas.Where(x => x.YearID == dateNow.Year)
                        .Where(m => m.MonthID == j)
                     .Sum(x => x.TotalCalory), 2));

                }
                return wChart;
            }
        }

        private int GetDayIds(DateTime itm)
        {
            int id = 0;
            switch (itm.DayOfWeek.ToString().ToLower())
            {
                case "sunday":
                    id = 7;
                    break;
                case "monday":
                    id = 1;
                    break;
                case "tuesday":
                    id = 2;
                    break;
                case "wednesday":
                    id = 3;
                    break;
                case "thursday":
                    id = 4;
                    break;
                case "friday":
                    id = 5;
                    break;
                case "saturday":
                    id = 6;
                    break;

            }
            return id;
        }
        public static int GetWeekOfMonth(DateTime time)
        {

            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}