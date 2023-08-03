using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PracticePanther.MAUI.ViewModels
{
    internal class ClientViewViewModel : INotifyPropertyChanged
    {
        // The currently selected client
        public Client SelectedClient { get; set; }

        // Command to execute the search
        public ICommand SearchClientCommand { get; private set; }

        // The search query entered by the user
        public string ClientQuery { get; set; }

        // The collection of client view models to display
        public ObservableCollection<ClientViewModel> Clients
        {
            get
            {
                // Filter and map the clients based on the search query
                return new ObservableCollection<ClientViewModel>(
                    ClientService.Current.Search(ClientQuery ?? string.Empty)
                        .Select(c => new ClientViewModel(c)).ToList());
            }
        }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the property change event
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Default constructor
        public ClientViewViewModel()
        {
            SearchClientCommand = new Command(ExecuteSearchClientCommand);
        }

        // Executes the search command
        public void ExecuteSearchClientCommand()
        {
            NotifyPropertyChanged(nameof(Clients));
        }

        // Deletes the selected client
        public void Delete()
        {
            if (SelectedClient != null)
            {
                // Delete the client from the service
                ClientService.Current.Delete(SelectedClient.Id);

                // Clear the selected client
                SelectedClient = null;

                // Notify that the Clients collection and SelectedClient have changed
                NotifyPropertyChanged(nameof(Clients));
                NotifyPropertyChanged(nameof(SelectedClient));
            }
        }

        // Refreshes the client list
        public void RefreshClientList()
        {
            NotifyPropertyChanged(nameof(Clients));
        }
    }
}
