﻿using PracticePanther.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Library.DTO
{
    public class ClientDTO
    {
        public ClientDTO()
        {
            Name = string.Empty;
        }

        public ClientDTO(Client c)

        {
            this.Id = c.Id; 
            this.Name = c.Name; 
        }
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public bool IsActive { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }

        // Method that overloads ToString
        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
}
