using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PracticePanther.MAUI.ViewModels
{
    internal class TimeViewViewModel : INotifyPropertyChanged
    {
        // Represents the selected time entry
        public Time SelectedTimeEntry { get; set; }

        // Command to execute the search
        public ICommand SearchTimeEntryCommand { get; private set; }

        // The search query entered by the user
        public string TimeEntryQuery { get; set; }

        // List of time 
        public ObservableCollection<TimeViewModel> TimeEntries
        {
            get
            {
                // Filter and map the time entries based on the search query
                return new ObservableCollection<TimeViewModel>(
                    TimeService.Current.Search(TimeEntryQuery ?? string.Empty)
                           .Select(c => new TimeViewModel(c)).ToList());
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
        public TimeViewViewModel()
        {
            SearchTimeEntryCommand = new Command(ExecuteSearchTimeEntryCommand);
        }

        // Executes the search command
        public void ExecuteSearchTimeEntryCommand()
        {
            NotifyPropertyChanged(nameof(TimeEntries));
        }

        // Refreshes the client list
        public void RefreshTimeEntriesList()
        {
            NotifyPropertyChanged(nameof(TimeEntries));
        }

        // Deletes the selected timeemployee
        public void Delete()
        {
            if (SelectedTimeEntry != null)
            {
                // Delete the time from the service
                ClientService.Current.Delete(SelectedTimeEntry.Id);

                // Clear the selected time
                SelectedTimeEntry = null;

                // Notify that the time collection and Selectedtime have changed
                NotifyPropertyChanged(nameof(TimeEntries));
                NotifyPropertyChanged(nameof(SelectedTimeEntry));
            }
        }

    }
}
