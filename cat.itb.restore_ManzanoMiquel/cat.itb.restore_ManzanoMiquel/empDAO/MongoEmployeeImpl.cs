using cat.itb.restore_ManzanoMiquel.connections;
using MongoDB.Driver;

namespace cat.itb.restore_ManzanoMiquel.empDAO
{
    public class MongoEmployeeImpl : EmployeeDAO
    {
        public void DeleteAll()
        {
            var database = MongoConnection.GetDatabase("itb");
            database.DropCollection("employees");

        }

        public void InsertAll(List<Employee> emps)
        {

            DeleteAll();
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Employee>("employees");

            try
            {
                collection.InsertMany(emps);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nCollection employees inserted");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Collection couldn't be inserted");
            }
            Console.ResetColor();
        }

        public List<Employee> SelectAll()
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Employee>("employees");

            var emps = collection.AsQueryable<Employee>().ToList();

            return emps;
        }


        public Employee Select(int empId)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Employee>("employees");

            var emp = collection.AsQueryable<Employee>()
                    .Where(e => e._id == empId)
                    .Single();
            return emp;
        }

        public Boolean Insert(Employee emp)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Employee>("employees");

            Boolean bol;
            try
            {
                collection.InsertOne(emp);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nEmployees inserted");
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


        public Boolean Delete(int empId)
        {
            Boolean bol;
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Employee>("employees");

            var deleteFilter = Builders<Employee>.Filter.Eq("_id", empId);

            var empDeleted = collection.DeleteOne(deleteFilter);
            Console.WriteLine("Employee deleted: " + empId);
            var num = empDeleted.DeletedCount;
            if (empDeleted.DeletedCount != 0)
            {
                bol = true;
            }
            else
            {
                bol = false;
            }

            return bol;
        }

        public Boolean Update(Employee emp)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Employee>("employees");

            Delete(emp._id);
            Console.WriteLine("Departament updated: " + emp._id);
            return Insert(emp);
        }
    }
}
