using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WorkoutTracker.DataLayer.Models;
using WorkOutTrackerAPI.DataAccess;

namespace WorkOutTrackerAPI.Controllers
{ 
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class WorkOutTrackerController : ApiController
    {
        public string Get()
        {
            return "Hello World from Angular";
        }

        public string Get(string moduleId)
        {
            return "Hello " + moduleId;
        }

        WorkOutTrackerDAO dal = new WorkOutTrackerDAO();
        //private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ActionName("CreateWorkoutCollection")]
        [HttpPost]
        public string CreateWorkoutCollection([FromBody]WorkoutCollectionModel wCollection)
        {
            try
            {
                var rr = dal.CreateWorkoutCollection(wCollection);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("GetAllWorkoutColletion")]
        [HttpGet]
        public IEnumerable<WorkoutDetails> GetAllWorkoutColletion([FromBody]string name)
        {
            try
            {
                var rr = dal.GetWorkoutCollectionDetails(name);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("GetWorkoutCollectionById")]
        [HttpGet]
        public WorkoutCollectionModel GetWorkoutCollectionById(int id)

        {
            try
            {
                var rr = dal.GetWorkoutCollectionById(Convert.ToInt32(id));
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }


        [ActionName("UpdateWorkoutColletion")]
        [HttpPost]
        public string UpdateWorkoutColletion([FromBody]WorkoutCollectionModel wrk)
        {
            try
            {
                var rr = dal.UpdateWorkoutCollection(wrk);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("DeleteWorkoutCollection")]
        [HttpPost]
        public string DeleteWorkoutCollection([FromBody]int wrk)

        {
            try
            {
                var rr = dal.DeleteWorkoutCollection(wrk);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("StartWorkout")]
        [HttpPost]
        public string StartWorkout([FromBody]StartOrEndWorkout wrk)
        {
            try
            {
                var dateInfo = ConvertDate(wrk.DateInfo + wrk.TimeInfo);
                if (dateInfo == null)
                    return "Invalid date or time";
                else
                {
                    wrk.DateTimeInfo = dateInfo.Value;
                    var rr = dal.StartWorkOut(wrk);
                    return "";
                }
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("EndWorkOut")]
        [HttpPost]
        public string EndWorkOut([FromBody]StartOrEndWorkout wrk)
        {
            try
            {
                var dateInfo = ConvertDate(wrk.DateInfo + wrk.TimeInfo);
                if (dateInfo == null)
                    return "Invalid date or time";
                else
                {
                    wrk.DateTimeInfo = dateInfo.Value;
                    var start = dal.GetWorkoutStartDateTime(wrk.WorkoutId);
                    if (start > dateInfo)
                    {
                        return "End date/time should be greater than Start date/time(" + start.ToString("dd'/'MM'/'yyyy HH:mm:ss tt");
                    }
                    else
                    {
                        dal.EndWorkOut(wrk);
                        return "";
                    }

                }
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        private DateTime? ConvertDate(string dateStr)
        {
            try
            {
                return DateTime.ParseExact(dateStr.Trim(), "dd/MM/yyyyHH:mm", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [ActionName("GetStartWorkoutDetails")]
        [HttpGet]
        public StartOrEndWorkout GetStartWorkoutDetails(int id)
        {
            try
            {
                var rr = dal.GetStartWorkoutDetails(Convert.ToInt32(id));
                CultureInfo ci = CultureInfo.InvariantCulture;
                rr.DateInfo = rr.DateTimeInfo.ToString("dd/MM/yyyy", ci); ;
                rr.TimeInfo = rr.DateTimeInfo.ToString("HH:mm", ci);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("GetEndWorkoutDetails")]
        [HttpGet]
        public StartOrEndWorkout GetEndWorkoutDetails(int id)
        {
            try
            {
                var rr = dal.GetEndWorkoutDetails(Convert.ToInt32(id));
                CultureInfo ci = CultureInfo.InvariantCulture;
                rr.DateInfo = rr.DateTimeInfo.ToString("dd/MM/yyyy", ci); ;
                rr.TimeInfo = rr.DateTimeInfo.ToString("HH:mm", ci);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("GetWorkoutChartDetails")]
        [HttpGet]
        public WorkoutChart GetWorkoutChartDetails()
        {
            try
            {
                return dal.GetWorkoutChartDetails();
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }
    }
}
