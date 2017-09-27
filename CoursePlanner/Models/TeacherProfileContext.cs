using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CoursePlanner.Models
{
    public class TeacherProfileContext : DbContext
    {
        public TeacherProfileContext() : base("TeacherProfileContext")
        {
        }

        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<TeacherContract> TeacherContract { get; set; }
        public DbSet<TeacherReduction> TeacherReduction { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}