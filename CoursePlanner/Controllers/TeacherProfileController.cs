using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursePlanner.Controllers
{
    [Authorize]
    public class TeacherProfileController : Controller
    {
        //
        // GET: /TeacherProfile/

        public ActionResult TeacherProfile()
        {
            return View();
        }

        // Make sure percentages are adjusted to decimals!

    }
}
