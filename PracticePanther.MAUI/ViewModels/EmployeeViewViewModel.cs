using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PracticePanther.MAUI.ViewModels
{
    // Represents the view model for the EmployeeView
    internal class EmployeeViewViewModel : INotifyPropertyChanged
    {
        public Employee SelectedEmployee { get; set; }

        // Command to execute the search
        public ICommand SearchEmployeeCommand { get; private set; }

        // The search query entered by the user
        public string EmployeeQuery { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor
        public EmployeeViewViewModel()
        {
            // Command to execute the search
            SearchEmployeeCommand = new Command(ExecuteSearchEmployeeCommand);
        }

        // Executes the search command
        public void ExecuteSearchEmployeeCommand()
        {
            // Notify that the Clients collection has changed
            NotifyPropertyChanged(nameof(Employees));
        }

        // Refreshes the client list
        public void RefreshEmployeeList()
        {
            // Notify that the Clients collection has changed
            NotifyPropertyChanged(nameof(Employees));
        }

        // Deletes the selected employee
        public void Delete()
        {
            if (SelectedEmployee != null)
            {
                // Delete the client from the service
                ClientService.Current.Delete(SelectedEmployee.Id);

                // Clear the selected client
                SelectedEmployee = null;

                // Notify that the Clients collection and SelectedClient have changed
                NotifyPropertyChanged(nameof(Employees));
                NotifyPropertyChanged(nameof(SelectedEmployee));
            }
        }

    }
}
