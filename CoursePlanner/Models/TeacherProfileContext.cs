using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CoursePlanner.Models
{
    public class TeacherProfileContext : DbContext
    {
        public TeacherProfileContext() : base("DefaultConnection")
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherContract> TeacherContracts { get; set; }
        public DbSet<TeacherReduction> TeacherReductions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}