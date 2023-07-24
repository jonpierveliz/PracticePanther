using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PracticePanther.Library.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DueDate { get; set; }

        // Method that overloads ToString
        public override string ToString()
        {
            return $"{Id} {TotalAmount}";
        }

    }
}
