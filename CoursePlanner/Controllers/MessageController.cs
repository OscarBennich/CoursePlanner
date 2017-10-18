using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoursePlanner.Models;
using System.Web.Security;

namespace CoursePlanner.Controllers
{
    public class MessageController : BaseController
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        //
        // GET: /Message/

        public ActionResult Index()
        {
            int teacherID = GetTeacherId();
            var message = db.Message.Where(m => m.TeacherReciever.TeacherId == teacherID).Include(m => m.CourseOccurrence).Include(m => m.ResponseToMessage).Include(m => m.TeacherReciever).Include(m => m.TeacherSender);

            return View(message.ToList());
        }

        //
        // GET: /Message/Details/5

        public ActionResult Details(int id = 0)
        {
            Message message = db.Message.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            // Set the read date of the message to DateTime.Now when the user clicks into the message
            message.MessageReadDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View(message);
        }

        //
        // GET: /Message/Create

        public ActionResult Create()
        {
            ViewBag.CourseOccurrenceID = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year");
            ViewBag.ResponseMessageID = new SelectList(db.Message, "MessageID", "MessageText");
            ViewBag.RecieverID = new SelectList(db.Teacher, "TeacherId", "TeacherName");

            int teacherID = GetTeacherId();
            ViewBag.SenderID = teacherID;
            
            return View();
        }

        //
        // POST: /Message/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Message message)
        {
            int teacherID = GetTeacherId();

            message.MessageSendDate = DateTime.Now;
            message.CourseOccurrence = db.CourseOccurrence.Where(c => c.CourseOccurrenceID == message.CourseOccurrenceID).FirstOrDefault();
            message.MessageType = MessageType.TextMessage;
            message.TeacherSender = db.Teacher.Where(t => t.TeacherId == teacherID).FirstOrDefault();
            message.TeacherReciever = db.Teacher.Where(t => t.TeacherId == message.RecieverID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Message.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseOccurrenceID = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", message.CourseOccurrenceID);
            ViewBag.ResponseMessageID = new SelectList(db.Message, "MessageID", "MessageText", message.ResponseMessageID);
            ViewBag.RecieverID = new SelectList(db.Teacher, "TeacherId", "TeacherName", message.RecieverID);

            ViewBag.SenderID = teacherID;

            return View(message);
        }

        //
        // GET: /Message/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Message message = db.Message.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            
            }
            ViewBag.CourseOccurrenceID = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", message.CourseOccurrenceID);
            ViewBag.ResponseMessageID = new SelectList(db.Message, "MessageID", "MessageText", message.ResponseMessageID);
            ViewBag.RecieverID = new SelectList(db.Teacher, "TeacherId", "TeacherName", message.RecieverID);
            ViewBag.SenderID = new SelectList(db.Teacher, "TeacherId", "TeacherName", message.SenderID);
            return View(message);
        }

        //
        // POST: /Message/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseOccurrenceID = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", message.CourseOccurrenceID);
            ViewBag.ResponseMessageID = new SelectList(db.Message, "MessageID", "MessageText", message.ResponseMessageID);
            ViewBag.RecieverID = new SelectList(db.Teacher, "TeacherId", "TeacherName", message.RecieverID);
            ViewBag.SenderID = new SelectList(db.Teacher, "TeacherId", "TeacherName", message.SenderID);
            return View(message);
        }

        //
        // GET: /Message/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Message message = db.Message.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        //
        // POST: /Message/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Message.Find(id);
            db.Message.Remove(message);
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