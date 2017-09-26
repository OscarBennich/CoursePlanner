using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    [Table("TeacherReduction")]
    public class TeacherReduction
    {
        private int _id;
        private string _type;
        private string _term;
        private string _description;
        private float _percentage;

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

        public float Percentage
        {
            get { return _percentage; }
        }

        public TeacherReduction(int id, reductionTypes type, string description, string term, float percentage)
        {
            _id = id;
            _type = type.ToString();
            _description = description;
            _term = term;
            _percentage = percentage;
        }
        
    }
}