using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace DAL
{
    public interface IDataStream
    {
        List<Student> ReadAllStudents(string fileName);
        void CreateFile(string fileName, IEnumerable<Student> students);
    }
}
