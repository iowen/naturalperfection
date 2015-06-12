using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.Models
{
    public class ClosedDatesModel
    {
        public ClosedDatesModel()
        {
            closedDates = new List<DateTime>();
            closedDatesStrings = new List<string>();
        }
        public List<DateTime> closedDates { get; set; }
        public List<string> closedDatesStrings { get; set; }
    }
    public class ClosedDatesJsonModel
    {
        public ClosedDatesJsonModel()
        {
            closedDates = new List<string>();
        }
        public List<string> closedDates { get; set; }
    }
    public class WorkScheduleModel
    {
        public WorkScheduleModel()
        {
            sun = new List<string>();
            mon  = new List<string>();
            tue = new List<string>();
            wed = new List<string>();
            thu = new List<string>();
            fri = new List<string>();
            sat = new List<string>();
        }
        public List<string> sun { get; set; }
        public List<string> mon { get; set; }
        public List<string> tue { get; set; }
        public List<string> wed { get; set; }
        public List<string> thu { get; set; }
        public List<string> fri { get; set; }
        public List<string> sat { get; set; }
    }
}
