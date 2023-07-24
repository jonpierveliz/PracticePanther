using PracticePanther.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Library.Services
{
    public class ProjectService
    {
        private List<Project> projects;

        // List of projects
        public List<Project> Projects
        {
            get
            {
                return projects;
            }
        }

        private static ProjectService? instance;

        // Singleton instance of ProjectService
        public static ProjectService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectService();
                }

                return instance;
            }
        }

        private ProjectService()
        {
            // Initializes the list of projects
            projects = new List<Project>
    {
        new Project { Id = 1, Name = "Project 1", ClientId = 1 },
        new Project { Id = 2, Name = "Project 2", ClientId = 1 },
        new Project { Id = 3, Name = "Project 3", ClientId = 3 },
        new Project { Id = 4, Name = "Project 4", ClientId = 4 },
        new Project { Id = 5, Name = "Project 5", ClientId = 5 }
    };
        }


        // Returns a project by id
        public Project? Get(int id)
        {
            return Projects.FirstOrDefault(p => p.Id == id);
        }

        // Deletes a client by id
        public void Delete(int id)
        {
            var projectToRemove = Projects.FirstOrDefault(x => x.Id == id);
            if (projectToRemove != null)
            {
                Projects.Remove(projectToRemove);
            }
        }

        // Adds or updates a project
        public void AddOrUpdate(Project project)
        {
            if (project.Id == 0)
            {
                // If the id is 0, it's a new project, so assign a new id
                project.Id = LastId + 1;
                projects.Add(project);
            }
            else
            {
                // It's an existing project, find it in the list and update its properties
                var existingProject = projects.FirstOrDefault(p => p.Id == project.Id);
                if (existingProject != null)
                {
                    existingProject.Name = project.Name;
                    // Update other properties as needed
                }
            }
        }


        // Gets the last id in the list of projects
        private int LastId
        {
            get
            {
                // If the list is not empty, return the maximum id, otherwise return 0
                return Projects.Any() ? Projects.Select(p => p.Id).Max() : 0;
            }
        }

        // Searches for clients matching the provided query
        public IEnumerable<Project> Search(string query)
        {
            return Projects
                .Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()));
        }
    }
}
