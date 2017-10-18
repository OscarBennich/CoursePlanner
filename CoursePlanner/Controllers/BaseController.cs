using CoursePlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace CoursePlanner.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        private CoursePlannerEntities db = new CoursePlannerEntities();
        //    //protected override ViewResult View(IView view, object model)
        //    //{
        //    //   // this.ViewBag.TeacherId = GetTeacherId();
        //    //    return base.View(view, model);
        //    //}
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //ViewBag.CurrentTeacherId = new Func<int, int>(GetTeacherId);
            ViewBag.CurrentTeacherId = GetTeacherId();
            base.OnActionExecuting(filterContext);
        }

        public int GetTeacherId()
        {


            int teacherId = (from m in db.Teacher
                             where m.TeacherUserId == WebSecurity.CurrentUserId
                             select m.TeacherId).FirstOrDefault();
            return teacherId;


        }

    }
}