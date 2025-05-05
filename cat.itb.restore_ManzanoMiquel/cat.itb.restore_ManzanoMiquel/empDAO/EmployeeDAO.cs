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
        Boolean Insert(Employee emp);
        Boolean Delete(int empId);
        Boolean Update(Employee emp);
    }
}
