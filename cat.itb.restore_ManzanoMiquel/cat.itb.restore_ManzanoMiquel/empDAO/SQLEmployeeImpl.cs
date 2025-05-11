using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.restore_ManzanoMiquel.connections;
using Npgsql;

namespace cat.itb.restore_ManzanoMiquel.empDAO
{
    public class SQLEmployeeImpl : EmployeeDAO
    {
        public void DeleteAll()
        {
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand("DELETE FROM employees", conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertAll(List<Employee> emps)
        {
            DeleteAll();
            using (var conn = new SQLConnection().GetConnection())
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    foreach (var emp in emps)
                    {
                        var cmd = new NpgsqlCommand(
                            "INSERT INTO employees VALUES (@id, @surname, @job, @managerid, @startdate, @salary, @commission, @depid)", conn);

                        cmd.Parameters.AddWithValue("id", emp._id);
                        cmd.Parameters.AddWithValue("surname", emp.Surname);
                        cmd.Parameters.AddWithValue("job", emp.Job);
                        cmd.Parameters.AddWithValue("managerid", emp.Managerid);
                        cmd.Parameters.AddWithValue("startdate", emp.Startdate);
                        cmd.Parameters.AddWithValue("salary", emp.Salary);
                        cmd.Parameters.AddWithValue("commission", emp.Commission);
                        cmd.Parameters.AddWithValue("depid", emp.Depid);

                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error en InsertAll: {ex.Message}");
                    throw;
                }
            }
        }

        public List<Employee> SelectAll()
        {
            var employees = new List<Employee>();
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand("SELECT * FROM employees", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            _id = reader.GetInt32(0),
                            Surname = reader.GetString(1),
                            Job = reader.GetString(2),
                            Managerid = reader.GetInt32(3),
                            Startdate = reader.GetDateTime(4),
                            Salary = reader.GetDecimal(5),
                            Commission = reader.GetDecimal(6),
                            Depid = reader.GetInt32(7)
                        });
                    }
                }
            }
            return employees;
        }

        public Employee Select(int empId)
        {
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand("SELECT * FROM employees WHERE _id = @id", conn);
                cmd.Parameters.AddWithValue("id", empId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Employee
                        {
                            _id = reader.GetInt32(0),
                            Surname = reader.GetString(1),
                            Job = reader.GetString(2),
                            Managerid = reader.GetInt32(3),
                            Startdate = reader.GetDateTime(4),
                            Salary = reader.GetDecimal(5),
                            Commission = reader.GetDecimal(6),
                            Depid = reader.GetInt32(7)
                        };
                    }
                }
                return null;
            }
        }

        public bool Insert(Employee emp)
        {
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand(
                    "INSERT INTO employees VALUES (@id, @surname, @job, @managerid, @startdate, @salary, @commission, @depid)", conn);

                cmd.Parameters.AddWithValue("id", emp._id);
                cmd.Parameters.AddWithValue("surname", emp.Surname);
                cmd.Parameters.AddWithValue("job", emp.Job);
                cmd.Parameters.AddWithValue("managerid", emp.Managerid);
                cmd.Parameters.AddWithValue("startdate", emp.Startdate);
                cmd.Parameters.AddWithValue("salary", emp.Salary);
                cmd.Parameters.AddWithValue("commission", emp.Commission);
                cmd.Parameters.AddWithValue("depid", emp.Depid);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (PostgresException ex) when (ex.SqlState == "23505") // Violación de clave única
                {
                    Console.WriteLine($"Error: Ya existe un empleado con ID {emp._id}");
                    return false;
                }
            }
        }

        public bool Delete(int empId)
        {
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand("DELETE FROM employees WHERE _id = @id", conn);
                cmd.Parameters.AddWithValue("id", empId);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (PostgresException ex)
                {
                    Console.WriteLine($"Error al eliminar empleado: {ex.Message}");
                    return false;
                }
            }
        }

        public bool Update(Employee emp)
        {
            using (var conn = new SQLConnection().GetConnection())
            {
                var cmd = new NpgsqlCommand(
                    "UPDATE employees SET surname = @surname, job = @job, managerid = @managerid, " +
                    "startdate = @startdate, salary = @salary, commission = @commission, depid = @depid " +
                    "WHERE _id = @id", conn);

                cmd.Parameters.AddWithValue("surname", emp.Surname);
                cmd.Parameters.AddWithValue("job", emp.Job);
                cmd.Parameters.AddWithValue("managerid", emp.Managerid);
                cmd.Parameters.AddWithValue("startdate", emp.Startdate);
                cmd.Parameters.AddWithValue("salary", emp.Salary);
                cmd.Parameters.AddWithValue("commission", emp.Commission);
                cmd.Parameters.AddWithValue("depid", emp.Depid);
                cmd.Parameters.AddWithValue("id", emp._id);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (PostgresException ex)
                {
                    Console.WriteLine($"Error al actualizar empleado: {ex.Message}");
                    return false;
                }
            }
        }
    }
}