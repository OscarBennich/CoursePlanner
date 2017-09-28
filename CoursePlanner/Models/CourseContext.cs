using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        public DbSet<CourseModel> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CourseContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }

}