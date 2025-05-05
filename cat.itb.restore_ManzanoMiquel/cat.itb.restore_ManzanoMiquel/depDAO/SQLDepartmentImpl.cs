using System;
using System.Collections.Generic;
using cat.itb.gestioHR.connections;
using Npgsql;

namespace cat.itb.gestioHR.depDAO
{
    public class SQLDepartmentImpl : DepartmentDAO
    {
        
        private NpgsqlConnection conn;

        public void DeleteAll()
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();
            
            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM departments", conn);

            try
            {
                cmd.ExecuteNonQuery();
              
                Console.WriteLine("Departments deleted");
            }
            catch
            {
                Console.WriteLine("Couldn't delete Departments");
                
            }

            conn.Close();
         
        }
        
        public void InsertAll(List<Department> deps)
        {
            DeleteAll();
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();
            
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO departments VALUES (@prodNum, @descripcio)", conn);
            
            foreach (var dep in deps)
            {
                cmd.Parameters.AddWithValue("depno", dep._id);
                cmd.Parameters.AddWithValue("nom", dep.Name);
                cmd.Parameters.AddWithValue("loc", dep.Loc);
                cmd.Prepare();
                try
                {
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Department with Id {0} and Name {1} added",
                        dep._id, dep.Name);
                }
                catch
                {
                    Console.WriteLine("Couldn't add Department with Id {0}", dep._id);
                }
                
                cmd.Parameters.Clear();
            }
            
            conn.Close();
        }
        
        public List<Department> SelectAll()
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            var cmd = new NpgsqlCommand("SELECT * FROM departments", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<Department> deps = new List<Department>(); 
            
            while (dr.Read())
            {
                Department dep = new Department();
                dep._id = dr.GetInt32(0);
                dep.Name = dr.GetString(1);
                dep.Loc = dr.GetString(2);
                deps.Add(dep);
            }

            conn.Close();
            return deps;
        }
        
        public Department Select(int depId)
        {
       
           SQLConnection db = new SQLConnection();
           conn = db.GetConnection();
           
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM departments WHERE _id =" + depId, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            Department dep = new Department();
            
            if (dr.Read())
            {
                dep._id = dr.GetInt32(0);
                dep.Name = dr.GetString(1);
                dep.Loc = dr.GetString(2);
            }
            else
            {
                dep = null;
               
            }
            conn.Close();
            return dep;
            
        }

        public Boolean Insert(Department dep)
        {
   
           SQLConnection db = new SQLConnection();
           conn = db.GetConnection();
           
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO departments VALUES (@depno, @nom, @loc)", conn);

            Boolean bol; 
            cmd.Parameters.AddWithValue("depno", dep._id);
            cmd.Parameters.AddWithValue("nom", dep.Name);
            cmd.Parameters.AddWithValue("loc", dep.Loc);
            cmd.Prepare();
            try
            {
                cmd.ExecuteNonQuery();
                bol = true;
                Console.WriteLine("Department with Id {0} and Name {1} added",
                    dep._id, dep.Name);
            }
            catch
            {
                bol = false;
                Console.WriteLine("Couldn't add Department with Id {0}", dep._id);
            }
           
            conn.Close();
            return bol;

        }

        public Boolean Delete(int depId)
        {
          
           SQLConnection db = new SQLConnection();
           conn = db.GetConnection();
            Boolean bol; 
            
            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM departments WHERE _id =" +depId, conn);

            try
            {
                 cmd.ExecuteNonQuery();
                 bol = true;
                 Console.WriteLine("Department with Id {0} deleted",
                    depId);
            }
            catch
            {
                Console.WriteLine("Couldn't delete Department with Id {0}",depId);
                bol = false;
            }

            conn.Close();
            return bol;
        }

        public Boolean Update(Department dep)
        {
            SQLConnection db = new SQLConnection();
           conn = db.GetConnection();
            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE departments SET name = @nom, loc = @loc  WHERE _id = @depId", conn);
            Boolean bol; 
          
            cmd.Parameters.AddWithValue("nom", dep.Name);
            cmd.Parameters.AddWithValue("loc", dep.Loc);
            cmd.Parameters.AddWithValue("depId", dep._id);
            cmd.Prepare();
            try
            {
                cmd.ExecuteNonQuery();
                bol = true;
                Console.WriteLine("Department with ID {0} updated", dep._id);
            }
            catch
            {
                bol = false;
                Console.WriteLine("Couldn't update Department {0}", dep.Name);
            }
            
            
            conn.Close();
            return bol;
        }
        
    }
}