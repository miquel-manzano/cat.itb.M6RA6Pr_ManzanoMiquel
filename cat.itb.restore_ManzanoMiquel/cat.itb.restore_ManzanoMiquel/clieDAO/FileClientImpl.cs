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
        public void DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public void InsertAll(List<Client> clies)
        {
            FileInfo file = new FileInfo("../../../../files/clients.json");
            StreamWriter sw = file.CreateText();
            try
            {
                foreach (var clie in clies)
                    sw.WriteLine(JsonConvert.SerializeObject(clie));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nSuccesful inserts in file departments.json");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInserts in departments.json couldn't be executed");
            }
            sw.Close();
            Console.ResetColor();
        }

        public List<Client> SelectAll()
        {
            FileInfo file = new FileInfo("../../../../files/clients.json");
            StreamReader sr = file.OpenText();
            string clies;
            List<Client> list = new List<Client>();
            while ((clies = sr.ReadLine()) != null)
                list.Add(JsonConvert.DeserializeObject<Client>(clies));
            sr.Close();

            return list;
        }

        public Client Select(int clieId)
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(Client clie)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int clieId)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Client clie)
        {
            throw new System.NotImplementedException();
        }
    }
}
