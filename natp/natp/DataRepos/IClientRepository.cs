using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public interface IClientRepository
    {
        List<Client> getAllClients();
        Client getClient(int id);
        Client getClientByAccount(int id);
        int addClient(Client client);
        bool updateClient(Client client);
    }
}
