using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public DbSet<Teacher> Teacher { get; set; }
    }


    [Table("Teacher")]
    public class Teacher
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; private set; }
        
        public string Name { get; set; }
        public DateTime Dob { get; set; }

        [ForeignKey("TeacherContract")]
        public int ContractId { get; set; }
        public TeacherContract ContractDetails { get; set; }
        

        public Teacher(int id, string name, DateTime dob, TeacherContract contract)
        {
            TeacherId = id;
            Name = name;
            Dob = dob;
            ContractDetails = contract;

        }

        public int GetBaseAnnualHours()
        {
            if (getAge() > 40) return 1700;
            if (getAge() > 30 && getAge() < 40) return 1735;
            if (getAge() > 0 && getAge() < 30) return 1756;
            return 0;
        }

        // Get hours per term
        public int GetAllTeachingHoursFall()
        {
            int baseHours = GetBaseAnnualHours();

            float totalPercentage = ContractDetails.TotalPercentageFall;
            float reductivePercentage = ContractDetails.FallTotalPercentageForReduction();
            float teachingPercentage = totalPercentage * (1 - reductivePercentage);

            baseHours = (int)Math.Round(baseHours * teachingPercentage, MidpointRounding.ToEven);

            return baseHours; 
        }

        // Get hours per term
        public int GetAllTeachingHoursSpring()
        {
            int baseHours = GetBaseAnnualHours();

            float totalPercentage = ContractDetails.TotalPercentageSpring;
            float reductivePercentage = ContractDetails.SpringTotalPercentageForReduction();
            float teachingPercentage = totalPercentage * (1 - reductivePercentage);

            baseHours = (int)Math.Round(baseHours * teachingPercentage, MidpointRounding.ToEven);

            return baseHours;
        }

        // Should in future get all hours allocated for teacher on the courses in the courses list!
        public int GetAllocatedHoursFall()
        {
            // Modell som innehåller kurs, lärare och antal timmar
            // ANVÄND LINK: Where (this && "fall"), Select (Hours), sum (All)
            // Hämta alla där läraren är "this"
            // Summera alla timfält där term = fall

            return 0;
        }

        // Get hours for SPRING
        // Should in future get all hours allocated for teacher on the courses in the courses list!
        public int GetAllocatedHoursSpring()
        {
            // Modell som innehåller kurs, lärare och antal timmar
            // ANVÄND LINK: Where (this && "fall"), Select (Hours), sum (All)
            // Hämta alla där läraren är "this"
            // Summera alla timfält där term = fall

            return 0;
        }

        public int GetRemainingHoursFall()
        {
            return GetAllTeachingHoursFall() - GetAllocatedHoursFall();
        }

        public int GetRemainingHoursSpring()
        {
            return GetAllTeachingHoursSpring() - GetAllocatedHoursSpring();
        }

        private int getAge()
        {
            return DateTime.Today.Year - Dob.Year;
        }
    }
}