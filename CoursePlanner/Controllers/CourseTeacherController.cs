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
    public class CourseTeacherController : Controller
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        //
        // GET: /CourseTeacher/

        public ActionResult Index()
        {
            var courseteacher = db.CourseTeacher.Include(c => c.CourseOccurrence).Include(c => c.Teacher);
            return View(courseteacher.ToList());
        }

        //
        // GET: /CourseTeacher/Details/5

        public ActionResult Details(int id = 0)
        {
            CourseTeacher courseteacher = db.CourseTeacher.Find(id);
            if (courseteacher == null)
            {
                return HttpNotFound();
            }
            return View(courseteacher);
        }

        //
        // GET: /CourseTeacher/Create

        public ActionResult Create()
        {
            ViewBag.CourseOccurrenceId = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year");
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "TeacherName");
            return View();
        }

        //
        // POST: /CourseTeacher/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseTeacher courseteacher)
        {
            if (ModelState.IsValid)
            {
                db.CourseTeacher.Add(courseteacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseOccurrenceId = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", courseteacher.CourseOccurrenceId);
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "TeacherName", courseteacher.TeacherId);
            return View(courseteacher);
        }

        //
        // GET: /CourseTeacher/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CourseTeacher courseteacher = db.CourseTeacher.Find(id);
            if (courseteacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseOccurrenceId = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", courseteacher.CourseOccurrenceId);
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "TeacherName", courseteacher.TeacherId);
            return View(courseteacher);
        }

        //
        // POST: /CourseTeacher/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseTeacher courseteacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseteacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseOccurrenceId = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", courseteacher.CourseOccurrenceId);
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "TeacherName", courseteacher.TeacherId);
            return View(courseteacher);
        }

        //
        // GET: /CourseTeacher/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CourseTeacher courseteacher = db.CourseTeacher.Find(id);
            if (courseteacher == null)
            {
                return HttpNotFound();
            }
            return View(courseteacher);
        }

        //
        // POST: /CourseTeacher/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseTeacher courseteacher = db.CourseTeacher.Find(id);
            db.CourseTeacher.Remove(courseteacher);
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