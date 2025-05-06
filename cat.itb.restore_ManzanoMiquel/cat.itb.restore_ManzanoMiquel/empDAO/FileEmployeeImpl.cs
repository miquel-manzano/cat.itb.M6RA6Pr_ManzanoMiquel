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
        public void DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public void InsertAll(List<Employee> emps)
        {
            FileInfo file = new FileInfo("../../../../files/employees.json");
            StreamWriter sw = file.CreateText();
            try
            {
                foreach (var emp in emps)
                    sw.WriteLine(JsonConvert.SerializeObject(emp));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nSuccesful inserts in file departments.json");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInserts in departments.json couldn't be executed");
            }
            sw.Close();
            Console.ResetColor();
        }

        public List<Employee> SelectAll()
        {
            FileInfo file = new FileInfo("../../../../files/employees.json");
            StreamReader sr = file.OpenText();
            string emps;
            List<Employee> list = new List<Employee>();
            while ((emps = sr.ReadLine()) != null)
                list.Add(JsonConvert.DeserializeObject<Employee>(emps));
            sr.Close();

            return list;
        }

        public Employee Select(int empId)
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(Employee emp)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int empId)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Employee emp)
        {
            throw new System.NotImplementedException();
        }
    }
}
