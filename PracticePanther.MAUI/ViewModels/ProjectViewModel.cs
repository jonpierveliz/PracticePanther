using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PracticePanther.MAUI.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PracticePanther.MAUI.ViewModels
{
    class ProjectViewModel
    {
        // Represents the project model
        public Project Model { get; set; }

        // Command to add project
        public ICommand AddProjectCommand { get; set; }

        // Command to delete project
        public ICommand DeleteProjectCommand { get; private set; }

        // Commmand to cancel process
        public ICommand CancelCommand { get; private set; }

        // Command to edit project
        public ICommand EditProjectCommand { get; private set; }

        // Command for bill 
        public ICommand BillCommand { get; private set; }

        // Display project info
        public string Display
        {
            get
            {
                return Model.ToString();
            }
        }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the property change event
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
       
        // Executes the edit command 
        private void ExecuteEditProject()
        {
            Shell.Current.GoToAsync($"//ProjectDetail?clientId={Model.ClientId}&projectId={Model.Id}");
        }

        // Executes the cancel command 
        private void ExecuteCancelProject()
        {
            Shell.Current.GoToAsync($"//ClientDetail?clientId={Model.ClientId}");
        }

       // Executes the delete command
        private void ExecuteDeleteProject(int id)
        {
            ProjectService.Current.Delete(id);
        }

        // Executes the bill command 
        private void ExecuteBill()
        {
            var window = new Window()
            {
                Width = 250,
                Height = 350,
                X = 0,
                Y = 0
            };
            var view = new BillsDetailView(Model.Id, window);
            window.Page = view;
            Application.Current.OpenWindow(window);
        }

        // Sets up the commands for the view model
        public void SetUpCommands()
        {
            DeleteProjectCommand = new Command((c) => ExecuteDeleteProject((c as ProjectViewModel).Model.Id));
            CancelCommand = new Command(ExecuteCancelProject);
            EditProjectCommand = new Command(ExecuteEditProject);
            BillCommand = new Command(ExecuteBill);
        }

        // Constructor that takes a client id
        public ProjectViewModel(int clientId, int projectId)
        {
            if(projectId > 0)
            {
                // edit
                Model = ProjectService.Current.Get(projectId);
            } else
            {
                // add
                Model = new Project { ClientId = clientId };
            }

            SetUpCommands();
        }


        // Default constructor
        public ProjectViewModel()
        {
            Model = new Project();
            SetUpCommands();
        }

        // Constructor that takes a project model
        public ProjectViewModel(Project project)
        {

            if (project != null)
            {
                Model = project;
            }
            else
            {
                Model = new Project();
            }

            SetUpCommands();
        }

        // Adds or updates the client
        public void AddOrUpdate()
        {
            ProjectService.Current.AddOrUpdate(Model);
        }

    }
}

