using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.restore_ManzanoMiquel.empDAO
{
    public class Employee
    {
        public virtual int _id { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Job { get; set; }
        public virtual int Managerid { get; set; }
        public virtual DateTime Startdate { get; set; }
        public virtual decimal Salary { get; set; }
        public virtual decimal Commission { get; set; }
        public virtual int Depid { get; set; } // FK departments
    }
}
