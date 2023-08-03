using PracticePanther.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Library.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Client()
        {
            Name = string.Empty;
        }

        public Client(ClientDTO dto)
        {
            this.Id = dto.Id;
            this.Name = dto.Name;
        }

        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }
        public string Property5 { get; set; }
        public string Property6 { get; set; }
        public string Property7 { get; set; }
        public string Property8 { get; set; }
        public string Property9 { get; set; }
        public string Property10 { get; set; }

    }
}
