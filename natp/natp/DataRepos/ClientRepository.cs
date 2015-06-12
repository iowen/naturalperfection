using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace natp.DataRepos
{
    public class ClientRepository : IClientRepository
    {
        private npDataContext db;

        public ClientRepository(npDataContext db)
        {
            this.db = db;
        }

        public List<Client> getAllClients()
        {
            var result = new List<Client>();
            try
            {
                result = db.Clients.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<Client>();
            }
            return result;
        }

        public Client getClient(int id)
        {
            var result = new Client();
            try
            {
                result = db.Clients.First(a => a.ClientId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new Client() { ClientId = -1};
            }
            return result;
        }

        public Client getClientByAccount(int id)
        {
            var result = new Client();
            try
            {
                result = db.Clients.First(a => a.AccountId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new Client() { ClientId = -1 };
            }
            return result;
        }

        public int addClient(Client client)
        {
            try
            {
                this.db.Clients.InsertOnSubmit(client);
                this.db.SubmitChanges();
                return client.ClientId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public bool updateClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}