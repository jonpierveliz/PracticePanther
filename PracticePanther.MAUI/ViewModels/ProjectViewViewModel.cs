using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
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
    internal class ProjectViewViewModel
    {
        // Represents the client associated with the projects
        public Client Client { get; set; }

        // The currently selected projcet 
        public Project SelectedProject { get; set; }

        // Command to execute the search 
        public ICommand SearchProjectCommand { get; private set; }  

        // The search query entered by the user 
        public string ProjectQuery { get; set; }

        // Represents the collection of projects
        public ObservableCollection<Project> Projects
        {
            get
            {
                // If the client is null or has an id of 0, return all projects
                if (Client == null || Client.Id == 0)
                {
                    // Filter and map the projects based on the project query
                    return new ObservableCollection<Project>(ProjectService.Current.Projects
                        .Where(p => p.Name.Contains(ProjectQuery ?? string.Empty)));
                }
                // Otherwise, return projects filtered by client id and project query
                return new ObservableCollection<Project>(ProjectService.Current.Projects
                    .Where(p => p.ClientId == Client.Id && p.Name.Contains(ProjectQuery ?? string.Empty)));
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
        public ProjectViewViewModel()
        {
            SearchProjectCommand = new Command(ExecuteSearchProjectCommand);
        }

        // Executes the search command 
        public void ExecuteSearchProjectCommand()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        // Refreshes the project list
        public void RefreshProjectList()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        // Deletes the selected project
        public void Delete()
        {
            if (SelectedProject != null)
            {
                // Delete the project from the service
                ProjectService.Current.Delete(SelectedProject.Id);

                // Clear the selected project
                SelectedProject = null;

                // Notify that the Projects collection and SelectedProject have changed
                NotifyPropertyChanged(nameof(Projects));
                NotifyPropertyChanged(nameof(SelectedProject));
            }
        }

    }
}
