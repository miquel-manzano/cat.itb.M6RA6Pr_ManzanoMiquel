
namespace cat.itb.restore_ManzanoMiquel.clieDAO
{
    public interface ClientDAO
    {
        void DeleteAll();
        void InsertAll(List<Client> clies);
        List<Client> SelectAll();
        Client Select(int clieId);
        Boolean Insert(Client clie);
        Boolean Delete(int clieId);
        Boolean Update(Client clie);
    }
}
