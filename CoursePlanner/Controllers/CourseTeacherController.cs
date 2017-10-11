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

namespace CoursePlanner.Controllers
{
    public class CourseTeacherController : Controller
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        //
        // GET: /CourseTeacher/

        public ActionResult Index()
        {
            var courseteacher = db.CourseTeacher.Include(c => c.CourseOccurrence).Include(c => c.Teacher);
            return View(courseteacher.ToList());
        }

        //
        // GET: /CourseTeacher/Details/5/1

        public ActionResult Details(int cid = 0, int tid = 0)
        {
            CourseTeacher courseteacher = db.CourseTeacher.Where(c => c.CourseOccurrenceId == cid && c.TeacherId == tid).FirstOrDefault();
            if (courseteacher == null)
            {
                return HttpNotFound();
            }
            return View(courseteacher);
        }


        //
        // GET: /CourseTeacher/Create

        public ActionResult Create(int cid = 0, int tid = 0)
        {
            var courses = db.CourseOccurrence.Where(c => c.Status != Statuses.Completed).Select(c => new SelectListItem
            {
                Value = SqlFunctions.StringConvert((double)c.CourseOccurrenceID).Trim(),
                Text = c.Course.CourseName + " " + c.Year,
            }).OrderBy(c => c.Text).ToList();

            ViewBag.CourseOccurrenceId = new SelectList(courses, "Value", "Text", cid.ToString());
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "TeacherName", tid);
            
            //ViewBag.TeacherCourses = GetTeacherCourses(tid).ToList();

            string academicYear = GetAcademicYear();

            ViewBag.CoursesForTeacher = db.CourseTeacher.Where(x => x.TeacherId == tid && x.CourseOccurrence.Year == academicYear).OrderBy(x => x.CourseOccurrence.Course.CourseName).ToList();
            ViewBag.TeachersForCourse = db.CourseTeacher.Where(x => x.CourseOccurrenceId == cid && x.CourseOccurrence.Year == academicYear).OrderBy(x => x.Teacher.TeacherName).ToList();

            ViewBag.SelectedCourseName = db.CourseTeacher.Where(x => x.CourseOccurrenceId == cid).Select(x => x.CourseOccurrence.Course.CourseName + " " + x.CourseOccurrence.Year).FirstOrDefault();
            ViewBag.SelectedTeacherName = db.CourseTeacher.Where(x => x.TeacherId == tid).Select(x => x.Teacher.TeacherName).FirstOrDefault();

            return View();
        }

        //
        // POST: /CourseTeacher/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseTeacher courseteacher)
        {
            if (ModelState.IsValid)
            {
                db.CourseTeacher.Add(courseteacher);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Edit(courseteacher);
                }

                return RedirectToAction("Create", new { tid = courseteacher.TeacherId, cid = courseteacher.CourseOccurrenceId});
            }

            ViewBag.CourseOccurrenceId = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", courseteacher.CourseOccurrenceId);
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "TeacherName", courseteacher.TeacherId);
            return View(courseteacher);
        }

        //
        // GET: /CourseTeacher/Edit/5

        public ActionResult Edit(int cid = 0, int tid = 0)
        {
            CourseTeacher courseteacher = db.CourseTeacher.Where(c => c.CourseOccurrenceId == cid && c.TeacherId == tid).FirstOrDefault();
            if (courseteacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseOccurrenceId = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", courseteacher.CourseOccurrenceId);
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "TeacherName", courseteacher.TeacherId);
            return View(courseteacher);
        }

        //
        // POST: /CourseTeacher/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseTeacher courseteacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseteacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseOccurrenceId = new SelectList(db.CourseOccurrence, "CourseOccurrenceID", "Year", courseteacher.CourseOccurrenceId);
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "TeacherName", courseteacher.TeacherId);
            return View(courseteacher);
        }

        //
        // GET: /CourseTeacher/Delete/5

        public ActionResult Delete(int cid = 0, int tid = 0)
        {
            CourseTeacher courseteacher = db.CourseTeacher.Where(c => c.CourseOccurrenceId == cid && c.TeacherId == tid).FirstOrDefault();
            if (courseteacher == null)
            {
                return HttpNotFound();
            }
            return View(courseteacher);
        }

        //
        // POST: /CourseTeacher/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int cid = 0, int tid = 0)
        {
            CourseTeacher courseteacher = db.CourseTeacher.Where(c => c.CourseOccurrenceId == cid && c.TeacherId == tid).FirstOrDefault();
            db.CourseTeacher.Remove(courseteacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private IEnumerable<CourseTeacher> GetTeacherCourses(int teacherID)
        {
            string academicYear = GetAcademicYear();
            return db.CourseTeacher.Where(c => c.TeacherId == teacherID && c.CourseOccurrence.Year == academicYear);
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
    }
}