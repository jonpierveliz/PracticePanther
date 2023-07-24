using System;
using System.Collections.Generic;
using PracticePanther.Library.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PracticePanther.Library.Services
{
    public class ClientService
    {
        // List of clients
        public List<Client> Clients
        {
            get
            {
                return clients;
            }
        }
        private List<Client> clients;

        private static ClientService? instance;

        // Singleton instance of ClientService
        public static ClientService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClientService();
                }
                return instance;
            }
        }

        // Returns a client by id
        public Client? Get(int id)
        {
            return Clients.FirstOrDefault(c => c.Id == id);
        }

        private ClientService()
        {
            // Initializes the list of clients with some default data
            clients = new List<Client>
            {
                new Client{ Id = 1, Name = "Client 1", },
                new Client{ Id = 2, Name = "Client 2"},
                new Client{ Id = 3, Name = "Client 3"},
                new Client{ Id = 4, Name = "Client 4"},
                new Client{ Id = 5, Name = "Client 5"},

            };
        }

        // Deletes a client by id
        public void Delete(int id)
        {
            var clientToRemove = Clients.FirstOrDefault(x => x.Id == id);
            if (clientToRemove != null)
            {
                Clients.Remove(clientToRemove);
            }
        }

        // Gets the last id in the list of clients
        private int LastId
        {
            get
            {
                // If the list is not empty, return the maximum id, otherwise return 0
                return Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;
            }
        }

        // Adds or updates a client
        public void AddOrUpdate(Client c)
        {
            var isAdd = false;
            if (c.Id == 0)
            {
                isAdd = true;
                // If the id is 0, it's a new client, so assign a new id
                c.Id = LastId + 1;
            }
            if (isAdd)
            {
                // Add the client to the list
                Clients.Add(c);
            }
        }

        // Searches for clients matching the provided query
        public IEnumerable<Client> Search(string query)
        {
            return Clients
                .Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()));
        }
    }
}
