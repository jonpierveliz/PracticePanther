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
using PracticePanther.Library.DTO;

namespace PracticePanther.MAUI.ViewModels
{
    internal class ClientViewModel : INotifyPropertyChanged
    {
        // Represents the client model
        public ClientDTO Model { get; set; }

        // Property to store the query used for filtering projects
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

        // Display client info
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

        // Command to search for projects
        public ICommand SearchProjectCommand { get; private set; }

        // Command to see time entries
        public ICommand TimeCommand { get; private set; }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the property change event
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Executes the command to filter the projects based on the query
        public void ExecuteSearchProject()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        // Executes the command to delete the client
        public void ExecuteDeleteClient(int id)
        {
            ClientService.Current.Delete(id);
        }

        // Executes the command to navigate to the client detail page
        public void ExecuteEditClient(int id)
        {
            Shell.Current.GoToAsync($"//ClientDetail?clientId={id}");
        }

        // Executes the command to add a new project for the client
        public void ExecuteAddProject()
        {
            AddOrUpdate();
            Shell.Current.GoToAsync($"//ProjectDetail?clientId={Model.Id}");
        }

        // Executes the comand to open a window to manage bills and time entries for a project
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
            NotifyPropertyChanged(nameof(ClientDTO));
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
        public ClientViewModel(ClientDTO client)
        {
            // If a client is provided, use it as the model, otherwise create a new client
            if (client != null)
            {
                Model = client;
            }
            else
            {
                Model = new ClientDTO();
            }

            SetUpCommands();
        }

        // Constructor with a clientId parameter
        public ClientViewModel(int clientId)
        {
           
            if (clientId > 0)
            {
                Model = ClientService.Current.Get(clientId);
            }
            else
            {
                Model = new ClientDTO();
            }

            SetUpCommands();
        }

        // Default constructor
        public ClientViewModel()
        {
            Model = new ClientDTO();
            SetUpCommands();
        }

        // Adds or updates the client
        public void AddOrUpdate()
        {
            ClientService.Current.AddOrUpdate(Model);
        }
    }
}
