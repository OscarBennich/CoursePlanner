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

    [Table("TeacherReduction")]
    public class TeacherReduction
    {
        // Will be primary key
        public int Id { get; set; }
        public ReductionType Type { get; set; }
        public string Description { get; set; }
        public string Term { get; set; }
        public float Percentage { get; set; }

        // Foreign key
        public int ContractId { get; set; }

        public TeacherReduction(int id, ReductionType type, string description, string term, float percentage)
        {
            Id = id;
            Type = type;
            Description = description;
            Term = term;
            Percentage = percentage;
        }
        
    }
}