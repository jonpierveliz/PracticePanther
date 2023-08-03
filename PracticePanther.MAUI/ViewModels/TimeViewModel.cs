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
    internal class TimeViewModel
    {
        // Represents the time entry model
        public Time Model { get; set; }

        // Command to delete time entry
        public ICommand DeleteTimeEntryCommand { get; private set; }

        // Command to edit the time entry
        public ICommand EditTimeEntryCommand { get; private set; }

        // Display time info
        public string Display
        {
            get
            {
                return Model?.ToString() ?? string.Empty;
            }
        }

        // Executes the delete command 
        public void ExecuteDeleteTimeEntry(int id)
        {
            TimeService.Current.Delete(id);
        }

        // Executes the edit command 
        public void ExecuteEditTimeEntry(int id)
        {
            Shell.Current.GoToAsync($"//TimeDetail?timeEntryId={id}");
        }

        // Sets up the commands
        public void SetUpCommands()
        {
            DeleteTimeEntryCommand = new Command((c) => ExecuteDeleteTimeEntry((c as TimeViewModel).Model.Id));
            EditTimeEntryCommand = new Command((c) => ExecuteEditTimeEntry((c as TimeViewModel).Model.Id));
        }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the property change event
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor with a time id parameter
        public TimeViewModel(int timeEntryId)
        {
            // If the clientId is less than or equal to 0, create a new client, otherwise get the client from the service
            if (timeEntryId <= 0)
            {
                Model = new Time();
            }
            else
            {
                Model = TimeService.Current.Get(timeEntryId);
            }
            // Set the name based on the time entry's ID
            Model.Name = $"Time Entry {Model.Id}";
            SetUpCommands();
        }

        // Constructor that takes in time object
        public TimeViewModel(Time timeEntry)
        {
            if (timeEntry != null)
            {
                Model = timeEntry;
            }
            else
            {
                Model = new Time();
            }

            // Set the name based on the time entry's ID
            Model.Name = $"Time Entry {Model.Id}";
            SetUpCommands();

        }

        // Default Constructor
        public TimeViewModel()
        {
            Model = new Time();
            // Set the name based on the time entry's ID
            Model.Name = $"Time Entry {Model.Id}";
            SetUpCommands();

        }

        // Adds or updates the employee
        public void AddOrUpdate()
        {
            TimeService.Current.AddOrUpdate(Model);
        }

        // Refreshes the time list
        public void RefreshTimeEntriesList()
        {
            NotifyPropertyChanged(nameof(Time));
        }
    }
}
