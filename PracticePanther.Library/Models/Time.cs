using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Library.Models
{
    public class Time
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ? Narrative { get; set; }
        public Decimal Hours { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the property change event
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Method that overloads ToString
        public override string ToString()
        {
            return $"{Id} {Name}";
        }

        
    }
}
