using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CoursePlanner.Models;

namespace CoursePlanner.DAL
{
    public class CoursePlannerContext : DbContext
    {
        public CoursePlannerContext() : base("CoursePlannerContext")
        {
        }

        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherContract> TeacherContracts { get; set; }
        public DbSet<TeacherReduction> TeacherReductions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}