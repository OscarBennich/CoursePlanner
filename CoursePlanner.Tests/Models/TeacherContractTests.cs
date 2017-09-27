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
            var tc = new TeacherContract(1, 1, 1, Position.Professor);
            var target = 1;
            tc.AddReduction(new TeacherReduction(ReductionType.Commitment, "Study director", Term.Fall, 1, 1));

            Assert.AreEqual(target, tc.Reductions.Count);
        }
       

        [TestMethod()]
        public void RemoveReductionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FallTotalPercentageForReductionZeroReductionTest()
        {
            var tc = new TeacherContract(1, 1, 1, Position.Professor);
            var target = 0;
            
            Assert.AreEqual(target, tc.FallTotalPercentageForReduction());
        }

        [TestMethod()]
        public void FallTotalPercentageForReductionTest()
        {
            var tc = new TeacherContract(1, 1, 1, Position.Professor);

            var target = 0;

            Assert.AreEqual(target, tc.FallTotalPercentageForReduction());
        }

        [TestMethod()]
        public void SpringTotalPercentageForReductionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FallTotalPercentageForReductionTypeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SpringTotalPercentageForReductionTypeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FallReductionDescriptionWithPercentageGroupedByTypeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SpringReductionDescriptionWithPercentageGroupedByTypeTest()
        {
            Assert.Fail();
        }
    }
}