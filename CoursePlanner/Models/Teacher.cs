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
    public class Teacher : User
    {
        
        public string Dob { get; set; }

        public virtual TeacherContract TeacherContract { get; set; }
        public virtual List<Course> Courses { get; set; }

        public Teacher() { }

        public Teacher(string name, string dob, TeacherContract contract)
        {
            Name = name;
            Dob = dob;
            TeacherContract = contract;
            Courses = new List<Course>();
        }

        public Teacher(int id, string name, string dob, TeacherContract contract)
        {
            Id = id;
            Name = name;
            Dob = dob;
            TeacherContract = contract;
            Courses = new List<Course>();

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

            float totalPercentage = TeacherContract.TotalPercentageFall;
            float reductivePercentage = TeacherContract.FallTotalPercentageForReduction();
            float teachingPercentage = totalPercentage * (1 - reductivePercentage);

            baseHours = (int)Math.Round(baseHours * teachingPercentage, MidpointRounding.ToEven);

            return baseHours; 
        }

        // Get hours per term
        public int GetAllTeachingHoursSpring()
        {
            int baseHours = GetBaseAnnualHours();

            float totalPercentage = TeacherContract.TotalPercentageSpring;
            float reductivePercentage = TeacherContract.SpringTotalPercentageForReduction();
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
            return DateTime.Today.Year - int.Parse(Dob.Substring(0,4));
        }
    }
}