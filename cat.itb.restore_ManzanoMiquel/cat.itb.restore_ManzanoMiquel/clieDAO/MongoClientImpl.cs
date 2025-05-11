using cat.itb.gestioHR.depDAO;
using cat.itb.restore_ManzanoMiquel.connections;
using cat.itb.restore_ManzanoMiquel.empDAO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cat.itb.restore_ManzanoMiquel.clieDAO
{
    public class MongoClientImpl : ClientDAO
    {
        private const string CollectionName = "clients";

        public void DeleteAll()
        {
            var database = MongoConnection.GetDatabase("itb");
            database.DropCollection(CollectionName);
        }

        public void InsertAll(List<Client> clients)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>(CollectionName);
            collection.InsertMany(clients);
        }

        public List<Client> SelectAll()
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>(CollectionName);
            return collection.Find(_ => true).ToList();
        }

        public Client Select(int clientId)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>(CollectionName);
            return collection.Find(c => c._id == clientId).FirstOrDefault();
        }

        public bool Insert(Client client)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>(CollectionName);

            try
            {
                collection.InsertOne(client);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int clientId)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>(CollectionName);
            var result = collection.DeleteOne(c => c._id == clientId);
            return result.DeletedCount > 0;
        }

        public bool Update(Client client)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>(CollectionName);

            var filter = Builders<Client>.Filter.Eq(c => c._id, client._id);
            var update = Builders<Client>.Update
                .Set(c => c.Name, client.Name)
                .Set(c => c.Address, client.Address)
                .Set(c => c.City, client.City)
                .Set(c => c.St, client.St)
                .Set(c => c.Zipcode, client.Zipcode)
                .Set(c => c.Area, client.Area)
                .Set(c => c.Phone, client.Phone)
                .Set(c => c.Empid, client.Empid)
                .Set(c => c.Credit, client.Credit)
                .Set(c => c.Comments, client.Comments);

            var result = collection.UpdateOne(filter, update);
            return result.ModifiedCount > 0;
        }

        public List<Client> SelectByEmpId(int empId)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Client>(CollectionName);
            var filter = Builders<Client>.Filter.Eq("Empid", empId);
            return collection.Find(filter).ToList();
        }

        public List<Client> SelectByEmpSurname(string surname)
        {
            var database = MongoConnection.GetDatabase("itb");
            var clientsCollection = database.GetCollection<Client>(CollectionName);
            var employeesCollection = database.GetCollection<Employee>("employees");

            var pipeline = new[]
            {
                new BsonDocument("$lookup",
                    new BsonDocument
                    {
                        { "from", "employees" },
                        { "localField", "Empid" },
                        { "foreignField", "_id" },
                        { "as", "employee" }
                    }),
                new BsonDocument("$unwind", "$employee"),
                new BsonDocument("$match",
                    new BsonDocument("employee.Surname", surname)),
                new BsonDocument("$project",
                    new BsonDocument
                    {
                        { "_id", 1 },
                        { "Name", 1 },
                        { "Address", 1 },
                        { "City", 1 },
                        { "St", 1 },
                        { "Zipcode", 1 },
                        { "Area", 1 },
                        { "Phone", 1 },
                        { "Empid", 1 },
                        { "Credit", 1 },
                        { "Comments", 1 }
                    })
            };

            return clientsCollection.Aggregate<Client>(pipeline).ToList();
        }
    }
}
