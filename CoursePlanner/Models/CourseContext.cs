using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoursePlanDraft.Models
{

    public class CourseContext : DbContext
    {
        public CourseContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<CourseModel> Courses { get; set; }
    }

}