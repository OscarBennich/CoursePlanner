using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var target = 1756;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetBaseAnnualHoursTestBetween30and40()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1982, 7, 15), contract);

            var result = te.GetBaseAnnualHours();
            var target = 1735;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetBaseAnnualHoursTestOver40()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            var result = te.GetBaseAnnualHours();
            var target = 1700;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllTeachingHoursFallZeroReductionsTest()
        {
            var contract = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 1, 1, "Professor");
            var te = new Teacher(1, "Herbert", new DateTime(1972, 7, 15), contract);

            var result = te.GetAllTeachingHoursFall();
            var target = 1700;

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
            var target = 1700*0.5F;

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAllTeachingHoursSpringTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetAllocatedHoursFallTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetRemaingHoursFallTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllocatedHoursSpringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetRemaingHoursSpringTest()
        {
            Assert.Fail();
        }
    }
}