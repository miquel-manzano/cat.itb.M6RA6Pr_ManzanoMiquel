using cat.itb.gestioHR.depDAO;
using cat.itb.restore_ManzanoMiquel.connections;
using MongoDB.Driver;

namespace cat.itb.restore_ManzanoMiquel.clieDAO
{
    public class MongoClientImpl : ClientDAO
    {
        public void DeleteAll()
        {
            var database = MongoConnection.GetDatabase("itb");
            database.DropCollection("clients");

        }

        public void InsertAll(List<Client> clies)
        {

            DeleteAll();
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>("clients");

            try
            {
                collection.InsertMany(clies);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nCollection clients inserted");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Collection couldn't be inserted");
            }
            Console.ResetColor();
        }

        public List<Client> SelectAll()
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>("clients");

            var clies = collection.AsQueryable<Client>().ToList();

            return clies;
        }


        public Client Select(int clieId)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>("clients");

            var clie = collection.AsQueryable<Client>()
                    .Where(d => d._id == clieId)
                    .Single();
            return clie;
        }

        public Boolean Insert(Client clie)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>("clients");

            Boolean bol;
            try
            {
                collection.InsertOne(clie);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nClients inserted");
                bol = true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Collection couldn't be inserted");
                bol = false;
            }
            Console.ResetColor();

            return bol;
        }


        public Boolean Delete(int clieId)
        {
            Boolean bol;
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>("clients");

            var deleteFilter = Builders<Client>.Filter.Eq("_id", clieId);

            var depDeleted = collection.DeleteOne(deleteFilter);
            Console.WriteLine("Client deleted: " + clieId);
            var num = depDeleted.DeletedCount;
            if (depDeleted.DeletedCount != 0)
            {
                bol = true;
            }
            else
            {
                bol = false;
            }

            return bol;
        }

        public Boolean Update(Client clie)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Department>("clients");

            Delete(clie._id);
            Console.WriteLine("Client updated: " + clie._id);
            return Insert(clie);
        }
    }
}
