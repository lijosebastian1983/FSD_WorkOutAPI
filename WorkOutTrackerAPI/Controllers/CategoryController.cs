//using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WorkoutTracker.ServiceLayerAPI.Controllers
{
    //[Route("api/Category")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        WorkoutTracker.DataLayer.CategoryDAO dal = new DataLayer.CategoryDAO();
        //private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ActionName("GetAllCategory")]
        [HttpGet]
        public IEnumerable<DataLayer.Models.WorkoutCategoryModel> GetAllCategory([FromBody]string name)
        {
            try
            {
                var rr = dal.GetWorkoutCategory(name);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("CreateCategory")]
        [HttpPost]
        public string CreateCategory([FromBody] DataLayer.Models.WorkoutCategoryModel category)
        {
            try
            {
                var rr = dal.CreateCategory(category);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("UpdateCategory")]
        [HttpPost]
        public string UpdateCategory([FromBody] DataLayer.Models.WorkoutCategoryModel category)
        {
            try
            {
                var rr = dal.UpdateCategory(category);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("DeleteCategory")]
        [HttpPost]
        public string DeleteCategory([FromBody] DataLayer.Models.WorkoutCategoryModel category)
        {
            try { 
            var rr = dal.DeleteCategory(category);
            return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("TestGetAllCategory")]
        [HttpGet]
        public IEnumerable<DataLayer.Models.WorkoutCategoryModel> TestGetAllCategory()
        {
            try { 
            var rr = dal.GetWorkoutCategory("");
            return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [ActionName("TestCreateCategory")]
        [HttpPost]
        public string TestCreateCategory()
        {
            try
            {
                var rr = dal.CreateCategory(new DataLayer.Models.WorkoutCategoryModel() { category_id = 0, category_name = "TEst" });
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

         

    }
}
