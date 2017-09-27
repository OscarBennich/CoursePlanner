using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoursePlanDraft.Models
{
    [Table("Course")]
    public class CourseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CourseCode { get; set; }


        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        public string CourseClassificiation { get; set; }
        public string CourseType { get; set; }
        public string Credits { get; set; }
        public string HSTValue { get; set; }
        public string BudgetedNumberOfStudents { get; set; }
        public string CourseBudget { get; set; }
        public string Term { get; set; }
        public string Period { get; set; }
        public string StartDate { get; set; } //shouldn't be a string (TESTING)
        public string EndDate { get; set; }
    }
}

