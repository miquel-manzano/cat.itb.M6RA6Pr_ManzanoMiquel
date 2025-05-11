using cat.itb.gestioHR.depDAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.restore_ManzanoMiquel.clieDAO
{
    public class FileClientImpl : ClientDAO
    {
        private const string FilePath = "../../../../files/clients.json";

        public void DeleteAll()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }

        public void InsertAll(List<Client> clients)
        {
            string json = JsonConvert.SerializeObject(clients, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public List<Client> SelectAll()
        {
            if (!File.Exists(FilePath))
                return new List<Client>();

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<Client>>(json);
        }

        public Client Select(int clientId)
        {
            var clients = SelectAll();
            return clients.Find(c => c._id == clientId);
        }

        public bool Insert(Client client)
        {
            var clients = SelectAll();
            clients.Add(client);
            InsertAll(clients);
            return true;
        }

        public bool Delete(int clientId)
        {
            var clients = SelectAll();
            int removed = clients.RemoveAll(c => c._id == clientId);
            if (removed > 0)
            {
                InsertAll(clients);
                return true;
            }
            return false;
        }

        public bool Update(Client client)
        {
            var clients = SelectAll();
            int index = clients.FindIndex(c => c._id == client._id);
            if (index >= 0)
            {
                clients[index] = client;
                InsertAll(clients);
                return true;
            }
            return false;
        }

        public List<Client> SelectByEmpId(int empId)
        {
            var clients = SelectAll();
            return clients.FindAll(c => c.Empid == empId);
        }

        public List<Client> SelectByEmpSurname(string surname)
        {
            throw new System.NotImplementedException("Esta funcionalidad no está implementada para archivos");
        }
    }
}
