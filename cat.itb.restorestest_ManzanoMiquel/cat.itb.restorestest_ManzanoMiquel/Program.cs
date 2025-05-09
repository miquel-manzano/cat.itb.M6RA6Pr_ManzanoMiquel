using cat.itb.gestioHR.depDAO;
using cat.itb.restore_ManzanoMiquel;

namespace cat.itb.restorestest_ManzanoMiquel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inici programa");

            MongoDepartmentImpl mongoImp = new MongoDepartmentImpl();

            List<Department> list = mongoImp.SelectAll();

            foreach (Department department in list)
            {
                Console.WriteLine(department.Name);
            }
        }
    }
}
