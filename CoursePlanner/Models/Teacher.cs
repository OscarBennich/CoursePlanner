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
    
    public partial class Teacher
    {
        public Teacher()
        {
            this.CourseOccurrence = new HashSet<CourseOccurrence>();
            this.CourseTeacher = new HashSet<CourseTeacher>();
            this.TeacherReduction = new HashSet<TeacherReduction>();
            this.BaseMessage = new HashSet<BaseMessage>();
            this.BaseMessage1 = new HashSet<BaseMessage>();
        }
    
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public System.DateTime TeacherDateOfBirth { get; set; }
        public Nullable<int> TeacherUserId { get; set; }
        public Nullable<Positions> TeacherPosition { get; set; }
        public Nullable<double> TotalPercentageFall { get; set; }
        public Nullable<double> TotalPercentageSpring { get; set; }
        public Nullable<ContractTypes> TeacherContractType { get; set; }
        public Nullable<System.DateTime> FixedContractEndDate { get; set; }
    
        public virtual ICollection<CourseOccurrence> CourseOccurrence { get; set; }
        public virtual ICollection<CourseTeacher> CourseTeacher { get; set; }
        public virtual ICollection<TeacherReduction> TeacherReduction { get; set; }
        public virtual ICollection<BaseMessage> BaseMessage { get; set; }
        public virtual ICollection<BaseMessage> BaseMessage1 { get; set; }
    }
}
