using System;
using System.Collections.Generic;

namespace cat.itb.gestioHR.depDAO
{
    public interface DepartmentDAO
    {

        void DeleteAll();
        void InsertAll(List<Department> deps);
        List<Department> SelectAll();
        Department Select(int depId);
        Boolean Insert(Department dep);

        Boolean Delete(int depId);

        Boolean Update(Department dep);

    }
}