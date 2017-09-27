using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    public class TeacherProfileDataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TeacherProfileContext>
    {
        protected override void Seed(TeacherProfileContext context)
        {
            var teacherContracts = new List<TeacherContract>
            {
                new TeacherContract(100, 100, Position.Lektor),
                new TeacherContract(30, 30, Position.Amanuens)
            };
            teacherContracts.ForEach(s => context.TeacherContracts.Add(s));
            context.SaveChanges();

            var teacherReductions = new List<TeacherReduction>
            {
                new TeacherReduction(ReductionType.Commitment, "Study Director", Term.Fall, 0.5F, 1),
                new TeacherReduction(ReductionType.Commitment, "Meetings and Admin", Term.Fall, 0.1F, 1),
                new TeacherReduction(ReductionType.Commitment, "Study Director", Term.Fall, 0.5F, 1),
                new TeacherReduction(ReductionType.Commitment, "Meetings and Admin", Term.Fall, 0.1F, 1)
            };
            teacherReductions.ForEach(s => context.TeacherReductions.Add(s));
            context.SaveChanges();


            var teacher = new List<Teacher>
            {
                new Teacher("Tomas Eklund", new DateTime(1976, 1,1), 1),
                new Teacher("Sofie Roos", new DateTime(1992, 3, 13), 2)
            };

            teacher.ForEach(s => context.Teachers.Add(s));
            context.SaveChanges();


        }
    }
}