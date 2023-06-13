using System;
using System.Collections.Generic;
using PracticePanther.Library.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Library.Services
{
    public class ClientService
    {
        private static ClientService ? instance; 
        private static object _lock = new object();
        public static ClientService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ClientService();
                    }
                    return new ClientService();
                }
            }
        }
        private List<Client> clients; 
        private ClientService()
        {
            clients = new List<Client>();
        }

        public Client ? Get(int id)
        {
            return clients.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Client ? client)
        {
            if (clients != null)
            {
                clients.Add(client); 
            }
        }

        public void Delete(int id)
        {
            var clientToRemove = Get(id);
            if (clientToRemove != null)
            {
                clients.Remove(clientToRemove);
            }
        }
        public void Read()
        {
            clients.ForEach(Console.WriteLine);
        }
    }
}
