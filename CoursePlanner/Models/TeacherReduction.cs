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
    
    public partial class TeacherReduction
    {
        public int ReductionId { get; set; }
        public int TeacherId { get; set; }
        public Nullable<ReductionTypes> ReductionType { get; set; }
        public Nullable<Terms> Term { get; set; }
        public Nullable<double> Percentage { get; set; }
    
        public virtual Teacher Teacher { get; set; }
    }
}
