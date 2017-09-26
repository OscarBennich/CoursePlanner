﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoursePlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlanner.Models.Tests
{
    [TestClass]
    public class TeacherModelTests
    {
        [TestMethod]
        public void GetBaseAnnualHoursTestUnder30()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1992, 7, 15), contract);

            var result = te.GetBaseAnnualHours();
            const int target = 1756;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetBaseAnnualHoursTestBetween30and40()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1982, 7, 15), contract);

            var result = te.GetBaseAnnualHours();
            const int target = 1735;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetBaseAnnualHoursTestOver40()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            var result = te.GetBaseAnnualHours();
            const int target = 1700;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllTeachingHoursFallZeroReductionsTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            var result = te.GetAllTeachingHoursFall();
            const int target = 1700;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllTeachingHoursFallWithReductionsTest()
        {
            var reductionList = new List<TeacherReduction>()
            {
                new TeacherReduction(1, TeacherReduction.reductionTypes.Commitment, "", "Fall", 0.5F)
            };

            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor", reductionList);
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            var result = te.GetAllTeachingHoursFall();
            const float target = 1700*0.5F;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllTeachingHoursSpringZeroReductionsTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            var result = te.GetAllTeachingHoursSpring();
            const int target = 1700;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllTeachingHoursSpringWithReductionsTest()
        {
            var reductionList = new List<TeacherReduction>
            {
                new TeacherReduction(1, TeacherReduction.reductionTypes.Commitment, "", "Spring", 0.5F)
            };

            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor", reductionList);
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            var result = te.GetAllTeachingHoursSpring();
            const float target = 1700 * 0.5F;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllocatedHoursFallZeroHoursNoCourseTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            var result = te.GetAllocatedHoursFall();
            const int target = 0;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllocatedHoursFallZeroHoursWithCourseTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            // var courses = new List<Course>
            //{
            //      # Add One Fall Course that the teacher isn't in
            //}

            var result = te.GetAllocatedHoursFall();
            const int target = 0;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllocatedHoursFall50HoursOneCourseTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            // var courses = new List<Course>
            //{
            //      # Add One Fall Course
            //}

            var result = te.GetAllocatedHoursFall();
            const int target = 50;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllocatedHoursFall50HoursTwoCourseTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            // var courses = new List<Course>
            //{
            //      # Add Two Fall Courses
            //}

            var result = te.GetAllocatedHoursFall();

            const int target = 50;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllocatedHoursSpringZeroHoursNoCourseTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);
            
            var result = te.GetAllocatedHoursSpring();

            const int target = 0;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllocatedHoursSpringZeroHoursWithCourseTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            // var courses = new List<Course>
            //{
            //      # Add One Spring Course that the teacher isn't in
            //}

            var result = te.GetAllocatedHoursSpring();
            const int target = 0;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllocatedHoursSpring50HoursOneCourseTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            // var courses = new List<Course>
            //{
            //      # Add One Spring Course
            //}

            var result = te.GetAllocatedHoursSpring();

            const int target = 50;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllocatedHoursSpring50HoursTwoCourseTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            // var courses = new List<Course>
            //{
            //      # Add Two Spring Courses
            //}

            var result = te.GetAllocatedHoursSpring();

            const int target = 50;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetRemaingHoursFallTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

        }

        

        [TestMethod()]
        public void GetRemaingHoursSpringTest()
        {
            Assert.Fail();
        }
    }
}