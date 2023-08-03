using PracticePanther.Library.Models;
using System;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace PracticePanther.CLI
{
    internal class Program
    {
        Main method that begins the execution of program
        static void Main(string[] args)
        {
            // Create a list of clients 
            var clients = new List<Client>();

            // Create a list of projects
            var projects = new List<Project>();

            Console.WriteLine("Welcome to Practice Panther CLI");

            // Loop until user quits
            while (true)
            {
                {
                    Console.WriteLine("*******************************");

                    // Display menu 
                    Console.WriteLine("Main Menu");
                    Console.WriteLine("1) Client Menu");
                    Console.WriteLine("2) Project Menu");
                    Console.WriteLine("3) Quit");

                    // Prompt user for menu choice
                    Console.Write("Enter choice: ");
                    var userChoice = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("*******************************");

                    // If user chooses client menu
                    if (userChoice.Equals(1))
                    {
                        // Call function
                        clientMenu(clients);
                    }
                    else if (userChoice.Equals(2))
                    {
                        // Call function
                        projectMenu(clients, projects);
                    }
                    else if (userChoice.Equals(3))
                    {
                        Console.WriteLine("Exiting program...");
                        break;
                    }
                }

            }
        }

        // Method that executes client menu
        static void clientMenu(List<Client> clients)
        {
            // Display menu 
            Console.WriteLine("Client Menu");
            Console.WriteLine("C) Create a client");
            Console.WriteLine("R) Read list of clients");
            Console.WriteLine("U) Update a client");
            Console.WriteLine("D) Delete a client");

            // Prompt user for choice
            Console.Write("Enter choice: ");
            var choice = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("-------------------------------");

            // If user chooses to create a client
            if (choice.Equals("C", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Creating a client...");

                // Prompt user for client id
                Console.Write("Id: ");
                var id = int.Parse(Console.ReadLine() ?? "0");

                // Prompt user for client name 
                Console.Write("Name: ");
                var name = Console.ReadLine() ?? "John/Jane Doe";

                // Prompt user for open date
                Console.Write("Open date (yyyy-MM-dd) : ");
                var openDate = DateTime.Parse(Console.ReadLine() ?? "2023-1-1");

                // Prompt user for notes
                Console.Write("Notes: ");
                var notes = Console.ReadLine() ?? string.Empty;

                // Add client to list 
                clients.Add(new Client
                {
                    Id = id,
                    Name = name,
                    OpenDate = openDate,
                    Notes = notes,
                    IsActive = true
                }
                );

                Console.WriteLine("Client is active and successfully created...");

            }
            // If user chooses to read list of clients
            else if (choice.Equals("R", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Reading list of clients...");

                // If list is empty
                if (clients.Count == 0)
                {
                    Console.WriteLine("Client list is empty");
                }

                // Loop through list
                foreach (var client in clients)
                {
                    // Display each client 
                    Console.WriteLine(client);
                }

                Console.WriteLine("End of list...");
            }
            // If user chooses to update client
            else if (choice.Equals("U", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Updating a client...");

                // Prompt user what client they want to update
                Console.Write("Client id to be updated: ");
                var updateChoice = int.Parse(Console.ReadLine() ?? "0");

                // Retrieve client with given id
                var clientToUpdate = clients.FirstOrDefault(s => s.Id == updateChoice);

                // If correct user input
                if (clientToUpdate != null)
                {
                    // Display menu
                    Console.WriteLine("What do you want to update?");
                    Console.WriteLine("(1) Id     (2) Name");
                    Console.WriteLine("(3) Notes  (4) Status");
                    Console.Write("Enter choice: ");

                    // Prompt user for choice
                    var updatePropertyChoice = int.Parse(Console.ReadLine() ?? string.Empty);

                    // If user chooses to update id
                    if (updatePropertyChoice.Equals(1))
                    {
                        // Prompt user for new id
                        Console.Write("New id: ");
                        clientToUpdate.Id = int.Parse(Console.ReadLine() ?? "0");

                        Console.WriteLine("Client id successfully updated...");
                    }
                    // If user chooses to update name
                    else if (updatePropertyChoice.Equals(2))
                    {
                        // Prompt user for new name
                        Console.Write("New name: ");
                        clientToUpdate.Name = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Client name successfully updated...");
                    }
                    // If user chooses to update notes
                    else if (updatePropertyChoice.Equals(3))
                    {
                        // Prompt user for new notes
                        Console.Write("New notes: ");
                        clientToUpdate.Name = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Client name successfully updated...");
                    }
                    // If user chooses to update status
                    else if (updatePropertyChoice.Equals(4))
                    {
                        // Set status to false
                        clientToUpdate.IsActive = false;

                        Console.WriteLine("Client is not active anymore...");
                    }

                }
                // If incorrect user input
                else
                {
                    Console.WriteLine("Client could not be updated...");
                }


            }
            // If user chooses to delete a client
            else if (choice.Equals("D", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Deleting a client...");

                // Print client list
                Console.WriteLine("Client list: ");
                clients.ForEach(Console.WriteLine);

                // Prompt user for id
                Console.Write("Client id to be deleted: ");
                var deleteChoice = int.Parse(Console.ReadLine() ?? "0");

                // Retrieve client with given id
                var clientToRemove = clients.FirstOrDefault(s => s.Id == deleteChoice);

                // If correct user input
                if (clientToRemove != null)
                {
                    // Remove from list of clients
                    clients.Remove(clientToRemove);

                    Console.WriteLine("Client deleted...");
                }
                // If incorrect user input
                else
                {
                    Console.WriteLine("Client could not be deleted...");
                }
            }
        }
        // Method that executes project menu
        static void projectMenu(List<Client> clients, List<Project> projects)
        {
            // Display menu 
            Console.WriteLine("Project Menu");
            Console.WriteLine("C) Create a project");
            Console.WriteLine("R) Read list of projects");
            Console.WriteLine("U) Update a project");
            Console.WriteLine("D) Delete a project");

            // Prompt user for choice
            Console.Write("Enter choice: ");
            var choice = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("-------------------------------");

            // If user chooses to create a project
            if (choice.Equals("C", StringComparison.InvariantCultureIgnoreCase))
            {
                // Prompt user for project id
                Console.Write("Id: ");
                var projectId = int.Parse(Console.ReadLine() ?? "0");

                // Prompt user for open date
                Console.Write("Open date (yyyy-MM-dd) : ");
                var openDate = DateTime.Parse(Console.ReadLine() ?? "2023-1-1");

                // Prompt user for client short name 
                Console.Write("Project short name: ");
                var shortName = Console.ReadLine() ?? string.Empty;

                // Prompt user for client long name 
                Console.Write("Project long name: ");
                var longName = Console.ReadLine() ?? string.Empty;

                // Prompt user for client id
                Console.Write("Client id: ");
                var clientId = int.Parse(Console.ReadLine() ?? "0");

                // Retrieve client from given id
                Client? client = clients.Find(c => c.Id == clientId);

                // Add project of list
                projects.Add(new Project
                {
                    Id = projectId,
                    ShortName = shortName,
                    LongName = longName,
                    IsActive = true,
                    OpenDate = DateTime.Now,
                    ClientId = clientId,
                    Client = client
                }
                );

                Console.WriteLine("Client is active and successfully created...");


            }
            // If user chooses to read list of projects
            else if (choice.Equals("R", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Reading list of projects...");

                // If list is empty
                if (clients.Count == 0)
                {
                    Console.WriteLine("Project list is empty");
                }

                // Loop through list
                foreach (var project in projects)
                {
                    // Display each project
                    Console.WriteLine(project);
                }

            }
            // If user chooses to update project
            else if (choice.Equals("U", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Updating a project...");

                // Prompt user what project they want to update
                Console.Write("Project id to be updated: ");
                var updateChoice = int.Parse(Console.ReadLine() ?? "0");

                // Retrieve project with given id
                var projectToUpdate = projects.FirstOrDefault(s => s.Id == updateChoice);

                // If correct user input
                if (projectToUpdate != null)
                {
                    // Display menu
                    Console.WriteLine("What do you want to update?");
                    Console.WriteLine("(1) Id         (2) Short Name");
                    Console.WriteLine("(3) Long Name  (4) Client id");
                    Console.WriteLine("(5) Status");
                    Console.Write("Enter choice: ");

                    // Prompt user for choice
                    var updatePropertyChoice = int.Parse(Console.ReadLine() ?? "0");

                    // If user chooses to update id 
                    if (updatePropertyChoice.Equals(1))
                    {
                        // Prompt user for new id
                        Console.Write("New project id: ");
                        projectToUpdate.Id = int.Parse(Console.ReadLine() ?? "0");

                        Console.WriteLine("Project id successfully updated...");
                    }
                    // If user chooses to update short name 
                    else if (updatePropertyChoice.Equals(2))
                    {
                        // Prompt user for new short name
                        Console.Write("New short name: ");
                        projectToUpdate.ShortName = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Project short name successfully updated...");
                    }
                    // If user chooses to update long name
                    else if (updatePropertyChoice.Equals(3))
                    {
                        // Prompt user for new long name 
                        Console.Write("New long name: ");
                        projectToUpdate.LongName = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Project long name successfully updated...");
                    }
                    // If user chooses to update client id
                    else if (updatePropertyChoice.Equals(4))
                    {
                        // Prompt user for new client id
                        Console.Write("New client id: ");
                        projectToUpdate.ClientId = int.Parse(Console.ReadLine() ?? "0");

                        // Retrieve client from from given id 
                        Client? client = clients.Find(c => c.Id == projectToUpdate.ClientId);

                        // Set cleint
                        projectToUpdate.Client = client;

                        Console.WriteLine("New client id successfully updated...");

                    }
                    // If user chooses to update status
                    else if (updatePropertyChoice.Equals(5))
                    {
                        // Set statust to false 
                        projectToUpdate.IsActive = false;

                        Console.WriteLine("Project is not active anymore...");
                    }
                }
                // If client does not exist
                else
                {
                    Console.WriteLine("Project could not be updated...");
                }
            }
            // If user choses to delete a project
            else if (choice.Equals("D", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Deleting a project...");

                // Print project list
                Console.WriteLine("Project list");
                projects.ForEach(Console.WriteLine);

                // Prompt user for id 
                Console.Write("Project id to be deleted: ");
                var deleteChoice = int.Parse(Console.ReadLine() ?? "0");

                // Find project with given id
                var projectToRemove = projects.FirstOrDefault(s => s.Id == deleteChoice);

                // If correct user input
                if (projectToRemove != null)
                {
                    // Remove project 
                    projects.Remove(projectToRemove);

                    Console.WriteLine("Project successfully deleted...");
                }
                // If incorrect user input
                else
                {
                    Console.WriteLine("Project could not be deleted...");
                }
            }
        }
    }
}