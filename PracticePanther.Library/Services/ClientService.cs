using System;
using System.Collections.Generic;
using PracticePanther.Library.Models;
using PracticePanther.Library.Utilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PracticePanther.Library.DTO;

namespace PracticePanther.Library.Services 
{ 

    public class ClientService
    {
        // List of clients
        private List<ClientDTO> clients = new List<ClientDTO>();
        public List<ClientDTO> Clients
        {
            get
            {
                
                return clients ?? new List<ClientDTO>(); 
            }
        }
       
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
        public ClientDTO ? Get(int id)
        {
            return Clients.FirstOrDefault(c => c.Id == id);
        }

        // Default constructor
        private ClientService()
        {
            var response = new WebRequestHandler()
                .Get("/Client")
                .Result;

           
            clients = JsonConvert.
               DeserializeObject<List<ClientDTO>>(response) ?? new List<ClientDTO>();
        }

        // Deletes a client by id
        public void Delete(int id)
        {
            var response
                = new WebRequestHandler().Delete($"/Delete/{id}").Result;

            var deleteClient = JsonConvert.DeserializeObject<Client>(response) ?? null;

                if (deleteClient != null)
            {
                var clientToRemove = Clients.FirstOrDefault(x => x.Id == id);
                if (clientToRemove != null)
                {
                    Clients.Remove(clientToRemove);
                }
            }
           
        }

       

        // Adds or updates a client
        public void AddOrUpdate(ClientDTO c)
        {
            var response 
                = new WebRequestHandler().Post("/Client", c).Result;
            
            var returnClient = JsonConvert.DeserializeObject<ClientDTO>(response) ??null;
            if(returnClient != null)
            {
                var clientToUpdate = Clients.FirstOrDefault(c => c.Id == returnClient.Id);
                if(clientToUpdate == null)
                {
                    Clients.Add(returnClient);
                } else
                {
                    clientToUpdate = returnClient;
                }
            }

           
        }

        // Searches for clients matching the provided query
        public IEnumerable<ClientDTO> Search(string query)
        {
            return Clients
                .Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()));
        }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the property change event
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
