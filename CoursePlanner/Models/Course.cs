//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoursePlanner.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Course
    {
        public Course()
        {
            this.CourseOccurrence = new HashSet<CourseOccurrence>();
        }
    
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public CourseClassifications CourseClassificiation { get; set; }
        public CourseTypes CourseType { get; set; }
        public CourseLevels CourseLevel { get; set; }
        public CourseCredits CourseCredit { get; set; }
    
        public virtual ICollection<CourseOccurrence> CourseOccurrence { get; set; }
    }
}
