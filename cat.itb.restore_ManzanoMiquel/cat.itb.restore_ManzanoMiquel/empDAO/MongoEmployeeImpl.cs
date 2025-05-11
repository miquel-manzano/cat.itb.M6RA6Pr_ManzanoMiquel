using cat.itb.restore_ManzanoMiquel.connections;
using MongoDB.Driver;

namespace cat.itb.restore_ManzanoMiquel.empDAO
{
    public class MongoEmployeeImpl : EmployeeDAO
    {
        private const string CollectionName = "employees";

        public void DeleteAll()
        {
            try
            {
                var database = MongoConnection.GetDatabase("itb");
                database.DropCollection(CollectionName);
                Console.WriteLine("Colección 'employees' eliminada correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar colección: {ex.Message}");
            }
        }

        public void InsertAll(List<Employee> emps)
        {
            if (emps == null || emps.Count == 0)
            {
                Console.WriteLine("Lista de empleados vacía");
                return;
            }

            try
            {
                var database = MongoConnection.GetDatabase("itb");
                var collection = database.GetCollection<Employee>(CollectionName);

                // Opción para ignorar duplicados
                var options = new InsertManyOptions { IsOrdered = false };
                collection.InsertMany(emps, options);

                Console.WriteLine($"{emps.Count} empleados insertados correctamente");
            }
            catch (MongoBulkWriteException<Employee> ex)
            {
                Console.WriteLine($"Algunos empleados no se insertaron (duplicados): {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en InsertAll: {ex.Message}");
            }
        }

        public List<Employee> SelectAll()
        {
            try
            {
                var database = MongoConnection.GetDatabase("itb");
                var collection = database.GetCollection<Employee>(CollectionName);
                return collection.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SelectAll: {ex.Message}");
                return new List<Employee>();
            }
        }

        public Employee Select(int empId)
        {
            try
            {
                var database = MongoConnection.GetDatabase("itb");
                var collection = database.GetCollection<Employee>(CollectionName);
                return collection.Find(e => e._id == empId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Select: {ex.Message}");
                return null;
            }
        }

        public bool Insert(Employee emp)
        {
            if (emp == null) return false;

            try
            {
                var database = MongoConnection.GetDatabase("itb");
                var collection = database.GetCollection<Employee>(CollectionName);

                collection.InsertOne(emp);
                return true;
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                Console.WriteLine($"Error: Ya existe un empleado con ID {emp._id}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Insert: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int empId)
        {
            try
            {
                var database = MongoConnection.GetDatabase("itb");
                var collection = database.GetCollection<Employee>(CollectionName);

                var result = collection.DeleteOne(e => e._id == empId);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Delete: {ex.Message}");
                return false;
            }
        }

        public bool Update(Employee emp)
        {
            if (emp == null) return false;

            try
            {
                var database = MongoConnection.GetDatabase("itb");
                var collection = database.GetCollection<Employee>(CollectionName);

                var filter = Builders<Employee>.Filter.Eq(e => e._id, emp._id);
                var update = Builders<Employee>.Update
                    .Set(e => e.Surname, emp.Surname)
                    .Set(e => e.Job, emp.Job)
                    .Set(e => e.Managerid, emp.Managerid)
                    .Set(e => e.Startdate, emp.Startdate)
                    .Set(e => e.Salary, emp.Salary)
                    .Set(e => e.Commission, emp.Commission)
                    .Set(e => e.Depid, emp.Depid);

                var result = collection.UpdateOne(filter, update);
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Update: {ex.Message}");
                return false;
            }
        }
    }
}
