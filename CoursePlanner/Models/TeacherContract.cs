using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursePlanner.Models
{
    public class TeacherContract
    {
        private DateTime _start;
        private DateTime _end;
        private int _totalProcentageFall;
        private int _totalProcentageSpring;
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

        public int TotalPricentageFall
        {
            get { return _totalProcentageFall; }
            set { _totalProcentageFall = value; }
        }

        public int TotalPricentageSpring
        {
            get { return _totalProcentageSpring; }
            set { _totalProcentageSpring = value; }
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

        public TeacherContract(DateTime start, DateTime end, int procentageFall, int procentageString,  string position)
        {
            _start = start;
            _end = end;
            _totalProcentageFall = procentageFall;
            _totalProcentageSpring = procentageString;
            _position = position;
            _reductions = new List<TeacherReduction>();
        }

        public TeacherContract(DateTime start, DateTime end, int procentageFall, int procentageString, string position, List<TeacherReduction> reductions)
        {
            _start = start;
            _end = end;
            _totalProcentageFall = procentageFall;
            _totalProcentageSpring = procentageString;
            _position = position;
            _reductions = reductions;
        }

        public void AddReduction(TeacherReduction reduction)
        {
            if(_reductions.Sum(x => x.Procentage) < 100)
                _reductions.Add(reduction);
            // how are we going to do error handling?? 
        }

        public void RemoveReduction(TeacherReduction reduction)
        {
            _reductions.Remove(reduction);
        }

        public int TotalProcentageForReductionTypeFall(TeacherReduction.reductionTypes type)
        {
            return _reductions.Where(x => x.Type == type.ToString() && x.Term == "Fall").Sum(y => y.Procentage);
        }

        public int TotalProcentageForReductionTypeSpring(TeacherReduction.reductionTypes type)
        {
            return _reductions.Where(x => x.Type == type.ToString() && x.Term == "Spring").Sum(y => y.Procentage);
        }

        public IEnumerable<IGrouping<string, string>> FallReductionDescriptionWithProcentageGroupedByType()
        {
            return _reductions.Where(x => x.Term == "Fall").GroupBy(x => x.Type, x => x.Description + x.Procentage.ToString());
        }

        public IEnumerable<IGrouping<string, string>> SpringReductionDescriptionWithProcentageGroupedByType()
        {
            return _reductions.Where(x => x.Term == "Spring").GroupBy(x => x.Type, x => x.Description + x.Procentage.ToString());
        }


    }
}