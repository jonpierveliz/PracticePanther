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

        // Represents the project model
        public Bill Model { get; set; }

        private List<Time> timeEntries;

        public List<Time> TimeEntries
        {
            get => timeEntries;
            set
            {
                timeEntries = value;
                NotifyPropertyChanged();
            }
        }

        private List<Bill> bills;

        public List<Bill> Bills
        {
            get => bills;
            set
            {
                bills = value;
                NotifyPropertyChanged();
            }
        }

        public string Display
        {
            get
            {
                return Model?.ToString() ?? string.Empty;
            }
        }


        public ICommand SubmitCommand { get; private set; }

        public ICommand OkCommand { get; private set; }

        
        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the property change event
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int ProjectId { get; }
        public Window ParentWindow { get; }

        public void ExecuteOk()
        {
            Application.Current.CloseWindow(ParentWindow);
        }
        public void ExecuteSubmit()
        {
            var selectedTimeEntry = TimeEntries.Where(te => te.IsSelected).ToList();

            if (selectedTimeEntry != null)
            {
                decimal totalAmount = selectedTimeEntry.Sum(t => t.Hours * GetEmployeeRate(t.EmployeeId));


                // Create a Bill object based on the selected time entry
                var bill = new Bill
                {
                    ProjectId = ProjectId,
                    TotalAmount = totalAmount,
                    DueDate = DateTime.Today.AddDays(7),
                };

                // Add the bill to the BillService
                BillService.Current.AddBill(bill);

                
            }
        }

        public void SetUpCommands()
        {
            SubmitCommand = new Command(ExecuteSubmit);
            OkCommand = new Command(ExecuteOk);
        }
        public BillsViewModel(int projectId, Window parentWindow)
        {
            ProjectId = projectId;
            ParentWindow = parentWindow;

            LoadBills();
            LoadTimeEntries();
            SetUpCommands();
            RefreshBills();
         
     
        }

        public BillsViewModel(int projectId, int clientId, Window parentWindow)
        {
            projectId = projectId; // Store the passed projectId
            clientId = clientId; // Store the passed clientId
            ParentWindow = parentWindow;

            LoadBills();
            LoadTimeEntries();
            SetUpCommands();
            RefreshBills();
        }


        public BillsViewModel(Bill bill)
        {
            Model = bill;
        }
        private void LoadTimeEntries()
        {
            TimeEntries = TimeService.Current.TimeEntries
                .FindAll(t => t.ProjectId == ProjectId);
        }

        public void SelectTimeEntry(Time timeEntry)
        {
            timeEntry.IsSelected = !timeEntry.IsSelected;
        }

        public decimal GetEmployeeRate(int employeeId)
        {
            var employee = EmployeeService.Current.Get(employeeId);
            return employee.Rate;
        }

        

        private void LoadBills()
        {
            // Assuming BillService.Current.Bills returns a list of all bills.
            // You might need to modify this depending on how you store your data.
            Bills = BillService.Current.Bills
                        .FindAll(b => b.ProjectId == ProjectId);
        }

        public void RefreshBills()
        {
            NotifyPropertyChanged(nameof(Bills));
        }

        private void ViewBillsClicked(object sender, EventArgs e)
        {
            var selectedClient = ((Button)sender).BindingContext as ClientViewModel;

            if (selectedClient != null)
            {
                // Open the BillsView for the selected client
                var window = new Window()
                {
                    Width = 250,
                    Height = 350,
                    X = 0,
                    Y = 0
                };

                var billsView = new BillsView(selectedClient.Model.Id, window);
                window.Page = billsView;

                Application.Current.OpenWindow(window);
            }
        }
    }
}
