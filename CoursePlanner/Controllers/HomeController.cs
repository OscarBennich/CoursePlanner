using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoursePlanner.Models;
using WebGrease.Css.Extensions;

namespace CoursePlanner.Controllers
{
    [Authorize] 
    public class HomeController : Controller
    {
        private CoursePlannerEntities db = new CoursePlannerEntities();

        public ActionResult Index()
        {
            return View(db.Course.ToList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            var filters = formCollection.AllKeys.Where(key => formCollection[key].Contains("true")).ToList();

            return View(db.Course.ToList());
        }

        public static string CurrentSchoolYear()
        {
            int fall;

            if (DateTime.Today.Month > 6 && DateTime.Today.Month <= 12)
                fall = DateTime.Today.Year;
            else
            {
                fall = DateTime.Today.Year-1;
            }

            int spring;
            if (DateTime.Today.Month >= 1 && DateTime.Today.Month <= 6)
                spring = DateTime.Today.Year;
            else
            {
                spring = DateTime.Today.Year + 1;
            }

            return fall + "/" +  spring;

        }

        private void initializeData()
        {
            var courses = new List<Course>
            {
                new Course
                {
                    CourseClassificiation = CourseClassifications.Master,
                    CourseCode = "2IS200",
                    CourseCredit = CourseCredits.C15,
                    CourseName = "Agile Methods",
                    CourseLevel = CourseLevels.Advanced,
                    CourseType = CourseTypes.Campus,
                    CourseOccurrence = new List<CourseOccurrence>()
                    {
                        new CourseOccurrence()
                        {
                            Budget = 400,
                            Year = "2017/2018",
                            Term = Terms.Fall,
                            Period = Periods.P1P2
                        },
                        new CourseOccurrence()
                        {
                            Budget = 400,
                            Year = "2016/2017",
                            Term = Terms.Fall,
                            Period = Periods.P1P2
                        }
                    }
                },
                new Course
                {
                    CourseClassificiation = CourseClassifications.Master,
                    CourseCode = "2IS207",
                    CourseCredit = CourseCredits.C7_5,
                    CourseName = "Artificial Intelligence",
                    CourseLevel = CourseLevels.Advanced,
                    CourseType = CourseTypes.Campus,
                    CourseOccurrence = new List<CourseOccurrence>()
                    {
                        new CourseOccurrence()
                        {
                            Budget = 400,
                            Year = "2017/2018",
                            Term = Terms.Fall,
                            Period = Periods.P3
                        },
                        new CourseOccurrence()
                        {
                            Budget = 400,
                            Year = "2016/2017",
                            Term = Terms.Fall,
                            Period = Periods.P3
                        }
                    }
                },
                new Course
                {
                    CourseClassificiation = CourseClassifications.Bachelor,
                    CourseCode = "2IS210",
                    CourseCredit = CourseCredits.C7_5,
                    CourseName = "Object oriented programming 2",
                    CourseLevel = CourseLevels.Intermediate,
                    CourseType = CourseTypes.Campus,
                    CourseOccurrence = new List<CourseOccurrence>()
                    {
                        new CourseOccurrence()
                        {
                            Budget = 700,
                            Year = "2017/2018",
                            Term = Terms.Fall,
                            Period = Periods.P2
                        },
                        new CourseOccurrence()
                        {
                            Budget = 700,
                            Year = "2016/2017",
                            Term = Terms.Fall,
                            Period = Periods.P2
                        }
                    }

                },
                new Course
                {
                    CourseClassificiation = CourseClassifications.Bachelor,
                    CourseCode = "2IS212",
                    CourseCredit = CourseCredits.C7_5,
                    CourseName = "Databases",
                    CourseLevel = CourseLevels.Basic,
                    CourseType = CourseTypes.Campus,
                    CourseOccurrence = new List<CourseOccurrence>()
                    {
                        new CourseOccurrence()
                        {
                            Budget = 500,
                            Year = "2017/2018",
                            Term = Terms.Fall,
                            Period = Periods.P4
                        },
                        new CourseOccurrence()
                        {
                            Budget = 500,
                            Year = "2016/2017",
                            Term = Terms.Fall,
                            Period = Periods.P4
                        }
                    }

                },
                new Course
                {
                    CourseClassificiation = CourseClassifications.Bachelor,
                    CourseCode = "2IS215",
                    CourseCredit = CourseCredits.C7_5,
                    CourseName = "Algorithms and datastrutures",
                    CourseLevel = CourseLevels.Basic,
                    CourseType = CourseTypes.Campus,
                    CourseOccurrence = new List<CourseOccurrence>()
                    {
                        new CourseOccurrence()
                        {
                            Budget = 500,
                            Year = "2017/2018",
                            Term = Terms.Spring,
                            Period = Periods.P3
                        },
                        new CourseOccurrence()
                        {
                            Budget = 500,
                            Year = "2016/2017",
                            Term = Terms.Spring,
                            Period = Periods.P3
                        }
                    }

                },
                new Course
                {
                    CourseClassificiation = CourseClassifications.Master,
                    CourseCode = "2IS220",
                    CourseCredit = CourseCredits.C30,
                    CourseName = "Master thesis",
                    CourseLevel = CourseLevels.Advanced,
                    CourseType = CourseTypes.Campus,
                    CourseOccurrence = new List<CourseOccurrence>()
                    {
                        new CourseOccurrence()
                        {
                            Budget = 1000,
                            Year = "2017/2018",
                            Term = Terms.Spring,
                            Period = Periods.P1P2P3P4
                        },
                        new CourseOccurrence()
                        {
                            Budget = 1000,
                            Year = "2016/2017",
                            Term = Terms.Spring,
                            Period = Periods.P1P2P3P4
                        }
                    }

                }

            };

            courses.ForEach(course => db.Course.Add(course));
            db.SaveChanges();
        }




    }
}
