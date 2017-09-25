using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    public class TeacherModel
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

        public TeacherModel(int id, string name, DateTime dob, TeacherContract contract)
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
        public int GetAllHoursPerTerm()
        {
            return GetBaseAnnualHours() / 2;
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

        public int GetRemaingHoursFall()
        {
            return GetAllHoursPerTerm() - GetAllocatedHoursFall();
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

        public int GetRemaingHoursSpring()
        {
            return GetAllHoursPerTerm() - GetAllocatedHoursSpring();
        }

        private int getAge()
        {
            return DateTime.Today.Year - _dob.Year;
        }
    }
}