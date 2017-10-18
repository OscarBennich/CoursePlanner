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
    public class BaseMessageController : Controller
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        //
        // GET: /BaseMessage/

        public ActionResult Index()
        {
            var basemessage = db.BaseMessage.Include(b => b.TeacherReciever).Include(b => b.TeacherSender);
            return View(basemessage.ToList());
        }

        //
        // GET: /BaseMessage/Details/5

        public ActionResult Details(int id = 0)
        {
            BaseMessage basemessage = db.BaseMessage.Find(id);
            if (basemessage == null)
            {
                return HttpNotFound();
            }
            return View(basemessage);
        }

        //
        // GET: /BaseMessage/Create

        public ActionResult Create()
        {
            ViewBag.RecieverID = new SelectList(db.Teacher, "TeacherId", "TeacherName");
            ViewBag.SenderID = new SelectList(db.Teacher, "TeacherId", "TeacherName");
            return View();
        }

        //
        // POST: /BaseMessage/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BaseMessage basemessage)
        {
            if (ModelState.IsValid)
            {
                db.BaseMessage.Add(basemessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RecieverID = new SelectList(db.Teacher, "TeacherId", "TeacherName", basemessage.RecieverID);
            ViewBag.SenderID = new SelectList(db.Teacher, "TeacherId", "TeacherName", basemessage.SenderID);
            return View(basemessage);
        }

        //
        // GET: /BaseMessage/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BaseMessage basemessage = db.BaseMessage.Find(id);
            if (basemessage == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecieverID = new SelectList(db.Teacher, "TeacherId", "TeacherName", basemessage.RecieverID);
            ViewBag.SenderID = new SelectList(db.Teacher, "TeacherId", "TeacherName", basemessage.SenderID);
            return View(basemessage);
        }

        //
        // POST: /BaseMessage/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BaseMessage basemessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basemessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecieverID = new SelectList(db.Teacher, "TeacherId", "TeacherName", basemessage.RecieverID);
            ViewBag.SenderID = new SelectList(db.Teacher, "TeacherId", "TeacherName", basemessage.SenderID);
            return View(basemessage);
        }

        //
        // GET: /BaseMessage/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BaseMessage basemessage = db.BaseMessage.Find(id);
            if (basemessage == null)
            {
                return HttpNotFound();
            }
            return View(basemessage);
        }

        //
        // POST: /BaseMessage/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaseMessage basemessage = db.BaseMessage.Find(id);
            db.BaseMessage.Remove(basemessage);
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