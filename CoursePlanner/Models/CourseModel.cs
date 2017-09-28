using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
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
        [Display(Name = "Name")]
        public string CourseName { get; set; }

        [Display(Name = "Classification")]
        public string CourseClassificiation { get; set; }
        [Display(Name = "Type")]
        public string CourseType { get; set; }

        [Display(Name = "Credits")]
        public string Credits { get; set; }
        [Display(Name = "HST")]
        public string HSTValue { get; set; }
        [Display(Name = "Budgeted Number of Students")]
        public string BudgetedNumberOfStudents { get; set; }
        [Display(Name = "Budget")]
        public string CourseBudget { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }
        [Display(Name = "Period")]
        public string Period { get; set; }
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
    }
}

