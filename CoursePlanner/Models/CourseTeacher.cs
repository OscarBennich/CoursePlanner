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
    
    public partial class CourseTeacher
    {
        public int CourseOccurrenceId { get; set; }
        public int TeacherId { get; set; }
        public Nullable<int> Hours { get; set; }
    
        public virtual CourseOccurrence CourseOccurrence { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}