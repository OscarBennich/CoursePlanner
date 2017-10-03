using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    public enum Term
    {
        Fall, 
        Spring
    }

    public enum Position
    {
        Professor,
        Lektor,
        Adjunkt,
        Studievägledare,
        Doktorand,
        Amanuens
    }

    public enum ContractType
    {
        Temporary,
        Permanent
    }

    public class TeacherContract
    {
        //[Key]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float TotalPercentageFall { get; set; }
        public float TotalPercentageSpring { get; set; }
        public ContractType ContractType { get; set; }
        public string TemporaryContractEndDate { get; set; }
        public Position Position { get; set; }

        public virtual List<TeacherReduction> Reductions { get; set; }

        public TeacherContract() { }

        public TeacherContract(float percentageFall, float percentageString, Position position)
        {
            TotalPercentageFall = percentageFall;
            TotalPercentageSpring = percentageString;
            Position = position;

            Reductions = new List<TeacherReduction>();
        }

        public TeacherContract(int id, float percentageFall, float percentageString, Position position)
        {
            Id = id;
            TotalPercentageFall = percentageFall;
            TotalPercentageSpring = percentageString;
            Position = position;
            Reductions = new List<TeacherReduction>();
        }

        public TeacherContract(float percentageFall, float percentageString, Position position, List<TeacherReduction> reductions)
        {
            TotalPercentageFall = percentageFall;
            TotalPercentageSpring = percentageString;
            Position = position;
            Reductions = reductions;
        }

        public TeacherContract(int id, float percentageFall, float percentageString, Position position, List<TeacherReduction> reductions)
        {
            Id = id;
            TotalPercentageFall = percentageFall;
            TotalPercentageSpring = percentageString;
            Position = position;
            Reductions = reductions;
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
            return Reductions.Where(x => x.Term == Term.Fall).Sum(y => y.Percentage);
        }

        public float SpringTotalPercentageForReduction()
        {
            return Reductions.Where(x => x.Term == Term.Spring).Sum(y => y.Percentage);
        }

        public float FallTotalPercentageForReductionType(ReductionType type)
        {
            return Reductions.Where(x => x.Type == type && x.Term == Term.Fall).Sum(y => y.Percentage);
        }
        
        public float SpringTotalPercentageForReductionType(ReductionType type)
        {   
            return Reductions.Where(x => x.Type == type && x.Term == Term.Spring).Sum(y => y.Percentage);
        }

        public IEnumerable<IGrouping<ReductionType, string>> FallReductionDescriptionWithPercentageGroupedByType()
        {
            return Reductions.Where(x => x.Term == Term.Fall).GroupBy(x => x.Type, x => x.Description + " " + x.Percentage + "%");
        }

        public IEnumerable<IGrouping<ReductionType, string>> SpringReductionDescriptionWithPercentageGroupedByType()
        {
            return Reductions.Where(x => x.Term == Term.Spring).GroupBy(x => x.Type, x => x.Description + " " + x.Percentage + "%");
        }


    }
}