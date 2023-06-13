using PracticePanther.Library.Models;
using PracticePanther.Library.Services; 

namespace PracticePanther.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Client> clients = new List<Client>();
            List<Project> projects = new List<Project>();

            Console.WriteLine("Client Name: ");
            var name = Console.ReadLine() ?? string.Empty;

            var client = new Client
            {
                Name = name,
                Id = 1
            };

            Console.WriteLine("Project Name: ");
            name = Console.ReadLine() ?? string.Empty;

            var proj = new Project
            {
                LongName = name,
                Id = 1,
                ClientId = client.Id,
            };

            clients.Add(client);
            projects.Add(proj);

        }
    }
}