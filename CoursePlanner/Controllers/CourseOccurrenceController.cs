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
            return View(courseoccurrence.ToList());
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
            return View(courseoccurrence);
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