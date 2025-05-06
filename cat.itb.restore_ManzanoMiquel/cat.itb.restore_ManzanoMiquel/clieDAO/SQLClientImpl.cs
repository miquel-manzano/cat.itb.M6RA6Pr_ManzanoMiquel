using cat.itb.gestioHR.depDAO;
using cat.itb.restore_ManzanoMiquel.connections;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.restore_ManzanoMiquel.clieDAO
{
    public class SQLClientImpl : ClientDAO
    {
        private NpgsqlConnection conn;

        public void DeleteAll()
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM clients", conn);

            try
            {
                cmd.ExecuteNonQuery();

                Console.WriteLine("Clients deleted");
            }
            catch
            {
                Console.WriteLine("Couldn't delete Clients");

            }

            conn.Close();

        }

        public void InsertAll(List<Client> clies)
        {
            DeleteAll();
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO clients VALUES (@_id, @Name, @Address, @City, @St, @Zipcode, @Area, @Phone, @Empid, @Credit, @Comments)", conn);

            foreach (var clie in clies)
            {
                cmd.Parameters.AddWithValue("depno", clie._id);
                cmd.Parameters.AddWithValue("nom", clie.Name);
                cmd.Parameters.AddWithValue("loc", clie.Address);
                cmd.Parameters.AddWithValue("loc", clie.City);
                cmd.Parameters.AddWithValue("loc", clie.St);
                cmd.Parameters.AddWithValue("loc", clie.Zipcode);
                cmd.Parameters.AddWithValue("loc", clie.Area);
                cmd.Parameters.AddWithValue("loc", clie.Phone);
                cmd.Parameters.AddWithValue("loc", clie.Empid);
                cmd.Parameters.AddWithValue("loc", clie.Credit);
                cmd.Parameters.AddWithValue("loc", clie.Comments);
                cmd.Prepare();
                try
                {
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Client with Id {0} and Name {1} added",
                        clie._id, clie.Name);
                }
                catch
                {
                    Console.WriteLine("Couldn't add Client with Id {0}", clie._id);
                }

                cmd.Parameters.Clear();
            }

            conn.Close();
        }

        public List<Client> SelectAll()
        {
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            var cmd = new NpgsqlCommand("SELECT * FROM clients", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<Client> clies = new List<Client>();

            while (dr.Read())
            {
                Client clie = new Client();
                clie._id = dr.GetInt32(0);
                clie.Name = dr.GetString(1);
                clie.Address = dr.GetString(2);
                clie.City = dr.GetString(3);
                clie.St = dr.GetString(4);
                clie.Zipcode = dr.GetString(5);
                clie.Area = dr.GetInt32(6);
                clie.Phone = dr.GetString(7);
                clie.Empid = dr.GetInt32(8);
                clie.Credit = dr.GetDecimal(9);
                clie.Comments = dr.GetString(10);

                clies.Add(clie);
            }

            conn.Close();
            return clies;
        }

        public Client Select(int clieId)
        {

            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM clients WHERE _id =" + clieId, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            Client clie = new Client();

            if (dr.Read())
            {
                clie._id = dr.GetInt32(0);
                clie.Name = dr.GetString(1);
                clie.Address = dr.GetString(2);
                clie.City = dr.GetString(3);
                clie.St = dr.GetString(4);
                clie.Zipcode = dr.GetString(5);
                clie.Area = dr.GetInt32(6);
                clie.Phone = dr.GetString(7);
                clie.Empid = dr.GetInt32(8);
                clie.Credit = dr.GetDecimal(9);
                clie.Comments = dr.GetString(10);
            }
            else
            {
                clie = null;

            }
            conn.Close();
            return clie;
        }

        public Boolean Insert(Client clie)
        {

            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO clients VALUES (@_id, @Name, @Address, @City, @St, @Zipcode, @Area, @Phone, @Empid, @Credit, @Comments)", conn);

            Boolean bol;
            cmd.Parameters.AddWithValue("depno", clie._id);
            cmd.Parameters.AddWithValue("nom", clie.Name);
            cmd.Parameters.AddWithValue("loc", clie.Address);
            cmd.Parameters.AddWithValue("loc", clie.City);
            cmd.Parameters.AddWithValue("loc", clie.St);
            cmd.Parameters.AddWithValue("loc", clie.Zipcode);
            cmd.Parameters.AddWithValue("loc", clie.Area);
            cmd.Parameters.AddWithValue("loc", clie.Phone);
            cmd.Parameters.AddWithValue("loc", clie.Empid);
            cmd.Parameters.AddWithValue("loc", clie.Credit);
            cmd.Parameters.AddWithValue("loc", clie.Comments);
            cmd.Prepare();
            try
            {
                cmd.ExecuteNonQuery();
                bol = true;
                Console.WriteLine("Cient with Id {0} and Name {1} added",
                    clie._id, clie.Name);
            }
            catch
            {
                bol = false;
                Console.WriteLine("Couldn't add Client with Id {0}", clie._id);
            }

            conn.Close();
            return bol;
        }

        public Boolean Delete(int clieId)
        {

            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();
            Boolean bol;

            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM clients WHERE _id =" + clieId, conn);

            try
            {
                cmd.ExecuteNonQuery();
                bol = true;
                Console.WriteLine("Client with Id {0} deleted",
                   clieId);
            }
            catch
            {
                Console.WriteLine("Couldn't delete Client with Id {0}", clieId);
                bol = false;
            }

            conn.Close();
            return bol;
        }

        public Boolean Update(Client clie)
        {/*
            SQLConnection db = new SQLConnection();
            conn = db.GetConnection();
            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE clients SET name = @nom, loc = @loc  WHERE _id = @depId", conn);
            Boolean bol;

            cmd.Parameters.AddWithValue("nom", clie.Name);
            cmd.Parameters.AddWithValue("loc", clie.Loc);
            cmd.Parameters.AddWithValue("depId", clie._id);
            cmd.Prepare();
            try
            {
                cmd.ExecuteNonQuery();
                bol = true;
                Console.WriteLine("Department with ID {0} updated", clie._id);
            }
            catch
            {
                bol = false;
                Console.WriteLine("Couldn't update Department {0}", clie.Name);
            }


            conn.Close();
            return bol;*/
            return false;
        }
    }
}
