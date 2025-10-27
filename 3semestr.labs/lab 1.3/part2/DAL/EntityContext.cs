using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EntityContext
    {

        private const string FILE_NAME = "students.bin";

        public void SaveStudents(List<Student> students)
        {
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, students);
            }
        }

        public List<Student> LoadStudents()
        {
            if (!File.Exists(FILE_NAME))
            {
                return new List<Student>();
            }

            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<Student>)formatter.Deserialize(fs);
            }
        }

        public static List<Student> GetSampleStudents()
        {
            return new List<Student>
            {
            new Student { LastName = "Smal", FirstName = "Ruvim", Course = 4, StudentID = "ID001", DateOfBirth = new DateTime(2000, 4, 15) },
            new Student { LastName = "Smal", FirstName = "Olena", Course = 4, StudentID = "ID002", DateOfBirth = new DateTime(2001, 7, 20) },
            new Student { LastName = "Smal", FirstName = "Margo", Course = 4, StudentID = "ID003", DateOfBirth = new DateTime(2000, 3, 5) },
            new Student { LastName = "Smal", FirstName = "Tatti", Course = 3, StudentID = "ID004", DateOfBirth = new DateTime(2002, 5, 10) }
            };
        }
    }
}
