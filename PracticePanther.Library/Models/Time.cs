using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Library.Models
{
    internal class Time
    {
        public DateTime Date { get; set; }
        public string ? Narrative { get; set; }
        public Decimal Hours { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }

    }
}
