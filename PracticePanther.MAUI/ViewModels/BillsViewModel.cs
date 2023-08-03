using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.MAUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PracticePanther.MAUI.ViewModels
{
    public class BillsViewModel : INotifyPropertyChanged
    {
        // Reference to the Bill model
        public Bill Model { get; set; }

        // List of time entries related to the current project
        private List<Time> timeEntries;

        // List of bills related to the current project
        private List<Bill> bills;

        // List to track changes in the TimeEntries list
        public List<Time> TimeEntries
        {
            get => timeEntries;
            set
            {
                timeEntries = value;
                NotifyPropertyChanged();
            }
        }

        // List to track changes in the Bills list
        public List<Bill> Bills
        {
            get => bills;
            set
            {
                bills = value;
                NotifyPropertyChanged();
            }
        }

        // Property to display bill info
        public string Display
        {
            get
            {
                return Model?.ToString() ?? string.Empty;
            }
        }

        // Command to submit the selected time entries and create a bill
        public ICommand SubmitCommand { get; private set; }

        // Command to close the current window
        public ICommand OkCommand { get; private set; }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the property change event
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Stores the ProjectId and the parent window
        public int ProjectId { get; }
        public Window ParentWindow { get; }

        // Method to execute the OkCommand and close the current window
        public void ExecuteOk()
        {
            Application.Current.CloseWindow(ParentWindow);
        }

        // Method to execute the SubmitCommand and create a new bill
        public void ExecuteSubmit()
        {
            var selectedTimeEntry = TimeEntries.Where(te => te.IsSelected).ToList();

            if (selectedTimeEntry != null)
            {
                // Calculate the total amount based on the selected time entries and their associated employee rates
                decimal totalAmount = selectedTimeEntry.Sum(t => t.Hours * GetEmployeeRate(t.EmployeeId));

                // Create a Bill object based on the selected time entries
                var bill = new Bill
                {
                    ProjectId = ProjectId,
                    TotalAmount = totalAmount,
                    DueDate = DateTime.Today.AddDays(7),
                };

                // Add the newly created bill to the BillService.
                BillService.Current.AddBill(bill);
            }
        }

        // Method to set up the SubmitCommand and OkCommand
        public void SetUpCommands()
        {
            SubmitCommand = new Command(ExecuteSubmit);
            OkCommand = new Command(ExecuteOk);
        }

        // Constructor with only projectId and parent window
        public BillsViewModel(int projectId, Window parentWindow)
        {
            ProjectId = projectId;
            ParentWindow = parentWindow;

            LoadBills();
            LoadTimeEntries();
            SetUpCommands();
        }

        // Constructor with an existing Bill object
        public BillsViewModel(Bill bill)
        {
            Model = bill;
        }

        // Load the time entries associated with the current project
        private void LoadTimeEntries()
        {
            TimeEntries = TimeService.Current.TimeEntries
                .FindAll(t => t.ProjectId == ProjectId);
        }

        // Toggle the selection status of a time entry
        public void SelectTimeEntry(Time timeEntry)
        {
            timeEntry.IsSelected = !timeEntry.IsSelected;
        }

        // Get the employee rate based on the employeeId
        public decimal GetEmployeeRate(int employeeId)
        {
            var employee = EmployeeService.Current.Get(employeeId);
            return employee.Rate;
        }

        // Load the bills associated with the current project
        private void LoadBills()
        {
            Bills = BillService.Current.Bills
                .FindAll(b => b.ProjectId == ProjectId);
        }

    }
}
