using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace cat.itb.gestioHR.depDAO
{
    public class FileDepartmentImpl : DepartmentDAO
    {
        public void DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public void InsertAll(List<Department> deps)
        {
            FileInfo file = new FileInfo("../../../../departments.json");
            StreamWriter sw = file.CreateText();
            try
            {
                foreach (var dep in deps)
                    sw.WriteLine(JsonConvert.SerializeObject(dep));
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

        public List<Department> SelectAll()
        {
            FileInfo file = new FileInfo("../../../../departments.json");
            StreamReader sr = file.OpenText();
            string dept;
            List<Department> list = new List<Department>();
            while ((dept = sr.ReadLine()) != null)
                list.Add(JsonConvert.DeserializeObject<Department>(dept));
            sr.Close();

            return list;
        }

        public Department Select(int depId)
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(Department dep)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int depId)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Department dep)
        {
            throw new System.NotImplementedException();
        }
    }
}
