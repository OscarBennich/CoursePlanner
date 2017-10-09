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

            ViewBag.TeachingHoursAvailableFall = CalculateTeachingHoursAvailable(teacher, baseAnnualWorkingHours, Terms.Fall);
            ViewBag.TeachingHoursAvailableSpring = CalculateTeachingHoursAvailable(teacher, baseAnnualWorkingHours, Terms.Spring);

            ViewBag.TeachingHoursAllocatedFall = CalculateTeachingHoursAllocated(teacher, Terms.Fall);
            ViewBag.TeachingHoursAllocatedSpring = CalculateTeachingHoursAllocated(teacher, Terms.Spring);

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

        private double CalculateTeachingHoursAvailable(Teacher teacher, int baseAnnualWorkingHours, Terms term) //should split into terms, testing for now
        {   
            int teachingHoursAvailable = 0;
            double totalReductionPercentage = 0;

            if(term.Equals(Terms.Fall)) 
            {   
                // First split the base amount in 2 (2 terms in one year)
                // Then multiply by the total percentage for that term (for example *0,5)
                // This gives us the base available hours for this teacher for this term
                teachingHoursAvailable = Convert.ToInt32(baseAnnualWorkingHours/2 * teacher.TotalPercentageFall);

                try // Error handling for the case that the teacher has no reductions
                {
                    // Once we have the base amount we apply the sum of all other reductions (Research, Assignments, Administration and Other reductions) for that term
                    // For example a professor (per default) would have 50% research and 10% administration so the sum is 0,5 + 0,1 = 0,6
                    totalReductionPercentage = (double)db.TeacherReduction.Where(x => x.TeacherId == teacher.TeacherId && x.Term == Terms.Fall).Select(y => y.Percentage).Sum();
                }
                catch { }
            }
            else 
            {   
                teachingHoursAvailable = Convert.ToInt32(baseAnnualWorkingHours/2 * teacher.TotalPercentageSpring);

                try 
                {
                    totalReductionPercentage = (double)db.TeacherReduction.Where(x => x.TeacherId == teacher.TeacherId && x.Term == Terms.Spring).Select(y => y.Percentage).Sum();
                }
                catch { }
            }

            // Finally we apply the total reduction to the base value for the term
            // For example a base of 200 hours for this term for a professor would result in 
            // 200 * 0,6 = 120 hours
            return teachingHoursAvailable = Convert.ToInt32(teachingHoursAvailable * (1- totalReductionPercentage));
        }

        private int CalculateTeachingHoursAllocated(Teacher teacher, Terms term)
        {
            int teachingHoursAllocated = 0;

            string academicYear = DateTime.Today.Year + "-" + (DateTime.Today.Year + 1);

            if (DateTime.Today.Month <= 6)
            {
                academicYear = (DateTime.Today.Year - 1) + "-" + DateTime.Today.Year;
            }

            if (term.Equals(Terms.Fall)) 
            {
                try
                {   
                    // Calculate the sum of all hours allocated for this teacher in all course occurances which match
                    // with the teacherID and where the term matches (Fall in this case)
                    teachingHoursAllocated += Convert.ToInt32(db.CourseTeacher.Where(x => x.TeacherId == teacher.TeacherId && x.CourseOccurrence.Term == Terms.Fall && x.CourseOccurrence.Year == academicYear).Select(y => y.Hours).Sum());
                }
                catch { }
            }
            else 
            {
                try
                {
                    teachingHoursAllocated += Convert.ToInt32(db.CourseTeacher.Where(x => x.TeacherId == teacher.TeacherId && x.CourseOccurrence.Term == Terms.Spring && x.CourseOccurrence.Year == academicYear).Select(y => y.Hours).Sum());
                }
                catch { }
            }

            return teachingHoursAllocated;
        }

        private IEnumerable<CourseOccurrence> GetTeacherCourses(int teacherID, Terms term)
        {
            string academicYear = DateTime.Today.Year + "-" + (DateTime.Today.Year + 1);

            if (DateTime.Today.Month <= 6)
            {
                academicYear = (DateTime.Today.Year-1) + "-" + DateTime.Today.Year;
            }

            return db.CourseTeacher.Where(c => c.TeacherId == teacherID && c.CourseOccurrence.Term == term && c.CourseOccurrence.Year == academicYear).Select(c => c.CourseOccurrence);
        }
    }
}