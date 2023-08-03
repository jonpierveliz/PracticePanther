using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PracticePanther.MAUI.ViewModels
{
    internal class EmployeeViewViewModel : INotifyPropertyChanged
    {
        // Represents the currently selected employee
        public Employee SelectedEmployee { get; set; }

        // Command to execute the search
        public ICommand SearchEmployeeCommand { get; private set; }

        // The search query entered by the user
        public string EmployeeQuery { get; set; }

        // Collection of employee view models based on the search query
        public ObservableCollection<EmployeeViewModel> Employees
        {
            get
            {
                // Filter and map the employees based on the search query
                return new ObservableCollection<EmployeeViewModel>(
                    EmployeeService.Current.Search(EmployeeQuery ?? string.Empty)
                           .Select(c => new EmployeeViewModel(c)).ToList());
            }
        }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the property change event
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor
        public EmployeeViewViewModel()
        {
            SearchEmployeeCommand = new Command(ExecuteSearchEmployeeCommand);
        }

        // Executes the search command
        public void ExecuteSearchEmployeeCommand()
        {
            NotifyPropertyChanged(nameof(Employees));
        }

        // Refreshes the employee list
        public void RefreshEmployeeList()
        {
            NotifyPropertyChanged(nameof(Employees));
        }

        // Deletes the selected employee
        public void Delete()
        {
            if (SelectedEmployee != null)
            {
                // Delete the employee from the service
                EmployeeService.Current.Delete(SelectedEmployee.Id);

                // Clear the selected employee
                SelectedEmployee = null;

                // Notify that the Employees collection and SelectedEmployee have changed
                NotifyPropertyChanged(nameof(Employees));
                NotifyPropertyChanged(nameof(SelectedEmployee));
            }
        }
    }
}
