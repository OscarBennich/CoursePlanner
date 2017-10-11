using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CoursePlanner.Models;
using WebGrease.Css.Extensions;

namespace CoursePlanner.Controllers
{
    [Authorize] 
    public class HomeController : Controller
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        [HttpGet]
        public ActionResult Index()
        {
            //input.ForEach(t => t.Sort());
            //input = input.OrderBy(t => t.First()).ToList()

            var model = db.Course.ToList();
           // model.ForEach(c => c.CourseOccurrence.OrderBy(co => co.StartDate.ToString()));
            //model.OrderBy(c => c.CourseOccurrence.OrderBy(co => co.StartDate.ToString())).ThenBy(c => c.CourseName);
            return View(model);
        }   

        [HttpGet]
        public ActionResult FilterCourses(string filter)
        {
            List<Course> model;
            if (filter == "Fall")
                model = db.Course.Where(c => c.CourseOccurrence.Any(co => co.Term == Terms.Fall)).ToList();
            else if (filter == "Spring")
                model = db.Course.Where(c => c.CourseOccurrence.Any(co => co.Term == Terms.Spring)).ToList();
            else
                model = db.Course.ToList();

            return PartialView("_CourseConflictTable", model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            var filters = formCollection.AllKeys.Where(key => formCollection[key].Contains("true")).ToList();

            return View(db.Course.ToList());
        }

        public static string CurrentSchoolYear()
        {
            int fall;

            if (DateTime.Today.Month > 6 && DateTime.Today.Month <= 12)
                fall = DateTime.Today.Year;
            else
            {
                fall = DateTime.Today.Year-1;
            }

            int spring;
            if (DateTime.Today.Month >= 1 && DateTime.Today.Month <= 6)
                spring = DateTime.Today.Year;
            else
            {
                spring = DateTime.Today.Year + 1;
            }

            return fall + "/" +  spring;

        }
    }
}
