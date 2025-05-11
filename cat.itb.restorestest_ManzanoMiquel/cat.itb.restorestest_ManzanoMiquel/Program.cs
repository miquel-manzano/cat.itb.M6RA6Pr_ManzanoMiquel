using cat.itb.gestioHR.depDAO;
using cat.itb.restore_ManzanoMiquel;
using cat.itb.restore_ManzanoMiquel.clieDAO;
using cat.itb.restore_ManzanoMiquel.empDAO;

namespace cat.itb.restorestest_ManzanoMiquel
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Ejercicio 3: Crear employees.json desde PostgreSQL
            var sqlEmpDao = new SQLEmployeeImpl();
            var employees = sqlEmpDao.SelectAll();

            var fileEmpDao = new FileEmployeeImpl();
            fileEmpDao.InsertAll(employees);
            Console.WriteLine("employees.json creado exitosamente");

            // Ejercicio 4: Crear clients.json desde PostgreSQL
            var sqlClientDao = new SQLClientImpl();
            var clients = sqlClientDao.SelectAll();

            var fileClientDao = new FileClientImpl();
            fileClientDao.InsertAll(clients);
            Console.WriteLine("clients.json creado exitosamente");

            // Ejercicio 5: Cargar employees.json a MongoDB
            var mongoEmpDao = new MongoEmployeeImpl();
            mongoEmpDao.InsertAll(fileEmpDao.SelectAll());
            Console.WriteLine("Datos de employees cargados a MongoDB");

            // Ejercicio 6: Cargar clients.json a MongoDB
            
            var mongoClientDao = new MongoClientImpl();
            /*
            mongoClientDao.InsertAll(fileClientDao.SelectAll());
            Console.WriteLine("Datos de clients cargados a MongoDB");
            */

            // Ejercicio 7: Modificar empleado 7499
            var emp7499 = mongoEmpDao.Select(7499);
            if (emp7499 != null)
            {
                Console.WriteLine($"Empleado 7499 antes: {emp7499.Surname}, Salario: {emp7499.Salary}");

                emp7499.Salary = 1800;
                mongoEmpDao.Update(emp7499);
                sqlEmpDao.Update(emp7499);
                fileEmpDao.InsertAll(sqlEmpDao.SelectAll());

                Console.WriteLine($"Empleado 7499 después: {emp7499.Surname}, Salario: {emp7499.Salary}");
            }

            // Ejercicio 8: Modificar cliente 101
            var client101 = mongoClientDao.Select(101);
            if (client101 != null)
            {
                Console.WriteLine($"Cliente 101 antes: {client101.Name}, Teléfono: {client101.Phone}");

                client101.Phone = "555-2331";
                mongoClientDao.Update(client101);
                sqlClientDao.Update(client101);
                fileClientDao.InsertAll(sqlClientDao.SelectAll());

                Console.WriteLine($"Cliente 101 después: {client101.Name}, Teléfono: {client101.Phone}");
            }

            // Ejercicio 9: Insertar nuevo empleado
            /*
            var newEmployee = new Employee
            {
                _id = 9999,
                Surname = "Nuevo",
                Job = "ANALYST",
                Managerid = 7839,
                Startdate = DateTime.Now,
                Salary = 2500,
                Commission = 0,
                Depid = 20
            };

            sqlEmpDao.Insert(newEmployee);
            mongoEmpDao.Insert(newEmployee);
            fileEmpDao.InsertAll(sqlEmpDao.SelectAll());
            Console.WriteLine($"Nuevo empleado {newEmployee.Surname} insertado en todas las fuentes");*/

            // Ejercicio 10: Mostrar departamento del empleado 7788
            var emp7788 = sqlEmpDao.Select(7788);
            if (emp7788 != null)
            {
                var depDao = new SQLDepartmentImpl();
                var dep = depDao.Select(emp7788.Depid);
                Console.WriteLine($"Empleado 7788 trabaja en {dep?.Name ?? "Desconocido"}, {dep?.Loc ?? "Ubicación desconocida"}");
            }

            // Ejercicio 11: Eliminar clientes del empleado 7844
            var clients7844 = sqlClientDao.SelectByEmpId(7844);
            Console.WriteLine($"Empleado 7844 tiene {clients7844.Count} clientes asignados");

            foreach (var client in clients7844)
            {
                sqlClientDao.Delete(client._id);
                mongoClientDao.Delete(client._id);
            }

            fileClientDao.InsertAll(sqlClientDao.SelectAll());
            Console.WriteLine($"Todos los clientes del empleado 7844 han sido eliminados");

            // Ejercicio 12: Clientes del empleado ARROYO
            var arroyoClients = sqlClientDao.SelectByEmpSurname("ARROYO");
            Console.WriteLine($"Empleado ARROYO tiene {arroyoClients.Count} clientes:");

            foreach (var client in arroyoClients)
            {
                Console.WriteLine($"- {client.Name}, Teléfono: {client.Phone}");
            }

            // Verificación con MongoDB
            var arroyoClientsMongo = mongoClientDao.SelectByEmpSurname("ARROYO");
            Console.WriteLine($"Desde MongoDB: Empleado ARROYO tiene {arroyoClientsMongo.Count} clientes");
        }
    }
}
