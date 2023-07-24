using System;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.MAUI.Views;

namespace PracticePanther.MAUI.ViewModels
{
    // Represents the view model for a client
    internal class ClientViewModel : INotifyPropertyChanged
    {
        // Represents the client model
        public Client Model { get; set; }

        public string ProjectQuery { get; set; }


        // Collection of projects for the client
        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                // If this is a new client, return an empty collection
                if (Model == null || Model.Id == 0)
                {
                    return new ObservableCollection<ProjectViewModel>();
                }

                // Filter and select the projects for the current client
                return new ObservableCollection<ProjectViewModel>(ProjectService
                    .Current.Projects.Where(p => p.ClientId == Model.Id && p.Name.Contains(ProjectQuery ?? string.Empty))
                    .Select(r => new ProjectViewModel(r)));
            }
        }


        // Display id and name of the client
        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }

        // Command to delete the client
        public ICommand DeleteClientCommand { get; private set; }

        // Command to edit the client
        public ICommand EditClientCommand { get; private set; }

        // Command to add projects
        public ICommand AddProjectCommand { get; private set; }

        // Command to show projects
        public ICommand ShowProjectCommand { get; private set; }

        public ICommand TimeCommand { get; private set; }

        public ICommand GenerateBillCommand { get; private set; }

        public ICommand SearchProjectCommand { get; private set; }

        public ICommand ViewBillsCommand { get; private set; }


        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the property change event
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ExecuteViewBillsCommand()
        {
            Shell.Current.GoToAsync($"//Bills?clientId={Model.Id}");
        }
        public void ExecuteSearchProject()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        // Executes the delete command to delete the client
        public void ExecuteDeleteClient(int id)
        {
            ClientService.Current.Delete(id);
        }

        // Executes the edit command to navigate to the client detail page
        public void ExecuteEditClient(int id)
        {
            Shell.Current.GoToAsync($"//ClientDetail?clientId={id}");
        }

        public void ExecuteAddProject()
        {
            AddOrUpdate();
            Shell.Current.GoToAsync($"//ProjectDetail?clientId={Model.Id}");
        }
        public void ExecuteTime(ProjectViewModel projectViewModel)
        {
            var window = new Window()
            {
                Width = 250,
                Height = 350,
                X = 0,
                Y = 0
            };
            var view = new BillsDetailView(projectViewModel.Model.Id, window);
            window.Page = view;
            Application.Current.OpenWindow(window);
        }


        // Refreshes the client list
        public void RefreshClientList()
        {
            NotifyPropertyChanged(nameof(Client));
        }

        // Refreshes the project list
        public void RefreshProjectList()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        // Sets up the commands
        public void SetUpCommands()
        {
            DeleteClientCommand = new Command((c) => ExecuteDeleteClient((c as ClientViewModel).Model.Id));
            EditClientCommand = new Command((c) => ExecuteEditClient((c as ClientViewModel).Model.Id));
            AddProjectCommand = new Command((c) => ExecuteAddProject());
            TimeCommand = new Command<ProjectViewModel>(ExecuteTime);
            SearchProjectCommand = new Command(ExecuteSearchProject);
        }

        // Constructor with a client parameter
        public ClientViewModel(Client client)
        {
            // If a client is provided, use it as the model, otherwise create a new client
            if (client != null)
            {
                Model = client;
            }
            else
            {
                Model = new Client();
            }

            // Call function that sets up commands
            SetUpCommands();
        }

        // Constructor with a clientId parameter
        public ClientViewModel(int clientId)
        {
            // If the clientId is less than or equal to 0, create a new client, otherwise get the client from the service
            if (clientId <= 0)
            {
                Model = new Client();
            }
            else
            {
                Model = ClientService.Current.Get(clientId);
            }

            // Call function that sets up commands
            SetUpCommands();
        }

        // Default constructor
        public ClientViewModel()
        {
            Model = new Client();
            SetUpCommands();
        }

        // Adds or updates the client
        public void AddOrUpdate()
        {
                ClientService.Current.AddOrUpdate(Model);
        }




    }
}
