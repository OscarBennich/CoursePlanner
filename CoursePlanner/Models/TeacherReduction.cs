using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    public class TeacherReduction
    {
        private int _id;
        private string _type;
        private string _term;
        private string _description;
        private int _procentage;

        public enum reductionTypes
        {
            Commitment,
            Research,
            Other

        }

        public int Id
        {
            get { return _id; }
        }

        public string Type
        {
            get { return _type; }
        }

        public string Description
        {
            get { return _description; }
        }

        public string Term
        {
            get { return _term; }
        }

        public int Procentage
        {
            get { return _procentage; }
        }

        public TeacherReduction(int id, reductionTypes type, string description, string term, int procentage)
        {
            _id = id;
            _type = type.ToString();
            _description = description;
            _term = term;
            _procentage = procentage;
        }
        
    }
}