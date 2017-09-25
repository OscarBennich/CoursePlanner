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
    public class TeacherContractTests
    {
        [TestMethod]
        public void AddReductionTest()
        {
            var tc = new TeacherContract(DateTime.MinValue, DateTime.MaxValue, 100, 100, "Professor");
            var target = 1; 
            tc.AddReduction(new TeacherReduction(1, TeacherReduction.reductionTypes.Commitment, "Study director", "Fall", 20));

            Assert.AreEqual(target, tc.Reductions.Count);
        }

        [TestMethod]
        public void RemoveReductionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TotalProcentageForReductionTypeTestFall()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TotalProcentageForReductionTypeTestSpring()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FallReductionDescriptionWithProcentageGroupedByTypeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SpringReductionDescriptionWithProcentageGroupedByTypeTest()
        {
            Assert.Fail();
        }
    }
}