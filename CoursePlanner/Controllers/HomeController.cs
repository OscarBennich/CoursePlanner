using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CoursePlanner.Models;

namespace CoursePlanner.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Course.ToList());
        }

        [HttpGet]
        public ActionResult FilterCourses(string filter)
        {
            var courses = db.Course.ToList();
            List<Course> model;
            switch (filter)
            {
                case "Fall":
                    model = courses.Where(c => c.CourseOccurrence.Any(co => co.Term == Terms.Fall)).ToList();
                    break;
                case "Spring":
                    model = courses.Where(c => c.CourseOccurrence.Any(co => co.Term == Terms.Spring)).ToList();
                    break;
                case "Fall-P1":
                    model = courses.Where(c => c.CourseOccurrence.Any(co => co.Period.ToString().Substring(0, 2) == "P1" && co.Term == Terms.Fall)).ToList();
                    break;
                case "Fall-P2":
                    model = courses.Where(c => c.CourseOccurrence.Any(co => co.Period.ToString().Substring(0, 2) == "P2" && co.Term == Terms.Fall)).ToList();
                    break;
                case "Fall-P3":
                    model = courses.Where(c => c.CourseOccurrence.Any(co => co.Period.ToString().Substring(0, 2) == "P3" && co.Term == Terms.Fall)).ToList();
                    break;
                case "Fall-P4":
                    model = courses.Where(c => c.CourseOccurrence.Any(co => co.Period.ToString().Substring(0, 2) == "P4" && co.Term == Terms.Fall)).ToList();
                    break;
                case "Spring-P1":
                    model = courses.Where(c => c.CourseOccurrence.Any(co => co.Period.ToString().Substring(0, 2) == "P1" && co.Term == Terms.Spring)).ToList();
                    break;
                case "Spring-P2":
                    model = courses.Where(c => c.CourseOccurrence.Any(co => co.Period.ToString().Substring(0, 2) == "P2" && co.Term == Terms.Spring)).ToList();
                    break;
                case "Spring-P3":
                    model = courses.Where(c => c.CourseOccurrence.Any(co => co.Period.ToString().Substring(0, 2) == "P3" && co.Term == Terms.Spring)).ToList();
                    break;
                case "Spring-P4":
                    model = courses.Where(c => c.CourseOccurrence.Any(co => co.Period.ToString().Substring(0, 2) == "P4" && co.Term == Terms.Spring)).ToList();
                    break;
                default:
                    model = courses;
                    break;
            }

            return PartialView("_CourseConflictTable", model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            var filters = formCollection.AllKeys.Where(key => formCollection[key].Contains("true")).ToList();

            return View(db.Course.ToList());
        }

        private List<Course> GetCourseListSorted()
        {
            var model = db.Course.Where(c => c.CourseOccurrence.Any(co => co.Year.Equals(GetCurrentEduYear()))).ToList();
            model = db.Course.OrderBy(c => c.CourseOccurrence.Min(co => co.Term))
                .ThenBy(c => c.CourseOccurrence.Min(co => co.Period))
                .ThenBy(co => co.CourseName).ToList();

            return model;
        }

        public static string GetCurrentEduYear()
        {
            string currentEduYear;
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            if (currentMonth > 6)
            {
                int nextYear = currentYear + 1;
                currentEduYear = currentYear.ToString() + "/" + nextYear.ToString();
            }
            else
            {
                int previousYear = currentYear - 1;
                currentEduYear = previousYear.ToString() + "/" + currentYear.ToString();
            }

            return currentEduYear;
        }
    }
}
