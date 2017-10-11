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
    public class CourseOccurrenceController : Controller
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        //
        // GET: /CourseOccurrence/

        public ActionResult Index()
        {
            var courseoccurrence = db.CourseOccurrence.Include(c => c.Course).Include(c => c.Teacher);
            ViewBag.CurrentEduYear = GetCurrentEducationalYear();
            return View(courseoccurrence.ToList());
        }

        private string GetCurrentEducationalYear()
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

        //
        // GET: /CourseOccurrence/Details/5

        public ActionResult Details(int id = 0)
        {
            CourseOccurrence courseoccurrence = db.CourseOccurrence.Find(id);
            if (courseoccurrence == null)
            {
                return HttpNotFound();
            }

            string CurrentEduYear = GetCurrentEducationalYear();

            List<CourseOccurrence> courseOccurencesHistory = GetCoursesHistory(courseoccurrence.CourseID, CurrentEduYear).ToList();
            
            List<Teacher> teachersCourse = GetTeachers(courseoccurrence.CourseOccurrenceID).ToList();

            ViewBag.CourseOccurencesHistory = courseOccurencesHistory;

<<<<<<< HEAD
            ViewBag.teachersCourse = teachersCourse;
            ViewBag.TeachersHistory = 
                new Func<int, IEnumerable<Teacher>>(GetTeacherHistory);

            ViewBag.GetCourseResponsibleName =
              new Func<int, string>(GetCourseResponsibleNameFind);

            ViewBag.IsCourseResponsibleName =
              new Func<int, int,string>(IsCourseResponsibleNameFind);
            ViewBag.TeachersForCourseResponsible = db.Teacher.ToList();

          
            return View(courseoccurrence);
        }
		
        private IEnumerable<Teacher> GetTeacherHistory(int courseOccurenceID)
        {

            return db.CourseTeacher.Where(c => c.CourseOccurrenceId == courseOccurenceID).Select(c => c.Teacher).ToList();
        }

        private IEnumerable<Teacher> GetTeachers(int courseOccurenceID)
        {

            return db.CourseTeacher.Where(c => c.CourseOccurrenceId == courseOccurenceID).Select(c => c.Teacher);

        public string IsCourseResponsibleNameFind(int teacherId, int courseOccurrenceID)
        {
            var courseResponsibleID = (from m in db.CourseOccurrence
                                       where m.CourseOccurrenceID == courseOccurrenceID
                                       select m.CourseResponsibleID).Single();

            if (courseResponsibleID == teacherId)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }

        public string GetCourseResponsibleNameFind(int id)
        {
            try
            {
                var CourseResponsibleName = (from m in db.Teacher
                                             where m.TeacherId == id
                                             select m.TeacherName).Single();
                return Convert.ToString(CourseResponsibleName);
            }
            catch(Exception E)
            {
                return "no course responsible";
            }      
        }



        public string IsCourseResponsibleNameFind(int teacherId,int courseOccurrenceID)
        {
            var courseResponsibleID = (from m in db.CourseOccurrence
                                       where m.CourseOccurrenceID == courseOccurrenceID
                                      select m.CourseResponsibleID).Single();

            if (courseResponsibleID == teacherId)
            {
                return "Yes";
            }else{
                return "No";
            }
            

        }

        public string GetCourseResponsibleNameFind(int id)
        {
            var CourseResponsibleName = (from m in db.Teacher
                                         where m.TeacherId == id
                                         select m.TeacherName).Single();


            return Convert.ToString(CourseResponsibleName);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBudget(int id, int newBudget)
        {
            //CourseModel coursemodel = db.Courses.Find(id);
            CourseOccurrence courseoccurrence = db.CourseOccurrence.Find(id);
            courseoccurrence.Budget = newBudget;
            db.Entry(courseoccurrence).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details/" + courseoccurrence.CourseOccurrenceID);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourseResponsible(int id, int newcourseresponsibleId)
        {
            CourseOccurrence courseoccurrence = db.CourseOccurrence.Find(id);

            var newteacher = (from m in db.Teacher
                              where m.TeacherId == newcourseresponsibleId
                              select m.TeacherId).Single();

            courseoccurrence.CourseResponsibleID = newteacher;
            db.Entry(courseoccurrence).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details/" + courseoccurrence.CourseOccurrenceID);
        }

        //
        // GET: /CourseOccurrence/Create

        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Course, "CourseId", "CourseCode");
            ViewBag.CourseResponsibleID = new SelectList(db.Teacher, "TeacherId", "TeacherName");
            return View();
        }

        //
        // POST: /CourseOccurrence/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseOccurrence courseoccurrence)
        {
            if (ModelState.IsValid)
            {
                db.CourseOccurrence.Add(courseoccurrence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Course, "CourseId", "CourseCode", courseoccurrence.CourseID);
            ViewBag.CourseResponsibleID = new SelectList(db.Teacher, "TeacherId", "TeacherName", courseoccurrence.CourseResponsibleID);
            return View(courseoccurrence);
        }

        //
        // GET: /CourseOccurrence/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CourseOccurrence courseoccurrence = db.CourseOccurrence.Find(id);
            if (courseoccurrence == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Course, "CourseId", "CourseCode", courseoccurrence.CourseID);
            ViewBag.CourseResponsibleID = new SelectList(db.Teacher, "TeacherId", "TeacherName", courseoccurrence.CourseResponsibleID);
            return View(courseoccurrence);
        }

        //
        // POST: /CourseOccurrence/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseOccurrence courseoccurrence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseoccurrence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Course, "CourseId", "CourseCode", courseoccurrence.CourseID);
            ViewBag.CourseResponsibleID = new SelectList(db.Teacher, "TeacherId", "TeacherName", courseoccurrence.CourseResponsibleID);
            return View(courseoccurrence);
        }

        //
        // GET: /CourseOccurrence/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CourseOccurrence courseoccurrence = db.CourseOccurrence.Find(id);
            if (courseoccurrence == null)
            {
                return HttpNotFound();
            }
            return View(courseoccurrence);
        }

        //
        // POST: /CourseOccurrence/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseOccurrence courseoccurrence = db.CourseOccurrence.Find(id);
            db.CourseOccurrence.Remove(courseoccurrence);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


       
    }
}
