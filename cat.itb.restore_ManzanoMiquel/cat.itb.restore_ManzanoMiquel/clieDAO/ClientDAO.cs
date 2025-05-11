
namespace cat.itb.restore_ManzanoMiquel.clieDAO
{
    public interface ClientDAO
    {
        void DeleteAll();
        void InsertAll(List<Client> clients);
        List<Client> SelectAll();
        Client Select(int clientId);
        bool Insert(Client client);
        bool Delete(int clientId);
        bool Update(Client client);
        List<Client> SelectByEmpId(int empId);
        List<Client> SelectByEmpSurname(string surname);
    }
}
