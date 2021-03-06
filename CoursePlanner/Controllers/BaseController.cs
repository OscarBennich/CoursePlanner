﻿using CoursePlanner.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace CoursePlanner.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        private CoursePlannerEntities db = new CoursePlannerEntities();
        //    //protected override ViewResult View(IView view, object model)
        //    //{
        //    //   // this.ViewBag.TeacherId = GetTeacherId();
        //    //    return base.View(view, model);
        //    //}
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ViewBag.CurrentTeacherId = GetTeacherId();

            IEnumerable<Comment> comments = GetCommentsNotifications();
            ViewBag.CommentsNotifications = comments;
            ViewBag.CommentsNotificationsCount = comments.Count();

            IEnumerable<RequestApprovalMessage> messages = GetUnreadMessages();
            ViewBag.messagesNotifications = messages;
            ViewBag.messagesNotificationsCount = messages.Count();

            //ViewBag.CurrentTeacherId = new Func<int, int>(GetTeacherId);
            ViewBag.CurrentTeacherId = GetTeacherId();
            base.OnActionExecuting(filterContext);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult readRemoveMessage()
        {
            //CourseModel coursemodel = db.Courses.Find(id);
            int currentTeacherId = GetTeacherId();
            IEnumerable<BaseMessage> baseMessage = db.BaseMessage.Where(b => b.RecieverID == currentTeacherId && b.MessageText.Contains("removed")).ToList();
            foreach (var item in baseMessage)
            {
                //BaseMessage existingBaseMessage = db.BaseMessage.Where(b => b.BaseMessageID == item.BaseMessageID).FirstOrDefault();
                item.MessageReadDate = DateTime.Now;
                db.Entry(item).State = EntityState.Modified;
               
            }
            
            db.SaveChanges();

            return RedirectToAction("Index" , "RequestApprovalMessage");
        }


        public int GetTeacherId()
        {
            int teacherId = (from m in db.Teacher
                             where m.TeacherUserId == WebSecurity.CurrentUserId
                             select m.TeacherId).FirstOrDefault();
            return teacherId;
        }

        #region Comments Notifications

        public List<Comment> GetCommentsNotifications()
        {
            int currentTeacherId = GetTeacherId();
            return db.Comment.Where(x => x.BaseMessage.RecieverID == currentTeacherId && x.BaseMessage.MessageDeletionDate == null && x.BaseMessage.MessageReadDate == null).OrderByDescending(x => x.BaseMessage.MessageSendDate).ToList();
        }

        #endregion

        #region Messages Notifications

        public List<RequestApprovalMessage> GetUnreadMessages()
        {
            int currentTeacherId = GetTeacherId();
            return db.RequestApprovalMessage.Where(x => x.BaseMessage.RecieverID == currentTeacherId && x.BaseMessage.MessageDeletionDate == null && x.BaseMessage.MessageReadDate == null).OrderByDescending(x => x.BaseMessage.MessageSendDate).ToList();
        }

        #endregion

    }
}

