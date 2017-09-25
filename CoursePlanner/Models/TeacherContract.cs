using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    public class TeacherContract
    {
        private DateTime _start;
        private DateTime _end;
        private int _procentage;
        private string _position;

        public DateTime Start
        {
            get { return _start; }
        }

        public DateTime End
        {
            get { return _end; }
        }

        public int Procentage
        {
            get { return _procentage; }
        }

        public string Position
        {
            get { return _position; }
        }

        public TeacherContract(DateTime start, DateTime end, int procentage, string position)
        {
            _start = start;
            _end = end;
            _procentage = procentage;
            _position = position; 
        }
    }
}