using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoursePlanner;
using CoursePlanner.Controllers;

namespace CoursePlanner.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void GetCurrentEduYearTest()
        {
            var year = HomeController.GetCurrentEduYear();
            Assert.AreEqual("2017/2018", year);
        }
        
    }
}

