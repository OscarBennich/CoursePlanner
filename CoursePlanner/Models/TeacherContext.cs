using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    public class TeacherContext : DbContext
    {
        public TeacherContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<TeacherModel> Teachers { get; set; }
        //public DbSet<TeacherContract> TeacherContracts { get; set; }
        //public DbSet<TeacherReduction> TeacherReductions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TeacherContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}