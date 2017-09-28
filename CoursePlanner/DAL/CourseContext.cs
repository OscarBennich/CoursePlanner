using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using CoursePlanner.Models;

namespace CoursePlanner.DAL
{

    public class CourseContext : DbContext
    {
        public CourseContext()
            : base("CourseContext")
        {
        }

        public DbSet<CourseModel> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}