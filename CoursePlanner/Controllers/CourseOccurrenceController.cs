﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoursePlanner.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Objects.SqlClient;
using WebMatrix.WebData;

namespace CoursePlanner.Controllers
{
    public class CourseOccurrenceController : BaseController
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        //
        // GET: /CourseOccurrence/

        public ActionResult Index()
        {
            var courseoccurrence = db.CourseOccurrence.Include(c => c.Course).Include(c => c.Teacher);
            ViewBag.CurrentEduYear = GetCurrentEducationalYear();

            ViewBag.CourseAllocatedHours =
             new Func<int, int>(CourseAllocatedHoursFind);

            return View(courseoccurrence.ToList());
        }

        private int CourseAllocatedHoursFind(int courseOccurrenceId)
        {
            CourseOccurrence courseOccurrence = db.CourseOccurrence.Find(courseOccurrenceId);
            int CourseHoursTotalAllocated = CalculateCourseHoursAllocated(courseOccurrence);

            return CourseHoursTotalAllocated;
        }

        private int CalculateCourseHoursAllocated(CourseOccurrence courseOccurrence)
        {
            int CourseHoursTotalAllocated = 0;



            try
            {

                CourseHoursTotalAllocated += Convert.ToInt32(db.CourseTeacher.Where(x => x.CourseOccurrenceId == courseOccurrence.CourseOccurrenceID).Select(y => y.Hours).Sum());
            }
            catch { }




            return CourseHoursTotalAllocated;
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

            List<CourseTeacher> teachersCourse = GetTeachers(courseoccurrence.CourseOccurrenceID).ToList();

            ViewBag.CourseOccurencesHistory = courseOccurencesHistory;

            ViewBag.teachersCourse = teachersCourse;
            ViewBag.TeachersHistory =
                new Func<int, IEnumerable<Teacher>>(GetTeacherHistory);

            ViewBag.GetCourseResponsibleName =
              new Func<int, string>(GetCourseResponsibleNameFind);

            ViewBag.IsCourseResponsibleName =
              new Func<int, int, string>(IsCourseResponsibleNameFind);

            ViewBag.TeachersForCourseResponsible = db.CourseTeacher.Where(c => c.CourseOccurrenceId == id && c.CourseOccurrence.Year == CurrentEduYear).Select(c => c.Teacher).ToList();

            ViewBag.ResponseApprovalMessageList = GetResponseApprovalMessage(teachersCourse, courseoccurrence);

            return View(courseoccurrence);
        }

        private IEnumerable<ResponseApprovalMessage> GetResponseApprovalMessage(List<CourseTeacher> teachersCourse, CourseOccurrence courseOccurrence)
        {
            List<ResponseApprovalMessage> responseApprovalMessageList = new List<ResponseApprovalMessage>();

            foreach(Teacher teacher in teachersCourse.Select(t => t.Teacher).ToList())
            {
                ResponseApprovalMessage responseToAdd = (ResponseApprovalMessage)db.ResponseApprovalMessage.Where(x => x.BaseMessage.SenderID == teacher.TeacherId && x.RequestApprovalMessage.CourseOccurrence.CourseOccurrenceID == courseOccurrence.CourseOccurrenceID && x.Response != null && x.BaseMessage.MessageReadDate != null).OrderBy(x => x.BaseMessage.MessageReadDate).FirstOrDefault();
                responseApprovalMessageList.Add(responseToAdd);
            }

            return responseApprovalMessageList;
        }

        private IEnumerable<CourseOccurrence> GetCoursesHistory(int courseID, string year)
        {
            return db.CourseOccurrence.Where(c => c.CourseID == courseID && c.Year != year).ToList();
        }


        private IEnumerable<Teacher> GetTeacherHistory(int courseOccurenceID)
        {

            return db.CourseTeacher.Where(c => c.CourseOccurrenceId == courseOccurenceID).Select(c => c.Teacher).ToList();
        }

        private IEnumerable<CourseTeacher> GetTeachers(int courseOccurenceID)
        {

            return db.CourseTeacher.Where(c => c.CourseOccurrenceId == courseOccurenceID).Include(c => c.Teacher);
        }

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
            catch (Exception E)
            {
                return "no course responsible";
            }
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
        public ActionResult EditCourseResponsible(int courseID, int newcourseresponsibleId, int hours = 0)
        {   
            CourseOccurrence courseoccurrence = db.CourseOccurrence.Find(courseID);

            Teacher newTeacher = db.Teacher.Where(t => t.TeacherId == newcourseresponsibleId).FirstOrDefault();

            // Create CourseTeacher instance
            string currentYear = GetCurrentEducationalYear();
            List<Teacher> teacherList = db.CourseTeacher.Where(c => c.CourseOccurrenceId == courseID && courseoccurrence.Year == currentYear).Select(t => t.Teacher).ToList();

            // If the list of current teachers does NOT contain the course responsible, we add this teacher to the CourseTeacher table
            if (!teacherList.Contains(newTeacher))
            {
                CourseTeacher courseTeacher = new CourseTeacher()
                {
                    CourseOccurrence = courseoccurrence,
                    CourseOccurrenceId = courseoccurrence.CourseOccurrenceID,
                    Teacher = newTeacher,
                    TeacherId = newTeacher.TeacherId,
                    Hours = hours
                };

                db.CourseTeacher.Add(courseTeacher);
            }
            else 
            {
                CourseTeacher courseTeacher = db.CourseTeacher.Where(c => c.TeacherId == newTeacher.TeacherId && c.CourseOccurrenceId == courseoccurrence.CourseOccurrenceID && c.CourseOccurrence.Year == currentYear).FirstOrDefault();
                
                //Checking so that a current teacher doesn't get their hours reset if the field is not filled in
                if(hours != 0)
                {
                    courseTeacher.Hours = hours;
                }        
            }

            courseoccurrence.CourseResponsibleID = newTeacher.TeacherId;
            db.Entry(courseoccurrence).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details/" + courseoccurrence.CourseOccurrenceID);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int cid, int tid)
        {
            CourseTeacher courseteacher = db.CourseTeacher.Where(c => c.CourseOccurrenceId == cid && c.TeacherId == tid).FirstOrDefault();
            if (courseteacher == null)
            {
                return HttpNotFound();
            }
            db.CourseTeacher.Remove(courseteacher);
            db.SaveChanges();
            return RedirectToAction("Details/" + courseteacher.CourseOccurrenceId);

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
            return RedirectToAction("Details/" + courseoccurrence.CourseOccurrenceID);
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
            return RedirectToAction("Details/" + courseoccurrence.CourseOccurrenceID);
        }

        [Authorize(Roles = "Study Director")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourseTeacher(int cid, int tid)
        {

            CourseTeacher courseteacher = db.CourseTeacher.Where(c => c.CourseOccurrenceId == cid && c.TeacherId == tid).FirstOrDefault();
            db.CourseTeacher.Remove(courseteacher);
            db.SaveChanges();
            return RedirectToAction("Details/" + courseteacher.CourseOccurrenceId);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Study Director")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllocateTeacherHours(int cid, int tid, int hours)
        {
            var courseteacher = new CourseTeacher();
            courseteacher.CourseOccurrenceId = cid;
            courseteacher.TeacherId = tid;
            courseteacher.Hours = hours;
            if (ModelState.IsValid)
            {
                db.CourseTeacher.Add(courseteacher);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    EditExistingHours(courseteacher);
                }
            }
            return RedirectToAction("Details/" + courseteacher.CourseOccurrenceId);
        }
         

        //
        // POST: /CourseTeacher/Edit/5
        [Authorize(Roles = "Study Director")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditExistingHours(CourseTeacher courseteacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseteacher).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Details/" + courseteacher.CourseOccurrenceId);

        }

        private string GetAcademicYear()
        {
            string academicYear = DateTime.Today.Year + "/" + (DateTime.Today.Year + 1);

            if (DateTime.Today.Month <= 6)
            {
                academicYear = (DateTime.Today.Year - 1) + "/" + DateTime.Today.Year;
            }

            return academicYear;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRequestApprovalMessage(int courseOccurrenceID, int[] approvalMessageSelection)
        {
            string currentYear = GetCurrentEducationalYear();

            // Creating multiple messages, one for each selected teacher
            foreach (int receiverID in approvalMessageSelection)
            {   
                // Checking if there are previous undread messages that are about the same course occurrence sent to the same teacher
                List<RequestApprovalMessage> requestApprovalMessageList = db.RequestApprovalMessage.Where(m => m.CourseOccurrenceID == courseOccurrenceID && m.CourseOccurrence.Year == currentYear && m.BaseMessage.RecieverID == receiverID).ToList();
                foreach(RequestApprovalMessage message in requestApprovalMessageList)
                {   
                    // If the messages are unread then they get removed 
                    if(message.BaseMessage.MessageReadDate == null)
                    {   
                        BaseMessage baseMessageToDelete = db.BaseMessage.Where(m => m.BaseMessageID == message.BaseMessageID).FirstOrDefault();
                        baseMessageToDelete.MessageDeletionDate = DateTime.Now;
                    }
                }

                BaseMessage baseMessage = new BaseMessage();
                baseMessage.SenderID = GetTeacherId();
                baseMessage.RecieverID = receiverID;

                int currentHours = db.CourseTeacher.Where(c => c.TeacherId == receiverID && c.CourseOccurrenceId == courseOccurrenceID && c.CourseOccurrence.Year == currentYear).Select(c => c.Hours).FirstOrDefault();
                baseMessage.MessageText = "You need to approve or reject the changes that have been made to this course. Your hours are now " + currentHours.ToString() + ".";

                int courseResponsibleID = Convert.ToInt32(db.CourseOccurrence.Where(c => c.CourseOccurrenceID == courseOccurrenceID && c.Year == currentYear).Select(c => c.CourseResponsibleID).FirstOrDefault());
           
                // Case if the reciever is responsible for the course
                if (receiverID == courseResponsibleID)
                {
                    baseMessage.MessageText += " You are also the course responsible for this course.";
                }
                
                baseMessage.MessageSendDate = DateTime.Now;

                RequestApprovalMessage requestMessage = new RequestApprovalMessage();
                requestMessage.CourseOccurrenceID = courseOccurrenceID;
                requestMessage.BaseMessage = baseMessage;
                db.RequestApprovalMessage.Add(requestMessage);
            }

            db.SaveChanges();

            return RedirectToAction("Details/" + courseOccurrenceID);
        }
    }
}
