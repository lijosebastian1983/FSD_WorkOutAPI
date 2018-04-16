using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.DataLayer.Entities;
using WorkoutTracker.DataLayer.Models;

namespace WorkoutTracker.DataLayer
{
    public class CategoryDAO 
    { 

        public List<Models.WorkoutCategoryModel> GetWorkoutCategory(string name)
        {
            var model = new List<WorkoutCategoryModel>();
            using (var dbContext = new WorkoutDBContext())
            {
                model = dbContext.WorkoutCategory.Where(i => (i.category_name.StartsWith(name) || String.IsNullOrEmpty(name)))
                    .Select(x=> new WorkoutCategoryModel() {category_id=x.category_id,category_name=x.category_name })
                    .ToList();
            }
            return model;
        }

        public WorkoutCategoryModel GetWorkoutCategoryByName(string name)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                return dbContext.WorkoutCategory.Where(i => (i.category_name.StartsWith(name) || String.IsNullOrEmpty(name)))
                    .Select(x => new WorkoutCategoryModel() { category_id = x.category_id, category_name = x.category_name })
                    .FirstOrDefault();
            }
        }

        public WorkoutCategoryModel GetWorkoutCategoryById(int catId)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                return dbContext.WorkoutCategory.Where(i => (i.category_id==catId))
                    .Select(x => new WorkoutCategoryModel() { category_id = x.category_id, category_name = x.category_name })
                    .FirstOrDefault();
            }
        }

        public string CreateCategory(Models.WorkoutCategoryModel categoryModel)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var category = new Entities.workout_category() { category_id = categoryModel.category_id, category_name = categoryModel.category_name };
                dbContext.WorkoutCategory.Add(category);
                dbContext.SaveChanges();
                return "Category Successfully Inserted";
            }
        }

        public string UpdateCategory(Models.WorkoutCategoryModel category)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var cat = dbContext.WorkoutCategory.Where(a => a.category_id.Equals(category.category_id)).SingleOrDefault();
                cat.category_name = category.category_name;
                dbContext.SaveChanges();
                return "Category Successfully Updated";
            }
        }

        public string DeleteCategory(Models.WorkoutCategoryModel category)
        {
            using (var dbContext = new WorkoutDBContext())
            {
                var cat = dbContext.WorkoutCategory.Where(a => a.category_id.Equals(category.category_id))
                .SingleOrDefault();
                var workout = dbContext.WorkoutCollection.Where(i => i.category_id == cat.category_id).ToList();
                foreach (var it in workout)
                {
                    var active = dbContext.WorkoutActive.Where(i => i.workout_id == it.workout_id).ToList();
                    foreach (var ac in active)
                        dbContext.WorkoutActive.Remove(ac);
                    dbContext.WorkoutCollection.Remove(it);
                }
                dbContext.WorkoutCategory.Remove(cat);
                dbContext.SaveChanges();
                return "Category Successfully deleted";
            }
        }        
    }
}
