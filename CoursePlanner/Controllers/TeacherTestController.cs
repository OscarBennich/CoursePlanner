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
    public class TeacherTestController : Controller
    {
        private TeacherContext db = new TeacherContext();

        //
        // GET: /TeacherTest/

        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }

        //
        // GET: /TeacherTest/Details/5

        public ActionResult Details(int id = 0)
        {
            TeacherModel teachermodel = db.Teachers.Find(id);
            if (teachermodel == null)
            {
                return HttpNotFound();
            }
            return View(teachermodel);
        }

        //
        // GET: /TeacherTest/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TeacherTest/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeacherModel teachermodel)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teachermodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teachermodel);
        }

        //
        // GET: /TeacherTest/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TeacherModel teachermodel = db.Teachers.Find(id);
            if (teachermodel == null)
            {
                return HttpNotFound();
            }
            return View(teachermodel);
        }

        //
        // POST: /TeacherTest/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeacherModel teachermodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teachermodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teachermodel);
        }

        //
        // GET: /TeacherTest/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TeacherModel teachermodel = db.Teachers.Find(id);
            if (teachermodel == null)
            {
                return HttpNotFound();
            }
            return View(teachermodel);
        }

        //
        // POST: /TeacherTest/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherModel teachermodel = db.Teachers.Find(id);
            db.Teachers.Remove(teachermodel);
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