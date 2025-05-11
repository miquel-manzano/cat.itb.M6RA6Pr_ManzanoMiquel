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
        public void DeleteAll()
        {
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand("DELETE FROM clients", conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertAll(List<Client> clients)
        {
            DeleteAll();
            using (var conn = new SQLConnection().GetConnection())
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    foreach (var client in clients)
                    {
                        var cmd = new NpgsqlCommand(
                            "INSERT INTO clients VALUES (@id, @name, @address, @city, @st, @zipcode, " +
                            "@area, @phone, @empid, @credit, @comments)", conn);

                        cmd.Parameters.AddWithValue("id", client._id);
                        cmd.Parameters.AddWithValue("name", client.Name);
                        cmd.Parameters.AddWithValue("address", client.Address);
                        cmd.Parameters.AddWithValue("city", client.City);
                        cmd.Parameters.AddWithValue("st", client.St);
                        cmd.Parameters.AddWithValue("zipcode", client.Zipcode);
                        cmd.Parameters.AddWithValue("area", client.Area);
                        cmd.Parameters.AddWithValue("phone", client.Phone);
                        cmd.Parameters.AddWithValue("empid", client.Empid);
                        cmd.Parameters.AddWithValue("credit", client.Credit);
                        cmd.Parameters.AddWithValue("comments", (object)client.Comments ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<Client> SelectAll()
        {
            var clients = new List<Client>();
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand("SELECT * FROM clients", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Client
                        {
                            _id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Address = reader.GetString(2),
                            City = reader.GetString(3),
                            St = reader.GetString(4),
                            Zipcode = reader.GetString(5),
                            Area = reader.GetInt32(6),
                            Phone = reader.GetString(7),
                            Empid = reader.GetInt32(8),
                            Credit = reader.GetDecimal(9),
                            Comments = reader.IsDBNull(10) ? null : reader.GetString(10)
                        });
                    }
                }
            }
            return clients;
        }

        public Client Select(int clientId)
        {
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand("SELECT * FROM clients WHERE _id = @id", conn);
                cmd.Parameters.AddWithValue("id", clientId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Client
                        {
                            _id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Address = reader.GetString(2),
                            City = reader.GetString(3),
                            St = reader.GetString(4),
                            Zipcode = reader.GetString(5),
                            Area = reader.GetInt32(6),
                            Phone = reader.GetString(7),
                            Empid = reader.GetInt32(8),
                            Credit = reader.GetDecimal(9),
                            Comments = reader.IsDBNull(10) ? null : reader.GetString(10)
                        };
                    }
                }
            }
            return null;
        }

        public bool Insert(Client client)
        {
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand(
                    "INSERT INTO clients VALUES (@id, @name, @address, @city, @st, @zipcode, " +
                    "@area, @phone, @empid, @credit, @comments)", conn);

                cmd.Parameters.AddWithValue("id", client._id);
                cmd.Parameters.AddWithValue("name", client.Name);
                cmd.Parameters.AddWithValue("address", client.Address);
                cmd.Parameters.AddWithValue("city", client.City);
                cmd.Parameters.AddWithValue("st", client.St);
                cmd.Parameters.AddWithValue("zipcode", client.Zipcode);
                cmd.Parameters.AddWithValue("area", client.Area);
                cmd.Parameters.AddWithValue("phone", client.Phone);
                cmd.Parameters.AddWithValue("empid", client.Empid);
                cmd.Parameters.AddWithValue("credit", client.Credit);
                cmd.Parameters.AddWithValue("comments", (object)client.Comments ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int clientId)
        {
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand("DELETE FROM clients WHERE _id = @id", conn);
                cmd.Parameters.AddWithValue("id", clientId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(Client client)
        {
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand(
                    "UPDATE clients SET name = @name, address = @address, city = @city, " +
                    "st = @st, zipcode = @zipcode, area = @area, phone = @phone, " +
                    "empid = @empid, credit = @credit, comments = @comments " +
                    "WHERE _id = @id", conn);

                cmd.Parameters.AddWithValue("name", client.Name);
                cmd.Parameters.AddWithValue("address", client.Address);
                cmd.Parameters.AddWithValue("city", client.City);
                cmd.Parameters.AddWithValue("st", client.St);
                cmd.Parameters.AddWithValue("zipcode", client.Zipcode);
                cmd.Parameters.AddWithValue("area", client.Area);
                cmd.Parameters.AddWithValue("phone", client.Phone);
                cmd.Parameters.AddWithValue("empid", client.Empid);
                cmd.Parameters.AddWithValue("credit", client.Credit);
                cmd.Parameters.AddWithValue("comments", (object)client.Comments ?? DBNull.Value);
                cmd.Parameters.AddWithValue("id", client._id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Client> SelectByEmpId(int empId)
        {
            var clients = new List<Client>();
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand("SELECT * FROM clients WHERE empid = @empid", conn);
                cmd.Parameters.AddWithValue("empid", empId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Client
                        {
                            _id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Address = reader.GetString(2),
                            City = reader.GetString(3),
                            St = reader.GetString(4),
                            Zipcode = reader.GetString(5),
                            Area = reader.GetInt32(6),
                            Phone = reader.GetString(7),
                            Empid = empId,
                            Credit = reader.GetDecimal(9),
                            Comments = reader.IsDBNull(10) ? null : reader.GetString(10)
                        });
                    }
                }
            }
            return clients;
        }

        public List<Client> SelectByEmpSurname(string surname)
        {
            var clients = new List<Client>();
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand(
                    "SELECT c.* FROM clients c JOIN employees e ON c.empid = e._id " +
                    "WHERE e.surname = @surname", conn);
                cmd.Parameters.AddWithValue("surname", surname);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Client
                        {
                            _id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Address = reader.GetString(2),
                            City = reader.GetString(3),
                            St = reader.GetString(4),
                            Zipcode = reader.GetString(5),
                            Area = reader.GetInt32(6),
                            Phone = reader.GetString(7),
                            Empid = reader.GetInt32(8),
                            Credit = reader.GetDecimal(9),
                            Comments = reader.IsDBNull(10) ? null : reader.GetString(10)
                        });
                    }
                }
            }
            return clients;
        }
    }
}
