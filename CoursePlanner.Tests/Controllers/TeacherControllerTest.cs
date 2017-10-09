using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoursePlanner;
using CoursePlanner.Controllers;
using CoursePlanner.Models;

namespace CoursePlanner.Tests.Controllers
{
    [TestClass]
    public class TeacherControllerTest
    {
        [TestMethod]
        public void GetAgeBirthdayAfterNowTest() 
        {
            //arange
            var dateOfBirth = new DateTime(2000, 12, 24);
            var target = 16;

            //act
            var result = TeacherController.GetAge(dateOfBirth);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAgeBirthdayBeforeNowTest()
        {
           
            //arrange
            var dateOfBirth = new DateTime(2000, 06, 24);
            var target = 17;

            //act
            var result = TeacherController.GetAge(dateOfBirth);
            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAgeBaseAnnualHoursAbove40Test()
        {
            //arrange
            var dateOfBirth = new DateTime(1970,05,12);
            var target = 1700;

            //act
            var result = TeacherController.GetBaseAnnualHours(dateOfBirth);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAgeBaseAnnualHoursBetween30and40Test()
        {
            //arrange
            var dateOfBirth = new DateTime(1980, 05, 12);
            var target = 1735;

            //act
            var result = TeacherController.GetBaseAnnualHours(dateOfBirth);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAgeBaseAnnualHoursBelow30Test()
        {
            //arrange
            var dateOfBirth = new DateTime(1992, 05, 12);
            var target = 1756;

            //act
            var result = TeacherController.GetBaseAnnualHours(dateOfBirth);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAgeBaseAnnualHoursAgeBolow0Test()
        {
            //arrange
            var dateOfBirth = new DateTime(2017, 12, 12);
            var target = 0;

            //act
            var result = TeacherController.GetBaseAnnualHours(dateOfBirth);

            //assert
            Assert.AreEqual(target, result);
        }

         [TestMethod]
        public void CalculateTeachingHoursAvailableFallTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher= new Teacher() {
                TeacherId = 1,
                TeacherName= "Sofi",
                TotalPercentageSpring = 1,
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950,01,01)
            };
            var baseHours = TeacherController.GetBaseAnnualHours(new DateTime(1950,01,01));
             var target = baseHours / 2;


            //act
            var result = tc.CalculateTeachingHoursAvailable(teacher, Terms.Fall);

            //assert
            Assert.AreEqual(target, result);
        }

         [TestMethod]
         public void CalculateTeachingHoursAvailableSpringTest()
         {
             //arrange
             var tc = new TeacherController();
             var teacher = new Teacher()
             {
                 TeacherId = 001,
                 TeacherName = "Sofi",
                 TotalPercentageSpring = 1,
                 TotalPercentageFall = 1,
                 TeacherDateOfBirth = new DateTime(1950, 01, 01)
             };
             var baseHours = TeacherController.GetBaseAnnualHours(new DateTime(1950, 01, 01));
             var target = baseHours / 2;


             //act
             var result = tc.CalculateTeachingHoursAvailable(teacher, Terms.Spring);

             //assert
             Assert.AreEqual(target, result);
         }

         [TestMethod]
         public void CalculateTeachingHoursAvailableWithOneReductionFallTest()
         {
             //arrange
             var tc = new TeacherController();
             var teacher = new Teacher()
             {
                 TeacherId = 1,
                 TeacherName = "Sofi",
                 TotalPercentageSpring = 1,
                 TotalPercentageFall = 1,
                 TeacherDateOfBirth = new DateTime(1950, 01, 01),
                 TeacherReduction = new List<TeacherReduction>()
                 {
                     new TeacherReduction {
                     TeacherId = 1,
                     Term = Terms.Fall,
                     Percentage = 0.5
                     }
                 }
             };
             var baseHours = TeacherController.GetBaseAnnualHours(new DateTime(1950, 01, 01));
             var target = (baseHours / 2) * (1 - 0.5);


             //act
             var result = tc.CalculateTeachingHoursAvailable(teacher, Terms.Fall);

             //assert
             Assert.AreEqual(target, result);
         }

         [TestMethod]
         public void CalculateTeachingHoursAvailableWithTwoReductionsFallTest()
         {
             //arrange
             var tc = new TeacherController();
             var teacher = new Teacher()
             {
                 TeacherId = 1,
                 TeacherName = "Sofi",
                 TotalPercentageSpring = 1,
                 TotalPercentageFall = 1,
                 TeacherDateOfBirth = new DateTime(1950, 01, 01),
                 TeacherReduction = new List<TeacherReduction>()
                 {
                     new TeacherReduction {
                     TeacherId = 1,
                     Term = Terms.Fall,
                     Percentage = 0.5
                     },
                     new TeacherReduction {
                     TeacherId = 1,
                     Term = Terms.Fall,
                     Percentage = 0.2
                     }
                 }
             };
             var baseHours = TeacherController.GetBaseAnnualHours(new DateTime(1950, 01, 01));
             var target = Convert.ToInt32(Math.Round((baseHours / 2) * (1 - 0.7), MidpointRounding.AwayFromZero));


             //act
             var result = tc.CalculateTeachingHoursAvailable(teacher, Terms.Fall);

             //assert
             Assert.AreEqual(target, result);
         }
    }
}
