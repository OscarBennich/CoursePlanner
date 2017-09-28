using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    public class CourseModel
    {
        public int Id { get; set; }

        public string CourseCode { get; set; }

        public string CourseName { get; set; }

        public string CourseClassificiation { get; set; }
        public string CourseType { get; set; }
        public string Credits { get; set; }
        public string HSTValue { get; set; }
        public string BudgetedNumberOfStudents { get; set; }
        public string CourseBudget { get; set; }
        public string Term { get; set; }
        public string Period { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}

