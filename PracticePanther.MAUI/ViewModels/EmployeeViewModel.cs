using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.MAUI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PracticePanther.MAUI.ViewModels
{
    internal class EmployeeViewModel
    {
        // Represents the employee model 
        public Employee Model { get; set; } 

        // Command to delete employee
        public ICommand DeleteEmployeeCommand { get; private set; }

        // Command to edit the client
        public ICommand EditEmployeeCommand { get; private set; }

        // Display employee info
        public string Display
        {
            get
            {
                return Model?.ToString() ?? string.Empty;
            }
        }

        // Executes the delete command 
        public void ExecuteDeleteEmployee(int id)
        {
            EmployeeService.Current.Delete(id);
        }

        // Executes the edit command 
        public void ExecuteEditEmployee(int id)
        {
            Shell.Current.GoToAsync($"//EmployeeDetail?employeeId={id}");
        }


        // Sets up the commands
        public void SetUpCommands()
        {
            DeleteEmployeeCommand = new Command((c) => ExecuteDeleteEmployee((c as EmployeeViewModel).Model.Id));
            EditEmployeeCommand = new Command((c) => ExecuteEditEmployee((c as EmployeeViewModel).Model.Id));
        }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the property change event
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor with a clientId parameter
        public EmployeeViewModel(int employeeId)
        {
            // If the clientId is less than or equal to 0, create a new client, otherwise get the client from the service
            if (employeeId <= 0)
            {
                Model = new Employee();
            }
            else
            {
                Model = EmployeeService.Current.Get(employeeId);
            }

           SetUpCommands();
        }

        // Constructor with an employee object
        public EmployeeViewModel(Employee employee)
        {
            if (employee != null)
            {
                Model = employee;
            } else
            {
                Model = new Employee();
            }

            SetUpCommands();
            
        }

        // Default constructor
        public EmployeeViewModel()
        {
            Model = new Employee();
        }

        // Adds or updates the employee
        public void AddOrUpdate()
        {
            EmployeeService.Current.AddOrUpdate(Model);
        }

        // Refreshes the employee list
        public void RefreshEmployeeList()
        {
            NotifyPropertyChanged(nameof(Employee));
        }
    }
}
