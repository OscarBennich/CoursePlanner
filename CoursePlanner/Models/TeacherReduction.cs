using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    public enum ReductionType
    {
        Commitment,
        Research,
        Other
    }

    public class TeacherReduction
    {
        public int Id { get; set; }
        public ReductionType Type { get; set; }
        public string Description { get; set; }
        public Term Term { get; set; }
        public float Percentage { get; set; }
        
        public virtual TeacherContract TeacherContract { get; set; }

        public TeacherReduction() { }

        public TeacherReduction(ReductionType type, string description, Term term, float percentage, TeacherContract contract)
        {
            Type = type;
            Description = description;
            Term = term;
            Percentage = percentage;
            TeacherContract = contract;
        }

        public TeacherReduction(int id, ReductionType type, string description, Term term, float percentage, TeacherContract contract)
        {
            Id = id;
            Type = type;
            Description = description;
            Term = term;
            Percentage = percentage;
            TeacherContract = contract;
        }
        
    }
}