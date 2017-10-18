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
    public class RequestApprovalMessageController : Controller
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        //
        // GET: /RequestApprovalMessage/

        public ActionResult Index()
        {
            var requestapprovalmessage = db.RequestApprovalMessage.Include(r => r.BaseMessage).Include(r => r.CourseOccurrence);
            return View(requestapprovalmessage.ToList());
        }

        //
        // GET: /RequestApprovalMessage/Details/5

        public ActionResult Details(int id = 0)
        {
            RequestApprovalMessage requestapprovalmessage = db.RequestApprovalMessage.Find(id);
            if (requestapprovalmessage == null)
            {
                return HttpNotFound();
            }
            return View(requestapprovalmessage);
        }

        //
        // GET: /RequestApprovalMessage/Create

        public ActionResult Create()
        {
            ViewBag.BaseMessageID = new SelectList(db.BaseMessage, "BaseMessageID", "MessageText");
            ViewBag.CourseOccurrenceID = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year");
            return View();
        }

        //
        // POST: /RequestApprovalMessage/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestApprovalMessage requestapprovalmessage)
        {
            if (ModelState.IsValid)
            {
                db.RequestApprovalMessage.Add(requestapprovalmessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BaseMessageID = new SelectList(db.BaseMessage, "BaseMessageID", "MessageText", requestapprovalmessage.BaseMessageID);
            ViewBag.CourseOccurrenceID = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", requestapprovalmessage.CourseOccurrenceID);
            return View(requestapprovalmessage);
        }

        //
        // GET: /RequestApprovalMessage/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RequestApprovalMessage requestapprovalmessage = db.RequestApprovalMessage.Find(id);
            if (requestapprovalmessage == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaseMessageID = new SelectList(db.BaseMessage, "BaseMessageID", "MessageText", requestapprovalmessage.BaseMessageID);
            ViewBag.CourseOccurrenceID = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", requestapprovalmessage.CourseOccurrenceID);
            return View(requestapprovalmessage);
        }

        //
        // POST: /RequestApprovalMessage/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RequestApprovalMessage requestapprovalmessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestapprovalmessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BaseMessageID = new SelectList(db.BaseMessage, "BaseMessageID", "MessageText", requestapprovalmessage.BaseMessageID);
            ViewBag.CourseOccurrenceID = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", requestapprovalmessage.CourseOccurrenceID);
            return View(requestapprovalmessage);
        }

        //
        // GET: /RequestApprovalMessage/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RequestApprovalMessage requestapprovalmessage = db.RequestApprovalMessage.Find(id);
            if (requestapprovalmessage == null)
            {
                return HttpNotFound();
            }
            return View(requestapprovalmessage);
        }

        //
        // POST: /RequestApprovalMessage/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestApprovalMessage requestapprovalmessage = db.RequestApprovalMessage.Find(id);
            db.RequestApprovalMessage.Remove(requestapprovalmessage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResponseApprovalMessage(int requestApprovalMessageId, int senderId, int receiverId, string messageText, string response)
        {
            BaseMessage baseMessage = new BaseMessage();
            baseMessage.SenderID = senderId;
            baseMessage.RecieverID = receiverId;
            baseMessage.MessageText = messageText;
            baseMessage.MessageSendDate = DateTime.Now;

            ResponseApprovalMessage responseMessage = new ResponseApprovalMessage();
            responseMessage.RequestApprovalMessageID = requestApprovalMessageId;
            responseMessage.BaseMessage = baseMessage;

            responseMessage.Response = Convert.ToBoolean(response);

            db.ResponseApprovalMessage.Add(responseMessage);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

    }
}