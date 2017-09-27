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
            var contract1 = new TeacherContract
            {
                TotalPercentageFall = 1,
                TotalPercentageSpring = 1,
                Position = Position.Lektor
            };

            var contract2 = new TeacherContract
            {
                TotalPercentageFall = 30,
                TotalPercentageSpring = 30,
                Position = Position.Amanuens
            };


            var teacherContracts = new List<TeacherContract>
            {
                contract1,
                contract2
            };
            teacherContracts.ForEach(s => context.TeacherContracts.Add(s));
            context.SaveChanges();

            var teacherReductions = new List<TeacherReduction>
            {
                new TeacherReduction {Description = "Study Director", Term = Term.Fall, Percentage = 0.5F, Type = ReductionType.Commitment, TeacherContract = contract1 },
                new TeacherReduction {Description = "Meetings and Admin", Term = Term.Fall, Percentage = 0.1F, Type = ReductionType.Commitment, TeacherContract = contract1 },
                new TeacherReduction {Description = "Study Director", Term = Term.Spring, Percentage = 0.5F, Type = ReductionType.Commitment, TeacherContract = contract1},
                new TeacherReduction {Description = "Meetings and Admin", Term = Term.Spring, Percentage = 0.1F, Type = ReductionType.Commitment, TeacherContract = contract1 }
            };
            teacherReductions.ForEach(s => context.TeacherReductions.Add(s));
            context.SaveChanges();


            var teacher = new List<Teacher>
            {
                new Teacher { Name = "Tomas Eklund", Dob = "19760101", TeacherContract = contract1},
                new Teacher { Name = "Sofie Roos", Dob = "19920313", TeacherContract = contract2}
            };

            teacher.ForEach(s => context.Teachers.Add(s));
            context.SaveChanges();
        }
    }
}