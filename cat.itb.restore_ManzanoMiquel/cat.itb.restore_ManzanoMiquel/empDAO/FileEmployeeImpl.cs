using cat.itb.restore_ManzanoMiquel.clieDAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.restore_ManzanoMiquel.empDAO
{
    public class FileEmployeeImpl : EmployeeDAO
    {
        private const string FilePath = "../../../../files/employees.json";

        public void DeleteAll()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }

        public void InsertAll(List<Employee> emps)
        {
            string json = JsonConvert.SerializeObject(emps, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public List<Employee> SelectAll()
        {
            if (!File.Exists(FilePath))
                return new List<Employee>();

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<Employee>>(json);
        }

        public Employee Select(int empId)
        {
            var employees = SelectAll();
            return employees.Find(e => e._id == empId);
        }

        public bool Insert(Employee emp)
        {
            var employees = SelectAll();
            employees.Add(emp);
            InsertAll(employees);
            return true;
        }

        public bool Delete(int empId)
        {
            var employees = SelectAll();
            int removed = employees.RemoveAll(e => e._id == empId);
            if (removed > 0)
            {
                InsertAll(employees);
                return true;
            }
            return false;
        }

        public bool Update(Employee emp)
        {
            var employees = SelectAll();
            int index = employees.FindIndex(e => e._id == emp._id);
            if (index >= 0)
            {
                employees[index] = emp;
                InsertAll(employees);
                return true;
            }
            return false;
        }
    }
}
