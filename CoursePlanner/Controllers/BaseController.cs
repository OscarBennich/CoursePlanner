using CoursePlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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
         ViewBag.TeacherId = GetTeacherId();
         base.OnActionExecuting(filterContext);
     }
     private int GetTeacherId()
     {

         
         int teacherId = (from m in db.Teacher
                          where m.TeacherName.ToUpper().StartsWith(User.Identity.Name.ToUpper())
                          select m.TeacherId).FirstOrDefault();
         return teacherId;


     }
    }
}
