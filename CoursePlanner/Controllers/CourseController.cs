using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoursePlanDraft.Models;

namespace CoursePlanner.Controllers
{
    public class CourseController : Controller
    {
        private CourseContext db = new CourseContext();

        //
        // GET: /Course/

        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        //
        // GET: /Course/Details/5

        public ActionResult Details(int id = 0)
        {
            CourseModel coursemodel = db.Courses.Find(id);
            if (coursemodel == null)
            {
                return HttpNotFound();
            }
            return View(coursemodel);
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Course/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseModel coursemodel)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(coursemodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coursemodel);
        }

        //
        // GET: /Course/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CourseModel coursemodel = db.Courses.Find(id);
            if (coursemodel == null)
            {
                return HttpNotFound();
            }
            return View(coursemodel);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseModel coursemodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursemodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coursemodel);
        }

        //
        // GET: /Course/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CourseModel coursemodel = db.Courses.Find(id);
            if (coursemodel == null)
            {
                return HttpNotFound();
            }
            return View(coursemodel);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseModel coursemodel = db.Courses.Find(id);
            db.Courses.Remove(coursemodel);
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