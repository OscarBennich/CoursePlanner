using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    
        public class CourseContext : DbContext
        {
            public CourseContext()
                : base("DefaultConnection")
            {
            }

            public DbSet<CourseModels> Courses { get; set; }
        }
    
}