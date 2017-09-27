using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    public enum Position
    {
        Professor,
        Lektor,
        Adjunkt,
        Studievägledare,
        Doktorand,
        Amanuens
    }

    [Table("TeacherContract")]
    public class TeacherContract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public float TotalPercentageFall { get; set; }
        public float TotalPercentageSpring { get; set; }
        public Position Position { get; set; }
        public virtual List<TeacherReduction> Reductions { get; set; }

        public TeacherContract(DateTime start, DateTime end, float percentageFall, float percentageString, Position position)
        {
            Start = start;
            End = end;
            TotalPercentageFall = percentageFall;
            TotalPercentageSpring = percentageString;
            Position = position;
            // Reductions = new List<TeacherReduction>();
        }

        public TeacherContract(DateTime start, DateTime end, float percentageFall, float percentageString, Position position, List<TeacherReduction> reductions)
        {
            Start = start;
            End = end;
            TotalPercentageFall = percentageFall;
            TotalPercentageSpring = percentageString;
            Position = position;
            // Reductions = reductions;
        }

        public void AddReduction(TeacherReduction reduction)
        {
            if (Reductions.Sum(x => x.Percentage) < 1) // Should 1 be fixed?  
                Reductions.Add(reduction);
            // how are we going to do error handling?? 
        }

        public void RemoveReduction(TeacherReduction reduction)
        {
            Reductions.Remove(reduction);
        }

        public float FallTotalPercentageForReduction()
        {
            return Reductions.Where(x => x.Term == "Fall").Sum(y => y.Percentage);
        }

        public float SpringTotalPercentageForReduction()
        {
            return Reductions.Where(x => x.Term == "Spring").Sum(y => y.Percentage);
        }

        public float FallTotalPercentageForReductionType(ReductionType type)
        {
            return Reductions.Where(x => x.Type == type && x.Term == "Fall").Sum(y => y.Percentage);
        }
        
        public float SpringTotalPercentageForReductionType(ReductionType type)
        {   
            return Reductions.Where(x => x.Type == type && x.Term == "Spring").Sum(y => y.Percentage);
        }

        public IEnumerable<IGrouping<ReductionType, string>> FallReductionDescriptionWithPercentageGroupedByType()
        {
            return Reductions.Where(x => x.Term == "Fall").GroupBy(x => x.Type, x => x.Description + " " + x.Percentage + "%");
        }

        public IEnumerable<IGrouping<ReductionType, string>> SpringReductionDescriptionWithPercentageGroupedByType()
        {
            return Reductions.Where(x => x.Term == "Spring").GroupBy(x => x.Type, x => x.Description + " " + x.Percentage + "%");
        }


    }
}