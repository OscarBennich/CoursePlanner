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
    public class TeacherController : BaseController
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        //
        // GET: /Teacher/

        public ActionResult Index()
        {
            return View(db.Teacher.ToList());
        }

        //
        // GET: /Teacher/Details/5

        public ActionResult Details(int id = 0)
        {
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            List<CourseOccurrence> courseOccurencesFall = GetTeacherCourses(teacher.TeacherId, Terms.Fall).ToList();
            List<CourseOccurrence> courseOccurencesSpring = GetTeacherCourses(teacher.TeacherId, Terms.Spring).ToList();

            List<CourseOccurrence> sum = courseOccurencesFall.Concat(courseOccurencesSpring).ToList();

            int p1 = ReturnPeriodHours(sum, teacher, Periods.P1, Terms.Fall);
            int p2 = ReturnPeriodHours(sum, teacher, Periods.P2, Terms.Fall);
            int p3 = ReturnPeriodHours(sum, teacher, Periods.P3, Terms.Fall);
            int p4 = ReturnPeriodHours(sum, teacher, Periods.P4, Terms.Fall);
            int p5 = ReturnPeriodHours(sum, teacher, Periods.P1, Terms.Spring);
            int p6 = ReturnPeriodHours(sum, teacher, Periods.P2, Terms.Spring);
            int p7 = ReturnPeriodHours(sum, teacher, Periods.P3, Terms.Spring);
            int p8 = ReturnPeriodHours(sum, teacher, Periods.P4, Terms.Spring);

            ViewBag.Period1 = p1;
            ViewBag.Period2 = p2;
            ViewBag.Period3 = p3;
            ViewBag.Period4 = p4;
            ViewBag.Period5 = p5;
            ViewBag.Period6 = p6;
            ViewBag.Period7 = p7;
            ViewBag.Period8 = p8;

            ViewBag.TeachingHoursAllocatedFall = p1 + p2 + p3 + p4;
            ViewBag.TeachingHoursAllocatedSpring = p5 + p6 + p7 + p8;


            int baseAnnualWorkingHours = GetBaseAnnualHours(teacher.TeacherDateOfBirth);
            ViewBag.BaseAnnualWorkingHours = baseAnnualWorkingHours;

            ViewBag.TeachingHoursAvailableFall = CalculateTeachingHoursAvailable(teacher, baseAnnualWorkingHours, Terms.Fall);
            ViewBag.TeachingHoursAvailableSpring = CalculateTeachingHoursAvailable(teacher, baseAnnualWorkingHours, Terms.Spring);

            //ViewBag.TeachingHoursAllocatedFall = CalculateTeachingHoursAllocated(teacher, Terms.Fall);
            //ViewBag.TeachingHoursAllocatedSpring = CalculateTeachingHoursAllocated(teacher, Terms.Spring);

            ViewBag.TotalHoursAllocatedResearchFall = Convert.ToInt32((baseAnnualWorkingHours/2)*teacher.TotalPercentageFall * teacher.TeacherReduction.Where(x => x.ReductionType == ReductionTypes.Research && x.TeacherId == teacher.TeacherId && x.Term == Terms.Fall).Select(y => y.Percentage).FirstOrDefault());
            ViewBag.TotalHoursAllocatedAdministrationFall = Convert.ToInt32((baseAnnualWorkingHours / 2) * teacher.TotalPercentageFall * teacher.TeacherReduction.Where(x => x.ReductionType == ReductionTypes.Administration && x.TeacherId == teacher.TeacherId && x.Term == Terms.Fall).Select(y => y.Percentage).FirstOrDefault());
            ViewBag.TotalHoursAllocatedAssignmentsFall = Convert.ToInt32((baseAnnualWorkingHours / 2) * teacher.TotalPercentageFall * teacher.TeacherReduction.Where(x => x.ReductionType == ReductionTypes.Assignments && x.TeacherId == teacher.TeacherId && x.Term == Terms.Fall).Select(y => y.Percentage).FirstOrDefault());
            ViewBag.TotalHoursAllocatedOtherFall = Convert.ToInt32((baseAnnualWorkingHours / 2) * teacher.TotalPercentageFall * teacher.TeacherReduction.Where(x => x.ReductionType == ReductionTypes.Other && x.TeacherId == teacher.TeacherId && x.Term == Terms.Fall).Select(y => y.Percentage).FirstOrDefault());
            
            int TotalTeachingHoursFall = baseAnnualWorkingHours/2 - ViewBag.TotalHoursAllocatedResearchFall;
            TotalTeachingHoursFall = TotalTeachingHoursFall - ViewBag.TotalHoursAllocatedAdministrationFall;
            TotalTeachingHoursFall = TotalTeachingHoursFall - ViewBag.TotalHoursAllocatedAssignmentsFall;
            TotalTeachingHoursFall = TotalTeachingHoursFall - ViewBag.TotalHoursAllocatedOtherFall;

            ViewBag.TotalTeachingHoursFall = TotalTeachingHoursFall;


            ViewBag.TotalHoursAllocatedResearchSpring = Convert.ToInt32((baseAnnualWorkingHours / 2) * teacher.TotalPercentageSpring * teacher.TeacherReduction.Where(x => x.ReductionType == ReductionTypes.Research && x.TeacherId == teacher.TeacherId && x.Term == Terms.Spring).Select(y => y.Percentage).FirstOrDefault());
            ViewBag.TotalHoursAllocatedAdministrationSpring = Convert.ToInt32((baseAnnualWorkingHours / 2) * teacher.TotalPercentageSpring * teacher.TeacherReduction.Where(x => x.ReductionType == ReductionTypes.Administration && x.TeacherId == teacher.TeacherId && x.Term == Terms.Spring).Select(y => y.Percentage).FirstOrDefault());
            ViewBag.TotalHoursAllocatedAssignmentsSpring = Convert.ToInt32((baseAnnualWorkingHours / 2) * teacher.TotalPercentageSpring * teacher.TeacherReduction.Where(x => x.ReductionType == ReductionTypes.Assignments && x.TeacherId == teacher.TeacherId && x.Term == Terms.Spring).Select(y => y.Percentage).FirstOrDefault());
            ViewBag.TotalHoursAllocatedOtherSpring = Convert.ToInt32((baseAnnualWorkingHours / 2) * teacher.TotalPercentageSpring * teacher.TeacherReduction.Where(x => x.ReductionType == ReductionTypes.Other && x.TeacherId == teacher.TeacherId && x.Term == Terms.Spring).Select(y => y.Percentage).FirstOrDefault());

            int TotalTeachingHoursSpring = baseAnnualWorkingHours/2 - ViewBag.TotalHoursAllocatedResearchSpring;
            TotalTeachingHoursSpring = TotalTeachingHoursSpring - ViewBag.TotalHoursAllocatedAdministrationSpring;
            TotalTeachingHoursSpring = TotalTeachingHoursSpring - ViewBag.TotalHoursAllocatedAssignmentsSpring;
            TotalTeachingHoursSpring = TotalTeachingHoursSpring - ViewBag.TotalHoursAllocatedOtherSpring;

            ViewBag.TotalTeachingHoursSpring = TotalTeachingHoursSpring;

            ViewBag.ExpectedPeriodHours = (TotalTeachingHoursFall + TotalTeachingHoursSpring) / 8;

            ViewBag.GetCourseResponsibleName =
              new Func<int, string>(GetCourseResponsibleNameFind);

            ViewBag.IsCourseResponsibleName =
              new Func<int, int, string>(IsCourseResponsibleNameFind);
         

            ViewBag.CourseOccurencesFall = courseOccurencesFall;
            ViewBag.CourseOccurencesSpring = courseOccurencesSpring;
         
            return View(teacher);
        }

        //
        // GET: /Teacher/Create

        public ActionResult Create()
        {
            return View();
        }

        //
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

        //
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

        //
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

        //
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private int GetBaseAnnualHours(DateTime teacherDateOfBirth)
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

        private int GetAge(DateTime teacherDateOfBirth)
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

        private double CalculateTeachingHoursAvailable(Teacher teacher, int baseAnnualWorkingHours, Terms term) //should split into terms, testing for now
        {   
            int teachingHoursAvailable = 0;
            double totalReductionPercentage = 0;

            if(term.Equals(Terms.Fall)) 
            {   
                // First split the base amount in 2 (2 terms in one year)
                // Then multiply by the total percentage for that term (for example *0,5)
                // This gives us the base available hours for this teacher for this term
                teachingHoursAvailable = Convert.ToInt32(baseAnnualWorkingHours/2 * teacher.TotalPercentageFall);

                try // Error handling for the case that the teacher has no reductions
                {
                    // Once we have the base amount we apply the sum of all other reductions (Research, Assignments, Administration and Other reductions) for that term
                    // For example a professor (per default) would have 50% research and 10% administration so the sum is 0,5 + 0,1 = 0,6
                    totalReductionPercentage = (double)db.TeacherReduction.Where(x => x.TeacherId == teacher.TeacherId && x.Term == Terms.Fall).Select(y => y.Percentage).Sum();
                }
                catch { }
            }
            else 
            {   
                teachingHoursAvailable = Convert.ToInt32(baseAnnualWorkingHours/2 * teacher.TotalPercentageSpring);

                try 
                {
                    totalReductionPercentage = (double)db.TeacherReduction.Where(x => x.TeacherId == teacher.TeacherId && x.Term == Terms.Spring).Select(y => y.Percentage).Sum();
                }
                catch { }
            }

            // Finally we apply the total reduction to the base value for the term
            // For example a base of 200 hours for this term for a professor would result in 
            // 200 * 0,6 = 120 hours
            return teachingHoursAvailable = Convert.ToInt32(teachingHoursAvailable * (1- totalReductionPercentage));
        }

        private int CalculateTeachingHoursAllocated(Teacher teacher, Terms term)
        {
            int teachingHoursAllocated = 0;

            if (term.Equals(Terms.Fall)) 
            {
                try
                {   
                    // Calculate the sum of all hours allocated for this teacher in all course occurances which match
                    // with the teacherID and where the term matches (Fall in this case)
                    teachingHoursAllocated += Convert.ToInt32(db.CourseTeacher.Where(x => x.TeacherId == teacher.TeacherId && x.CourseOccurrence.Term == Terms.Fall).Select(y => y.Hours).Sum());
                }
                catch { }
            }
            else 
            {
                try
                {
                    teachingHoursAllocated += Convert.ToInt32(db.CourseTeacher.Where(x => x.TeacherId == teacher.TeacherId && x.CourseOccurrence.Term == Terms.Spring).Select(y => y.Hours).Sum());
                }
                catch { }
            }

            return teachingHoursAllocated;
        }

        private IEnumerable<CourseOccurrence> GetTeacherCourses(int teacherID, Terms term)
        {
            string academicYear = DateTime.Today.Year + "/" + (DateTime.Today.Year + 1);

            if (DateTime.Today.Month <= 6)
            {
                academicYear = (DateTime.Today.Year-1) + "/" + DateTime.Today.Year;
            }

            return db.CourseTeacher.Where(c => c.TeacherId == teacherID && c.CourseOccurrence.Term == term && c.CourseOccurrence.Year == academicYear).Select(c => c.CourseOccurrence);
        }

        private int ReturnPeriodHours(IEnumerable<CourseOccurrence> GetTeacherCourses, Teacher teacher, Periods period, Terms term)
        {
            int output = 0;

            foreach (CourseOccurrence currentCourse in GetTeacherCourses)
            {
                var CourseHourData = db.CourseTeacher.Where(c => c.TeacherId == teacher.TeacherId && c.CourseOccurrenceId == currentCourse.CourseOccurrenceID && currentCourse.Term == term).FirstOrDefault();

                if (CourseHourData != null)
                {
                    if (currentCourse.Period.ToString().Substring(0, 2) == Periods.P1.ToString() && currentCourse.Period.ToString().Contains(period.ToString()))
                    {
                        var numPeriods = currentCourse.Period.ToString().Length;
                        
                        if (numPeriods == 2)
                        {
                            output += Convert.ToInt32(CourseHourData.Hours);
                        }
                        if (numPeriods == 4)
                        {
                            output += (Convert.ToInt32(CourseHourData.Hours)) / 2;
                        }
                        if (numPeriods == 6)
                        {
                            output += (Convert.ToInt32(CourseHourData.Hours)) / 3;
                        }
                        if (numPeriods == 8)
                        {
                            output += (Convert.ToInt32(CourseHourData.Hours)) / 4;
                        }
                    }
                    else if (currentCourse.Period.ToString().Substring(0, 2) == Periods.P2.ToString() && currentCourse.Period.ToString().Contains(period.ToString()))
                    {
                        var numPeriods = currentCourse.Period.ToString().Length;
                        
                        if (numPeriods == 2)
                        {
                            output += Convert.ToInt32(CourseHourData.Hours);
                        }
                        if (numPeriods == 4)
                        {
                            output += (Convert.ToInt32(CourseHourData.Hours)) / 2;
                        }
                        if (numPeriods == 6)
                        {
                            output += (Convert.ToInt32(CourseHourData.Hours)) / 3;
                        }
                    }

                    else if (currentCourse.Period.ToString().Substring(0, 2) == Periods.P3.ToString() && currentCourse.Period.ToString().Contains(period.ToString()))
                    {
                        var numPeriods = currentCourse.Period.ToString().Length;
                        
                        if (numPeriods == 2)
                        {
                            output += Convert.ToInt32(CourseHourData.Hours);
                        }
                        if (numPeriods == 4)
                        {
                            output += (Convert.ToInt32(CourseHourData.Hours)) / 2;
                        }
                    }

                    else if (currentCourse.Period.ToString().Substring(0, 2) == Periods.P4.ToString() && currentCourse.Period.ToString().Contains(period.ToString()))
                    {
                        var numPeriods = currentCourse.Period.ToString().Length;
                        
                        if (numPeriods == 2)
                        {
                            output += Convert.ToInt32(CourseHourData.Hours);
                        }
                    }
                }
            }         
            return output;
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
    }
}