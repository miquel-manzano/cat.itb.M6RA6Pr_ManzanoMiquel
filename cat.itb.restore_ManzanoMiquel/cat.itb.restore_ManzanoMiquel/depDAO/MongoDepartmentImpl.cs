using System;
using System.Collections.Generic;
using cat.itb.gestioHR.connections;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace cat.itb.gestioHR.depDAO
{
   public class MongoDepartmentImpl : DepartmentDAO
   {
       
       public void DeleteAll()
       {
           var database = MongoConnection.GetDatabase("itb");
           database.DropCollection("departments");
           
       }
       
        public void InsertAll( List<Department> deps)
        {
       
            DeleteAll();
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Department>("departments");

            try
            {
               collection.InsertMany(deps);
               Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nCollection departments inserted");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Collection couldn't be inserted");
            }
            Console.ResetColor();
        }

        public List<Department> SelectAll()
        {
            var database = MongoConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Department>("departments");

            var deps = booksCollection.AsQueryable<Department>().ToList();

            return deps;
        }


        public Department Select(int depId)
        {
            var database = MongoConnection.GetDatabase("itb");
                var collection = database.GetCollection<Department>("departments");

                var dep =  collection.AsQueryable<Department>()
                        .Where(d => d._id == depId)
                        .Single();
            return dep;
        }

        public Boolean Insert(Department dep)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Department>("departments");

            Boolean bol;
            try
            {
                collection.InsertOne(dep);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nDepartments inserted");
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
        
      
        public Boolean Delete(int depId)
        {
            Boolean bol;
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Department>("departments");

            var deleteFilter = Builders<Department>.Filter.Eq("_id", depId);
            
            var depDeleted = collection.DeleteOne(deleteFilter);
            Console.WriteLine("Department deleted: " + depId);
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

        public Boolean Update(Department dep)
        {
            var database = MongoConnection.GetDatabase("itb");
            var collection = database.GetCollection<Department>("departments");

            Delete(dep._id);
            Console.WriteLine("Departament updated: " + dep._id);
            return Insert(dep);
        }

   }
}