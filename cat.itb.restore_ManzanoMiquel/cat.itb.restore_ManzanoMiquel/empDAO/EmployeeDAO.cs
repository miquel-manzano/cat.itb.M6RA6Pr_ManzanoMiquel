using cat.itb.gestioHR.depDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.restore_ManzanoMiquel.empDAO
{
    public interface EmployeeDAO
    {
        void DeleteAll();
        void InsertAll(List<Employee> emps);
        List<Employee> SelectAll();
        Employee Select(int empId);
        bool Insert(Employee emp);
        bool Delete(int empId);
        bool Update(Employee emp);
    }
}
