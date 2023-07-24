using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PracticePanther.MAUI.ViewModels
{
    // Represents the view model for the TimeView
    internal class TimeViewViewModel : INotifyPropertyChanged
    {
        public Time SelectedTimeEntry { get; set; }

        // Command to execute the search
        public ICommand SearchTimeEntryCommand { get; private set; }

        // The search query entered by the user
        public string TimeEntryQuery { get; set; }

        public ObservableCollection<TimeViewModel> TimeEntries
        {
            get
            {
                // Filter and map the employees based on the search query
                return new ObservableCollection<TimeViewModel>(
                    TimeService.Current.Search(TimeEntryQuery ?? string.Empty)
                           .Select(c => new TimeViewModel(c)).ToList());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor
        public TimeViewViewModel()
        {
            // Command to execute the search
            SearchTimeEntryCommand = new Command(ExecuteSearchTimeEntryCommand);
        }

        // Executes the search command
        public void ExecuteSearchTimeEntryCommand()
        {
            // Notify that the Clients collection has changed
            NotifyPropertyChanged(nameof(TimeEntries));
        }

        // Refreshes the client list
        public void RefreshTimeEntriesList()
        {
            NotifyPropertyChanged(nameof(TimeEntries));
        }

        // Deletes the selected employee
        public void Delete()
        {
            if (SelectedTimeEntry != null)
            {
                // Delete the client from the service
                ClientService.Current.Delete(SelectedTimeEntry.Id);

                // Clear the selected client
                SelectedTimeEntry = null;

                // Notify that the Clients collection and SelectedClient have changed
                NotifyPropertyChanged(nameof(TimeEntries));
                NotifyPropertyChanged(nameof(SelectedTimeEntry));
            }
        }

    }
}
