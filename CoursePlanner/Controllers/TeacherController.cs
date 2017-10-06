using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoursePlanner.Models;

namespace CoursePlanner.Controllers
{
    public class TeacherController : Controller
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        //
        // GET: /Teacher/

        public ActionResult Index()
        {
            return View(db.Teacher.ToList());
        }

        //
        // GET: /Teacher/Details/5

        public ActionResult Details(int id = 0)
        {
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            int baseAnnualWorkingHours = GetBaseAnnualHours(teacher.TeacherDateOfBirth);
            ViewBag.BaseAnnualWorkingHours = baseAnnualWorkingHours;

            List<CourseOccurrence> courseOccurencesFall = GetTeacherCourses(teacher.TeacherId, Terms.Fall).ToList();
            List<CourseOccurrence> courseOccurencesSpring = GetTeacherCourses(teacher.TeacherId, Terms.Spring).ToList();

            ViewBag.CourseOccurencesFall = courseOccurencesFall;
            ViewBag.CourseOccurencesSpring = courseOccurencesSpring;

            return View(teacher);
        }

        //
        // GET: /Teacher/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Teacher/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teacher.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        //
        // GET: /Teacher/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //
        // POST: /Teacher/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        //
        // GET: /Teacher/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //
        // POST: /Teacher/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teacher.Find(id);
            db.Teacher.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private int GetBaseAnnualHours(DateTime teacherDateOfBirth)
        {
            int age = GetAge(teacherDateOfBirth);

            if (age >= 40)
            {
                return 1700;
            }
            else if (age >= 30 && age < 40)
            {
                return 1735;
            }
            else if (age > 0 && age < 30)
            {
                return 1756;
            }
            else //Shouldn't get here ever (age is below 0)
            {
                return 0;
            }
        }

        private int GetAge(DateTime teacherDateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - teacherDateOfBirth.Year;
            if (DateTime.Now.DayOfYear < teacherDateOfBirth.DayOfYear)
            {
                return age - 1;
            }
            else
            {
                return age;
            }
        }

        private int CalculateTeachingHoursAvailable(Teacher teacher, int baseAnnualWorkingHours, Terms term)
        {
            int teachingHoursAvailable = baseAnnualWorkingHours;

            if (term.Equals(Terms.Fall))
            {
                //teacher.
                //teachingHoursAvailable 
            }
            else
            {

            }

            //if(teacher.TeacherPosition.Equals(Positions.JuniorLecturer)
            return 0;
        }

        private IEnumerable<CourseOccurrence> GetTeacherCourses(int teacherID, Terms term)
        {
            return db.CourseTeacher.Where(c => c.TeacherId == teacherID && c.CourseOccurrence.Term == term).Select(c => c.CourseOccurrence);
            //return db.CourseOccurrence.Where(c => c.CourseTeacher.Where(t => t.TeacherId == teacherID).FirstOrDefault && c.Term == term);
        }
    }
}