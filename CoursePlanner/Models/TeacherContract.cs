using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    [Table("TeacherContract")]
    public class TeacherContract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        private int _contractId;

        private DateTime _start;
        private DateTime _end;
        private float _totalPercentageFall;
        private float _totalPercentageSpring;
        private List<TeacherReduction> _reductions;
        private string _position;

        public DateTime Start
        {
            get { return _start; }
            set { _start = value; }
        }

        public DateTime End
        {
            get { return _end; }
            set { _end = value; }
        }

        public float TotalPercentageFall
        {
            get { return _totalPercentageFall; }
            set { _totalPercentageFall = value; }
        }

        public float TotalPercentageSpring
        {
            get { return _totalPercentageSpring; }
            set { _totalPercentageSpring = value; }
        }

        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public List<TeacherReduction> Reductions
        {
            get { return _reductions; }
        }

        public TeacherContract(DateTime start, DateTime end, float percentageFall, float percentageString, string position)
        {
            _start = start;
            _end = end;
            _totalPercentageFall = percentageFall;
            _totalPercentageSpring = percentageString;
            _position = position;
            _reductions = new List<TeacherReduction>();
        }

        public TeacherContract(DateTime start, DateTime end, float percentageFall, float percentageString, string position, List<TeacherReduction> reductions)
        {
            _start = start;
            _end = end;
            _totalPercentageFall = percentageFall;
            _totalPercentageSpring = percentageString;
            _position = position;
            _reductions = reductions;
        }

        public void AddReduction(TeacherReduction reduction)
        {
            if (_reductions.Sum(x => x.Percentage) < 1) // Should 100 be fixed?  
                _reductions.Add(reduction);
            // how are we going to do error handling?? 
        }

        public void RemoveReduction(TeacherReduction reduction)
        {
            _reductions.Remove(reduction);
        }

        public float FallTotalPercentageForReduction()
        {
            return _reductions.Where(x => x.Term == "Fall").Sum(y => y.Percentage);
        }

        public float SpringTotalPercentageForReduction()
        {
            return _reductions.Where(x => x.Term == "Spring").Sum(y => y.Percentage);
        }

        public float FallTotalPercentageForReductionType(TeacherReduction.reductionTypes type)
        {
            return _reductions.Where(x => x.Type == type.ToString() && x.Term == "Fall").Sum(y => y.Percentage);
        }

        public float SpringTotalPercentageForReductionType(TeacherReduction.reductionTypes type)
        {
            return _reductions.Where(x => x.Type == type.ToString() && x.Term == "Spring").Sum(y => y.Percentage);
        }

        public IEnumerable<IGrouping<string, string>> FallReductionDescriptionWithPercentageGroupedByType()
        {
            return _reductions.Where(x => x.Term == "Fall").GroupBy(x => x.Type, x => x.Description + " " + x.Percentage + "%");
        }

        public IEnumerable<IGrouping<string, string>> SpringReductionDescriptionWithPercentageGroupedByType()
        {
            return _reductions.Where(x => x.Term == "Spring").GroupBy(x => x.Type, x => x.Description + " " + x.Percentage + "%");
        }


    }
}