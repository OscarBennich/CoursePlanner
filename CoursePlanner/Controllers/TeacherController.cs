using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoursePlanner.Models;
using WebMatrix.WebData;

namespace CoursePlanner.Controllers
{
    [Authorize]
    public class TeacherController : BaseController
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        #region Show View Methods

        // GET: /Teacher/
        public ActionResult Index()
        {
            ViewBag.BalanceInTerm = new Func<Teacher, Terms, int>(BalanceInTerm);
            ViewBag.BalancePerPeriodInTerm = new Func<Teacher, Terms, int[]>(BalancePerPeriodTerm);
            return View(db.Teacher.ToList());
        }

        // GET: /Teacher/Details/5
        public ActionResult Details(int id = 0)
        {
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            #region Comments

            IEnumerable<Comment> publicComments = GetComments(teacher);
            ViewBag.publicComments = publicComments;
            ViewBag.publicCommentsCount = publicComments.Count();
            IEnumerable<Comment> privateComments = GetComments(teacher, false);
            ViewBag.privateComments = privateComments;
            ViewBag.privateCommentsCount = privateComments.Count();

            #endregion

            #region ViewBag for teacher details

            ViewBag.TeachingHoursAvailableFall = TotalTeachingHoursTerm(teacher, Terms.Fall); ;         
            ViewBag.TeachingHoursAvailableSpring = TotalTeachingHoursTerm(teacher, Terms.Spring);       

            ViewBag.CourseOccurencesFall = GetTeacherCourses(teacher, Terms.Fall);
            ViewBag.CourseOccurencesSpring = GetTeacherCourses(teacher, Terms.Spring);

            ViewBag.HoursFallInPeriods = HoursInPeriodsOnTerm(teacher, Terms.Fall); ;
            ViewBag.HoursSpringInPeriods = HoursInPeriodsOnTerm(teacher, Terms.Spring);

            ViewBag.BaseAnnualWorkingHours = GetBaseAnnualHours(teacher.TeacherDateOfBirth);

            ViewBag.TeachingHoursAllocatedFall = TeachingHoursAllocatedTerm(teacher, Terms.Fall);
            ViewBag.TeachingHoursAllocatedSpring = TeachingHoursAllocatedTerm(teacher, Terms.Spring);

            ViewBag.TotalHoursAllocatedResearchFall = HoursAllocatedPerReduction(teacher, Terms.Fall, ReductionTypes.Research);             
            ViewBag.TotalHoursAllocatedAdministrationFall = HoursAllocatedPerReduction(teacher, Terms.Fall, ReductionTypes.Administration); 
            ViewBag.TotalHoursAllocatedAssignmentsFall = HoursAllocatedPerReduction(teacher, Terms.Fall, ReductionTypes.Assignments);       
            ViewBag.TotalHoursAllocatedOtherFall = HoursAllocatedPerReduction(teacher, Terms.Fall, ReductionTypes.Other);                   

            ViewBag.TotalHoursAllocatedResearchSpring = HoursAllocatedPerReduction(teacher, Terms.Spring, ReductionTypes.Research);                 
            ViewBag.TotalHoursAllocatedAdministrationSpring = HoursAllocatedPerReduction(teacher, Terms.Spring, ReductionTypes.Administration);     
            ViewBag.TotalHoursAllocatedAssignmentsSpring = HoursAllocatedPerReduction(teacher, Terms.Spring, ReductionTypes.Assignments);           
            ViewBag.TotalHoursAllocatedOtherSpring = HoursAllocatedPerReduction(teacher, Terms.Spring, ReductionTypes.Other);                       


            int TotalTeachingHoursFall = TotalTeachingHoursTerm(teacher, Terms.Fall);
            int TotalTeachingHoursSpring = TotalTeachingHoursTerm(teacher, Terms.Spring);

            ViewBag.TotalTeachingHoursFall = TotalTeachingHoursFall;
            ViewBag.TotalTeachingHoursSpring = TotalTeachingHoursSpring;
            ViewBag.ExpectedPeriodHoursFall = TotalTeachingHoursFall / 4;
            ViewBag.ExpectedPeriodHoursSpring = TotalTeachingHoursSpring / 4;

            ViewBag.GetCourseResponsibleName = new Func<int, string>(GetCourseResponsibleNameFromIdFind);

            ViewBag.BalancePerPeriodFall = BalancePerPeriodTerm(teacher, Terms.Fall);
            ViewBag.BalancePerPeriodSpring = BalancePerPeriodTerm(teacher, Terms.Spring);

#endregion

            return View(teacher);
        }

        // GET: /Teacher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teacher.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: /Teacher/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: /Teacher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: /Teacher/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //
        // POST: /Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teacher.Find(id);
            db.Teacher.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(int senderId, int receiverId, string messageText, string publicComment)
        {
            BaseMessage baseMessage = new BaseMessage();
            baseMessage.SenderID = senderId;
            baseMessage.RecieverID = receiverId;
            baseMessage.MessageText = messageText;
            baseMessage.MessageSendDate = DateTime.Now;

            Comment comment = new Comment();
            comment.BaseMessage = baseMessage;
            comment.IsPublic = Convert.ToBoolean(publicComment);

            db.Comment.Add(comment);
            db.SaveChanges();

            return new RedirectResult(Url.Action("Details/") + receiverId.ToString() + "#comments");
        }

        public ActionResult DeleteComment(int toDeleteComment)
        {
            var deletedComment = db.Comment.Where(x => x.BaseMessageID == toDeleteComment).FirstOrDefault();

            if (ModelState.IsValid)
            {
                deletedComment.BaseMessage.MessageDeletionDate = DateTime.Now;
                db.SaveChanges();
            }

            return new RedirectResult(Url.Action("Details/") + deletedComment.BaseMessage.RecieverID + "#comments");
        }

        public ActionResult ReadComment(int toReadComment)
        {
            var deletedComment = db.Comment.Where(x => x.CommentID == toReadComment).FirstOrDefault();

            if (ModelState.IsValid)
            {
                deletedComment.BaseMessage.MessageReadDate = DateTime.Now;
                db.SaveChanges();
            }

            return new RedirectResult(Url.Action("Details/") + deletedComment.BaseMessage.RecieverID + "#comments");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        #endregion

        #region Methods to fill viewbags

        public int TotalTeachingHoursTerm(Teacher teacher, Terms term)
        {
            return term == Terms.Fall ?
                (int)(Math.Round((decimal)((GetBaseAnnualHours(teacher.TeacherDateOfBirth) / 2) * teacher.TotalPercentageFall * (1 - GetTotalReductionPercentage(teacher, term))), 0, MidpointRounding.AwayFromZero))
                : (int)(Math.Round((decimal)((GetBaseAnnualHours(teacher.TeacherDateOfBirth) / 2) * teacher.TotalPercentageSpring * (1 - GetTotalReductionPercentage(teacher, term))), 0, MidpointRounding.AwayFromZero));
        }

        public int HoursAllocatedPerReduction(Teacher teacher, Terms term, ReductionTypes reduction)
        {
            return term == Terms.Fall ?
                (int)(Math.Round((decimal)((GetBaseAnnualHours(teacher.TeacherDateOfBirth) / 2) * teacher.TotalPercentageFall * GetReductionByTypePercentage(teacher, term, reduction)), 0, MidpointRounding.AwayFromZero))
                : (int)(Math.Round((decimal)((GetBaseAnnualHours(teacher.TeacherDateOfBirth) / 2) * teacher.TotalPercentageSpring * GetReductionByTypePercentage(teacher, term, reduction)), 0, MidpointRounding.AwayFromZero));
        }
        
        public double GetTotalReductionPercentage(Teacher teacher, Terms term)
        {
            return Math.Round((double)teacher.TeacherReduction.Where(r => r.Term == term).Sum(tr => tr.Percentage), 2, MidpointRounding.AwayFromZero);
        }

        public double GetReductionByTypePercentage(Teacher teacher, Terms term, ReductionTypes reduction)
        {
            return (double)(teacher.TeacherReduction.Where(r => r.Term == term && r.ReductionType == reduction).Sum(tr => tr.Percentage));
        }

        public int TeachingHoursAllocatedTerm(Teacher teacher, Terms term)
        {
            return term == Terms.Fall ?
                (int)(Math.Round((decimal)(teacher.CourseTeacher.Where(ct => ct.CourseOccurrence.Term == term && ct.CourseOccurrence.Year == GetCurrentEducationalYear()).Sum(co => co.Hours)), 0, MidpointRounding.AwayFromZero))
                : (int)(Math.Round((decimal)(teacher.CourseTeacher.Where(ct => ct.CourseOccurrence.Term == term && ct.CourseOccurrence.Year == GetCurrentEducationalYear()).Sum(co => co.Hours)), 0, MidpointRounding.AwayFromZero));
        }

        public int[] HoursInPeriodsOnTerm(Teacher teacher, Terms term)
        {
            var teacherCourses = GetTeacherCourses(teacher, term);
            int[] hours = { 0, 0, 0, 0 };

            foreach (CourseOccurrence course in teacherCourses)
            {
                var periods = course.Period.ToString();
                // if the course starts in period P1
                if (periods.Substring(0, 2) == "P1")
                {
                    if (periods.Length == 2)
                    {
                        hours[0] += HoursInCourse(course, teacher);
                    }
                    else if (periods.Length == 4)
                    {
                        hours[0] += HoursInCourse(course, teacher) / 2;
                        hours[1] += HoursInCourse(course, teacher) / 2;
                    }
                    else if (periods.Length == 6)
                    {
                        hours[0] += HoursInCourse(course, teacher) / 3;
                        hours[1] += HoursInCourse(course, teacher) / 3;
                        hours[2] += HoursInCourse(course, teacher) / 3;
                    }
                    else if (periods.Length == 8)
                    {
                        hours[0] += HoursInCourse(course, teacher) / 4;
                        hours[1] += HoursInCourse(course, teacher) / 4;
                        hours[2] += HoursInCourse(course, teacher) / 4;
                        hours[3] += HoursInCourse(course, teacher) / 4;
                    }
                }
                // if the course starts in period P2
                else if (periods.Substring(0, 2) == "P2")
                {
                    if (periods.Length == 2)
                    {
                        hours[1] += HoursInCourse(course, teacher);
                    }
                    else if (periods.Length == 4)
                    {
                        hours[1] += HoursInCourse(course, teacher) / 2;
                        hours[2] += HoursInCourse(course, teacher) / 2;
                    }
                    else if (periods.Length == 6)
                    {
                        hours[1] += HoursInCourse(course, teacher) / 3;
                        hours[2] += HoursInCourse(course, teacher) / 3;
                        hours[3] += HoursInCourse(course, teacher) / 3;
                    }
                }
                // if the course starts in period P3
                else if (periods.Substring(0, 2) == "P3")
                {
                    if (periods.Length == 2)
                    {
                        hours[2] += HoursInCourse(course, teacher);
                    }
                    else if (periods.Length == 4)
                    {
                        hours[2] += HoursInCourse(course, teacher) / 2;
                        hours[3] += HoursInCourse(course, teacher) / 2;
                    }
                }
                // if the course starts in period P4
                else if (periods.Substring(0, 2) == "P4")
                {
                    if (periods.Length == 2)
                    {
                        hours[3] += HoursInCourse(course, teacher);
                    }
                }

            }
            return hours;
        }

        private int[] BalancePerPeriodTerm(Teacher teacher, Terms term)
        {
            var totalHoursPeriods = TotalTeachingHoursTerm(teacher, term) / 4;
            var hoursInPeriod = HoursInPeriodsOnTerm(teacher, term);

            var conflictsInPeriods = new int[4] { 
                (totalHoursPeriods - hoursInPeriod[0]),
                (totalHoursPeriods - hoursInPeriod[1]),
                (totalHoursPeriods - hoursInPeriod[2]),
                (totalHoursPeriods - hoursInPeriod[3])
                };

            return conflictsInPeriods;
        }

        public int BalanceInTerm(Teacher teacher, Terms term)
        {
            return teacher == null ? 
                0 
                : (TotalTeachingHoursTerm(teacher, term) - TeachingHoursAllocatedTerm(teacher, term));
        }

        public int HoursInCourse(CourseOccurrence course, Teacher teacher)
        {
            return (int)course.CourseTeacher.Where(c => c.CourseOccurrence == course && c.Teacher == teacher).Select(y => y.Hours).FirstOrDefault();
        }

        public int GetBaseAnnualHours(DateTime teacherDateOfBirth)
        {
            int age = GetAge(teacherDateOfBirth);

            if (age >= 40)
            {
                return 1700;
            }
            else if (age >= 30 && age < 40)
            {
                return 1735;
            }
            else if (age > 0 && age < 30)
            {
                return 1756;
            }
            else //Shouldn't get here ever (age is below 0)
            {
                return 0;
            }
        }

        public int GetAge(DateTime teacherDateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - teacherDateOfBirth.Year;
            if (DateTime.Now.DayOfYear < teacherDateOfBirth.DayOfYear)
            {
                return age - 1;
            }
            else
            {
                return age;
            }
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

        private IEnumerable<CourseOccurrence> GetTeacherCourses(Teacher teacher, Terms term)
        {
            return teacher.CourseTeacher.Where(c => c.CourseOccurrence.Term == term && c.CourseOccurrence.Year == GetCurrentEducationalYear()).Select(c => c.CourseOccurrence);
        }

        public string GetCourseResponsibleNameFromIdFind(int courseResponsibleId)
        {
            Teacher courseResponsible = db.Teacher.Where(t => t.TeacherId == courseResponsibleId).FirstOrDefault();
            return courseResponsible == null ? "No course responsible" : courseResponsible.TeacherName.ToString();
        }
        
        private IEnumerable<Comment> GetComments(Teacher teacher, bool publicComment = true)
        {
            IEnumerable<Comment> comments = db.Comment.Where(x => x.BaseMessage.RecieverID == teacher.TeacherId && x.IsPublic == publicComment && x.BaseMessage.MessageDeletionDate == null);
            if (!publicComment)
            {
                int currentTeacherId = GetTeacherId();
                comments = comments.Where(x => x.BaseMessage.SenderID == currentTeacherId || x.BaseMessage.RecieverID == currentTeacherId);
            }
            return comments;
        }

        #endregion
    }
}