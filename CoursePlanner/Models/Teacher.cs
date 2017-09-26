using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    public class Teacher
    {
        private int _id;
        private string _name;
        private DateTime _dob;
        private TeacherContract _contractDetails;
        private int _allocatedHours;
        private string[] _ListOfCourses;

        public int Id
        {
            get { return _id; }
        }
        
        public string Name
        {
            get { return _name; }
        }

        public TeacherContract ContractDetails
        {
            get { return _contractDetails; }
        }

        public Teacher(int id, string name, DateTime dob, TeacherContract contract)
        {
            _id = id;
            _name = name;
            _dob = dob;
            _contractDetails = contract;

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

            float totalPercentage = _contractDetails.TotalPercentageFall;
            float reductivePercentage = _contractDetails.FallTotalPercentageForReduction();
            float teachingPercentage = totalPercentage * (1 - reductivePercentage);

            baseHours = (int)Math.Round(baseHours * teachingPercentage, MidpointRounding.ToEven);

            return baseHours; 
        }

        // Get hours per term
        public int GetAllTeachingHoursSpring()
        {
            int baseHours = GetBaseAnnualHours();

            float totalPercentage = _contractDetails.TotalPercentageSpring;
            float reductivePercentage = _contractDetails.SpringTotalPercentageForReduction();
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

            return _allocatedHours;
        }

        // Get hours for SPRING
        // Should in future get all hours allocated for teacher on the courses in the courses list!
        public int GetAllocatedHoursSpring()
        {
            // Modell som innehåller kurs, lärare och antal timmar
            // ANVÄND LINK: Where (this && "fall"), Select (Hours), sum (All)
            // Hämta alla där läraren är "this"
            // Summera alla timfält där term = fall

            return _allocatedHours;
        }

        public int GetRemaingHoursFall()
        {
            return GetAllTeachingHoursFall() - GetAllocatedHoursFall();
        }

        public int GetRemaingHoursSpring()
        {
            return GetAllTeachingHoursSpring() - GetAllocatedHoursSpring();
        }

        private int getAge()
        {
            return DateTime.Today.Year - _dob.Year;
        }
    }
}