﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Library.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public decimal Rate { get; set; }

        // Method that overloads ToString
        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
}
